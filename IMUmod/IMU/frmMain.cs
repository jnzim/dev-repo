using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using System.Diagnostics;
using ZedGraph;
using AForge.Controls;


/*
 * gains: kp = 2, ki = 3, kp_rate = 52, ki_rate = 8
 * 
 */

namespace IMU
{
    public partial class frmMain : Form
    {
    


        
        public frmMain()
        {
            InitializeComponent();
        }

        int plusJoystickDeadZone  = 30;
        int  minusJoystickDeadZone  = -30;
        bool gotJoystick = false;
        const double UMT_CONVERT_EULER = 1; //0.0109863;         //  number spec'd in the UM6 data sheet to convert sensor output to degrees
        const int NUM_BYTES_TO_RECIVE = 16;
        const int YEI_CONVERT_EULER = 1;
        const int pitchCommandScale = 10;
        const int rollCommandScale  = 10;
        const int yawCommandScale = 10;


        int cmd;  ///  hold command from text box enter event
                ///  
        short int_Step_Command_Increment = 0;
        int intUI_Update_mSec = 50;
        int intAutoCommandCounter = 0;
        //int intUI_SendData = 20;
        List<byte> bBuffer = new List<byte>();
        //const int YEI_CONVERT_EULER = (int)((1/16383)*180/Math.PI);
        //const int YEI_CONVERT_EULER = 1;

        short int16_GPS_N, int16_GPS_E, int16_GPS_A, int16_GPS_SPEED;


       const Int16 NULL_COMMAND			    =0x0000;
       const Int16 UM6_GET_VERSION			=0x00AA;
       const Int16 UM6_FLASH_COMMIT		    =0x00AB;
       const Int16 UM6_ZERO_GYROS			=0x00AC;
       const Int16 UM6_RESET_EFK			=0x00AD;
       const Int16 SET_ACCEL_REF			=0x00AF;
       const Int16 UM6_SET_MAG_REF			=0x00B0;
       const Int16 UM6_SET_HOME_POSITION	=0x00B3;
       const Int16 ZERO_SENSORS             =0x0001;
       const Int16 ARM_SYSTEM               =0x0002;
       const Int16 DISARM                   =0x0005;

        
        enum errorType
        {
            yawLargeNumber,
            yawNegNumber,
            pitchLargeNumber,
            pitchNegNumber,
            rollLargeNumber,
            rollNegNumber,

            AccelXLargeNumber,
            AccelXNegNumber,
            AccelYLargeNumber,
            AccelYNegNumber,
            AccelZLargeNumber,
            AccelZNegNumber

        }

        Joystick joystick;    
        frmBodePlotChart PlotChartForm; 
        // = new frmPlotChart();
        //frmBodePlot BodePlotForm;

        clsAxis Pitch  = new clsAxis();
        clsAxis Roll = new clsAxis();
        clsAxis Yaw = new clsAxis();
        clsAxis Thrust = new clsAxis();
        Filter filter = new Filter();

        

  

       static System.Windows.Forms.Timer UpdateUI_timer = new System.Windows.Forms.Timer();
       //static System.Windows.Forms.Timer Send_UART_timer = new System.Windows.Forms.Timer();
       //private delegate void SetTextDeleg(int data);


       private void Form1_Load(object sender, EventArgs e)
       {
           this.send_UI_message("Form Load");
           var ports = SerialPort.GetPortNames();
           this.buttonStop.Enabled = false;
           this.comboBoxComPort.DataSource = ports;

         }


        private void button1_Click(object sender, EventArgs e)
        {
            if (this.GetAvaliableJoystickControls() == true)
            {
                this.gotJoystick = true;
                if (this.initSerailPort() == true)
                {
                    this.EnableUI_Start();
                    this.StartTheTimmers();

                    if (checkBoxPlotForm.Checked == true)
                    {
                        this.OpenPlot();
                    }
                }
                else
                {
                    this.gotJoystick = false;
                    this.send_UI_message("No joystick found.");
                    this.StartTheTimmers();
                }

            }
       
        }






        private bool initSerailPort()
        {
              if (string.IsNullOrEmpty(comboBoxComPort.Text))
                {
                   this.send_UI_message( "No serial ports found.");
                   return false;
                }
                else
                {
                    this._serialPort = new SerialPort();
                    this._serialPort.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);
                    this._serialPort.PortName = this.comboBoxComPort.SelectedItem.ToString();
                    this._serialPort.BaudRate = 115200;
                    this._serialPort.Parity = Parity.None;
                    this._serialPort.StopBits = StopBits.Two;

                    this._serialPort.Open();
                    if (_serialPort.IsOpen)
                    {

                        this.send_UI_message("Serial port " + this.comboBoxComPort.SelectedItem.ToString() + " is initialized.");
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
        }

        private void OpenPlot()
        {

            this.PlotChartForm = new frmBodePlotChart();       
            this.PlotChartForm.Show();

            //this.BodePlotForm = new frmBodePlot();
            //this.BodePlotForm.Show();


            this.Refresh();
            Application.DoEvents();
            this.send_UI_message("Open plot form.");



         }
 

        private void buttonClose_Click(object sender, EventArgs e)
        {
            if (  _serialPort.IsOpen)
            {
                this._serialPort.Close();
                this.send_UI_message(this.comboBoxComPort.SelectedItem.ToString() + " is closed.");
                this.buttonStart.Enabled = true;
                this.buttonStop.Enabled = false;
                this.textIMUYaw.ReadOnly = true;
                this.comboBoxComPort.Enabled = true;
            }
            if (this.PlotChartForm != null)
            {
                this.PlotChartForm.Dispose();
            }
            this.send_UI_message("Disposed PlotChartForm form");
        }

        int map(int x, int in_min, int in_max, int out_min, int out_max)
        {
            return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
        }

        //  return (x - 0) * (16000 - 3000) / (4096 - 0) + 3000;
        //event handeler for data recvied event
        //  get all the bytes from the serail port buffer and put it in an array until there is time to process it
        void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
    
            while (_serialPort.BytesToRead > 0)
            {
                bBuffer.Add((byte)_serialPort.ReadByte());
            }            
           
        }


        // EXAMPLE OF FRAME:  255 254 90 97 2 34 8 255 255 254 255 253 10 110 255 254 91 100 
        // 255 = 0b1111 1111 0xFF
        // 254 = 0b1111 1110 0xFE

        //  look for the begingin of the frame and then start parsing out the data
        private void ProcessData()
        {
          

            //  check the first 2 element in the list, if they are 0xFF and 0xFE, toss them and read in a frame
            //  if they are not 0xFF and 0xFF, the first one and check the next 2
            //  go thru all the data thats been stored in the buffer
            while (bBuffer.Count >= NUM_BYTES_TO_RECIVE)
            {             
                
                if ((bBuffer.ElementAt(0) == 0xCC) && (bBuffer.ElementAt(1) == 0xCC))
                {
                    // we have found the start of a frame so we can remove the header, read in, and parse the data
                    //debugbuffer();
                    bBuffer.RemoveAt(0); 
                    bBuffer.RemoveAt(0);
                    ParseRecivedData();

                }
                else if (bBuffer.Count >= NUM_BYTES_TO_RECIVE)
                {
                    //  just remove 1 byte so we can iterate thru the buffer
                    bBuffer.RemoveAt(0);
                }
            }
                
        }


        void debugbuffer()
        {

            for (int i = 0 ; i <= (bBuffer.Count-1); i++)
            {
                //Debug.WriteLine(bBuffer[i].ToString("x"));
           }

        }
       

        //  Data that comes in from the serial port is stuffed into a byte array.  This function parses out that data and stores into global variables
        //  once we have found the beging of the frame, begin assmbling the bytes in to 16 bit int
        private void ParseRecivedData()
        {


            //Debug.WriteLine(0x00);
            //Debug.WriteLine(0x00);
      
            this.Thrust.thrust_cmd = (short)(bBuffer.ElementAt(0) << 8 | bBuffer.ElementAt(1));
            //Debug.WriteLine("T= " + Thrust.thrust_cmd.ToString("x"));
            bBuffer.RemoveAt(0); bBuffer.RemoveAt(0);

            this.Roll.attitude_feedback = (short)(bBuffer.ElementAt(0) << 8 | bBuffer.ElementAt(1));
            // Debug.WriteLine("R " + Roll.attitude_feedback.ToString());
            bBuffer.RemoveAt(0); bBuffer.RemoveAt(0);


            this.Pitch.attitude_feedback = (short)(bBuffer.ElementAt(0) << 8 | bBuffer.ElementAt(1));
            //Debug.WriteLine("P " + Pitch.attitude_feedback.ToString());
            bBuffer.RemoveAt(0); bBuffer.RemoveAt(0);

            this.Yaw.attitude_feedback = (short)(bBuffer.ElementAt(0) << 8 | bBuffer.ElementAt(1));
            //Debug.WriteLine("Y= " + Yaw.attitude_feedback.ToString("x"));
            bBuffer.RemoveAt(0); bBuffer.RemoveAt(0);

            this.Roll.rate_feedback = (short)(bBuffer.ElementAt(0) << 8 | bBuffer.ElementAt(1));
           // Debug.WriteLine("RR= " + Pitch.attitude_pid_out.ToString("x"));
            bBuffer.RemoveAt(0); bBuffer.RemoveAt(0);

            this.Pitch.rate_feedback = (short)(bBuffer.ElementAt(0) << 8 | bBuffer.ElementAt(1));
            //Debug.WriteLine("PR= " + Thrust.thrust_cmd.ToString("x"));
            bBuffer.RemoveAt(0); bBuffer.RemoveAt(0);

            this.Yaw.rate_feedback = (short)(bBuffer.ElementAt(0) << 8 | bBuffer.ElementAt(1));
            //Debug.WriteLine("Y= " + this.Yaw.rate_feedback.ToString("x"));
            bBuffer.RemoveAt(0); bBuffer.RemoveAt(0);

    

        }


        //  send the 16 bit commands 8 bits at a time 
        //  start with the header 0xFF 0xFD so the uC know where to start parsing 
        //  the uC DMAC will put this in a buffer
        private void SendCommand_SerailPort()
        {
            try
            {
                // Data header: 
                byte upperByte = (byte)(0xCC);
                byte lowerByte = (byte)(0xCC);
                byte[] buffer = new byte[] { upperByte, lowerByte };
                //Debug.WriteLine(upperByte.ToString("x"));
                //Debug.WriteLine(lowerByte.ToString("x"));
                this._serialPort.Write(buffer, 0, 2);

                //this.Thrust.thrust_cmd = 0x0009;
                buffer[0] = (byte)(this.Thrust.thrust_cmd >> 8);
                buffer[1] = (byte)(this.Thrust.thrust_cmd & 0xff);
                this._serialPort.Write(buffer, 0, 2);
                //Debug.WriteLine(buffer[0]); Debug.WriteLine(buffer[1]);


                //this.Roll.attitude_command = 0x0005;
                buffer[0] = (byte)(this.Roll.attitude_command >> 8);
                buffer[1] = (byte)(this.Roll.attitude_command & 0xff);
                //Debug.WriteLine(buffer[0].ToString("x"));
                //Debug.WriteLine(buffer[1].ToString("x"));
                this._serialPort.Write(buffer, 0, 2);

               // this.Pitch.attitude_command = 0x0003;
                buffer[0] = (byte)(this.Pitch.attitude_command >> 8);
                buffer[1] = (byte)(this.Pitch.attitude_command & 0xff);
                //Debug.WriteLine(buffer[0].ToString("x"));
                //Debug.WriteLine(buffer[1].ToString("x"));
                this._serialPort.Write(buffer, 0, 2);

                //this.Yaw.attitude_command = 0x0004;
                buffer[0] = (byte)(this.Yaw.attitude_command >> 8);
                buffer[1] = (byte)(this.Yaw.attitude_command & 0xff);
                //Debug.WriteLine(buffer[0].ToString("x"));
                //Debug.WriteLine(buffer[1].ToString("x"));
                this._serialPort.Write(buffer, 0, 2);


                buffer[0] = (byte)((short)this.numericUpDownKp_Pitch.Value >> 8);
                buffer[1] = (byte)((short)this.numericUpDownKp_Pitch.Value & 0xff);
                this._serialPort.Write(buffer, 0, 2);

                buffer[0] = (byte)((short)this.numericUpDownKi_Pitch.Value >> 8);
                buffer[1] = (byte)((short)this.numericUpDownKi_Pitch.Value & 0xff);

                this._serialPort.Write(buffer, 0, 2);

                buffer[0] = (byte)((short)this.numericUpDownKd_Pitch.Value >> 8);
                buffer[1] = (byte)((short)this.numericUpDownKd_Pitch.Value & 0xff);
                this._serialPort.Write(buffer, 0, 2);

                buffer[0] = (byte)(this.cmd >> 8);
                buffer[1] = (byte)(this.cmd & 0xff);
                this._serialPort.Write(buffer, 0, 2);
            

                //buffer[0] = (byte)((short)this.numericUpDownKi_Roll.Value >> 8);
                //buffer[1] = (byte)((short)this.numericUpDownKi_Roll.Value & 0xff);
                //this._serialPort.Write(buffer, 0, 2);


                //buffer[0] = (byte)((short)this.numericUpDownKd_Roll.Value >> 8);
                //buffer[1] = (byte)((short)this.numericUpDownKd_Roll.Value & 0xff);
                //this._serialPort.Write(buffer, 0, 2);


               this.cmd = 0x0000;        //clear command byte after it's sent
              
                
            }
            catch(Exception ex)
            {

                this.send_UI_message("SendCommand_SerailPort method error. " + ex.Message.ToString());
            }

        }


        private void Update_UI()
        {
            //float filtRoll = filter.complementaryFilter(this.Pitch.


            this.textIMURoll.Text = String.Format("{0:0.00}", (Convert.ToDouble(this.Roll.attitude_feedback) / YEI_CONVERT_EULER));
            this.textIMUPitch.Text = String.Format("{0:0.00}", (Convert.ToDouble(this.Pitch.attitude_feedback) / YEI_CONVERT_EULER));
            this.textIMUYaw.Text = String.Format("{0:0.00}", (Convert.ToDouble(this.Yaw.attitude_feedback) / YEI_CONVERT_EULER));

            this.textBoxRollRate.Text = String.Format("{0:0.00}", (Convert.ToDouble(this.Roll.rate_feedback) ));
            this.textBoxPitchRate.Text = String.Format("{0:0.00}", (Convert.ToDouble(this.Pitch.rate_feedback) ));
            this.textBoxYawRate.Text = String.Format("{0:0.00}", (Convert.ToDouble(this.Yaw.rate_feedback) ));

            //this.textBox_GPS_N.Text = String.Format("{0:0.00}", (Convert.ToDouble(this.int16_GPS_N)/16383));
            //this.textBox_GPS_E.Text = String.Format("{0:0.00}", (Convert.ToDouble(this.int16_GPS_E)/16383));
            //this.textBox_GPS_A.Text = String.Format("{0:0.00}", (Convert.ToDouble(this.int16_GPS_A)/16383));
            //this.textBox_GPS_SPD.Text = String.Format("{0:0.00}", (Convert.ToDouble(this.int16_GPS_SPEED)/16383));

            this.textBox_GPS_N.Text = String.Format("{0:0.00}", (Convert.ToDouble(this.int16_GPS_N)));
            this.textBox_GPS_E.Text = String.Format("{0:0.00}", (Convert.ToDouble(this.int16_GPS_E)));
            this.textBox_GPS_A.Text = String.Format("{0:0.00}", (Convert.ToDouble(this.int16_GPS_A)));
            this.textBox_GPS_SPD.Text = String.Format("{0:0.00}", (Convert.ToDouble(this.int16_GPS_SPEED)));


            this.textBoxcmdThrolttle.Text = this.Thrust.thrust_cmd.ToString();
            this.textBoxcmdPitch.Text = this.Pitch.attitude_command.ToString();
            this.textBoxcmdRoll.Text = this.Roll.attitude_command.ToString();
            this.textBoxcmdYaw.Text = this.Yaw.attitude_command.ToString();

       

            //this.textBoxPitchPIDout.Text = this.Pitch.attitude_pid_out.ToString();
            //// update the form with acceleration data

            // update the zedgraph plot with new data
            if (this.checkBoxPlotForm.Checked == true)
            {
                //this.PlotChartForm.UpdateGraph(DateTime.Now, this.int_imu_Yaw , this.int_Roll_Command , this.int_imu_Pitch , 
                //    this.int_Roll_Error , this.int_Roll_Rate_PID);

               // this.PlotChartForm.UpdateGraph(DateTime.Now, this.Yaw.attitude_feedback / YEI_NVERT_EULER, this.Roll.attitude_feedback / YEI_NVERT_EULER, this.Pitch.attitude_feedback / YEI_NVERT_EULER, this.Pitch.rate_feedback );
                this.PlotChartForm.UpdateGraph(DateTime.Now, (double)(this.Roll.rate_feedback), this.Pitch.rate_feedback , this.Yaw.rate_feedback , this.int16_GPS_E);
                //Debug.WriteLine(this.Roll.attitude_feedback.ToString() + " , " + this.Pitch.attitude_feedback.ToString() + " , " + this.Yaw.attitude_feedback.ToString() + " , " + this.Roll.rate_feedback.ToString() + " , " + this.Pitch.rate_feedback.ToString() + " , " + this.Yaw.rate_feedback.ToString());
                //Debug.WriteLine(this.Roll.attitude_feedback.ToString("x") + " , " + this.Pitch.attitude_feedback.ToString("x") + " , " + this.Yaw.attitude_feedback.ToString("x") + " , " + this.Roll.rate_feedback.ToString("x") + " , " + this.Pitch.rate_feedback.ToString("x") + " , " + this.Yaw.rate_feedback.ToString("x"));

                //this.PlotChartForm.UpdateGraph(DateTime.Now, this.int_imu_Yaw, this.int_imu_Pitch, this.int_imu_Roll, this.int_Roll_Error, 
                //    this.int_Roll_Command, this.int_Roll_PID);


                //this.PlotChartForm.UpdateGraph(DateTime.Now, this.int_imu_Roll, this.int_Roll_Error,
                //    this.int_Roll_Command, this.int_Roll_PID);

            }
        }

        private short GetAutoCommand()
        {
            if (this.intAutoCommandCounter < 500)
            {
                return 0;
            }
            else if (this.intAutoCommandCounter >= 200 && this.intAutoCommandCounter < 400)
            {
                return -1000;
            }
            else
            {
                return 2000;
                
            }

        }

        private bool GetAvaliableJoystickControls()
        {
            // enumerate available devices
            List<Joystick.DeviceInfo> devices = Joystick.GetAvailableDevices();

            foreach (Joystick.DeviceInfo di in devices)
            {
                this.send_UI_message(string.Format("{0} : {1} ({2} axes, {3} buttons)", di.ID, di.Name, di.Axes, di.Buttons));
            }

            try
            {
                // create new joystick and initialize it
                joystick = new Joystick(0);
                // get its current status
                joystick.Init(0);
                return true;
            }
            catch (Exception ex)
            {
                this.send_UI_message("No joystick found." + ex.Message.ToString());
                return false;
            

            }
           
        }
     
        private void GetJoystickCommands()
        {
            //  get the joystick command which is a float between -1 and 1 
           
            AForge.Controls.Joystick.Status status = joystick.GetCurrentStatus();

            this.Thrust.thrust_cmd = (short)(2047 * (1 + -status.ZAxis));
            if (this.checkBoxStepCommandOn.Checked == false)
            {
                if ((this.Yaw.attitude_command = (short)((16383/yawCommandScale * status.RAxis))) >= minusJoystickDeadZone && this.Yaw.attitude_command <= plusJoystickDeadZone)
                {
                   this.Yaw.attitude_command = 0;
                   
                }
                if ((this.Pitch.attitude_command = (short)((8192/pitchCommandScale * status.YAxis))) >= minusJoystickDeadZone && this.Pitch.attitude_command <= plusJoystickDeadZone)
                {
                    this.Pitch.attitude_command = 0;
                    //this.Pitch.attitude_command = this.GetAutoCommand();
                }
                if ((this.Roll.attitude_command = (short)((16383/rollCommandScale * status.XAxis))) >= minusJoystickDeadZone && this.Roll.attitude_command <= plusJoystickDeadZone)
                {
                    this.Roll.attitude_command = 0;
                   // this.Roll.attitude_command = this.GetAutoCommand();
                }

                this.Roll.attitude_command = this.GetAutoCommand();
            }
            else
            {

                this.Yaw.attitude_command = 0;

                this.Pitch.attitude_command = 0;

                this.Roll.attitude_command = 0; // (short)(this.int_Step_Command_Increment);
              
            }
        }

        private void Error(int errorType, object data)
        {
            switch (errorType)
            {
                case 0:

                    break;
                case 1:

                    break;
                case 2:

                default:
                    Console.WriteLine("Invalid selection. Please select 1, 2, or 3.");
                    break;
            }
        }

        private void send_UI_message(string message)
        {

            textBoxUImessage.Text += DateTime.Now.ToLongTimeString() + "\t " + message + "\r\n";
            textBoxUImessage.SelectionStart = textBoxUImessage.Text.Length;
            textBoxUImessage.ScrollToCaret();

        }

        private void checkBoxPlotForm_CheckedChanged(object sender, EventArgs e)
        {
            
        }



        void timer_Tick(object sender, EventArgs e)
        {
            if (this.gotJoystick == true)
            {
                this.SendCommand_SerailPort();
                this.ProcessData();
                this.GetJoystickCommands();
                this.Update_UI();
                this.intAutoCommandCounter++;
                if (this.intAutoCommandCounter >= 1000)
                {
                    this.intAutoCommandCounter = 0;
                }
            }
         
            else if (this.GetAvaliableJoystickControls() == true)
            {
                this.EnableUI_Start();
                this.gotJoystick = true;
            }
            

          }

        //void timer_Tick_USART(object sender, EventArgs e)
        //{
       
        //        this.SendCommand_SerailPort();

        //}
        


        //  we found the joystick so set up the UI so we can enable the user to 
        //  start talking to the quad
        private void EnableUI_Start()
        {
            this.buttonStart.Enabled = false;
            this.buttonStop.Enabled = true;
            this.comboBoxComPort.Enabled = false;
            this.buttonStop.Enabled = true;


        }


        //  set up the time and start it
        private void StartTheTimmers()
        {
            UpdateUI_timer.Tick += new EventHandler(timer_Tick);     // Everytime timer ticks], timer_Tick will be called
            UpdateUI_timer.Interval = intUI_Update_mSec;            // Timer will tick evert 10 seconds
            UpdateUI_timer.Enabled = true;                           // Enable the timer
            UpdateUI_timer.Start();                                  // Start the timer

            //Send_UART_timer.Tick += new EventHandler(timer_Tick_USART);     // Everytime timer ticks], timer_Tick will be called
            //Send_UART_timer.Interval = intUI_Update_mSec;            // Timer will tick evert 10 seconds
            //Send_UART_timer.Enabled = true;                           // Enable the timer
            //Send_UART_timer.Start();   
        }

        private void numericUpDownKi_Pitch_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void textBoxcmdThrolttle_Enter(object sender, EventArgs e)
        {
            //this.joyStickDataThrottle = Convert.ToInt16(this.textBoxcmdThrolttle.Text);
        }

        private void textBoxcmdThrolttle_Click(object sender, EventArgs e)
        {
            this.Thrust.thrust_cmd = Convert.ToInt16(this.textBoxcmdThrolttle.Text);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.int_Step_Command_Increment = (short)(Convert.ToInt16(this.textBoxStepCommand.Text));
        }


        private void btn_enter_Click(object sender, EventArgs e)
        {

            //KeyPressEventArgs arg = new KeyPressEventArgs(Convert.ToChar(Keys.Enter));

            //textBoxCommands_Enter(sender, arg);

        }

        private void textBoxCommands_DoubleClick(object sender, EventArgs e)
        {
            cmd = 0x0002;
        }

        private void listCommands_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.listCommands.SelectedIndex)
            {
                case 0:
                    this.cmd = ZERO_SENSORS;
                    break;
                case 1:
                    this.cmd = ARM_SYSTEM;
                    break;
                case 2:
                    this.cmd = DISARM;
                    break;
                case 3:
                    this.cmd = SET_ACCEL_REF;
                    break;
                case 4:
                    this.cmd = SET_ACCEL_REF;
                    break;
                case 5:
                    this.cmd = UM6_SET_MAG_REF;
                    break;
                case 6:
                    this.cmd = UM6_ZERO_GYROS;
                    break;
                case 7:
                    this.cmd = NULL_COMMAND;
                    break;
                case 8:
                    this.cmd = ZERO_SENSORS;
                    break;
            }
        }

 
//        #define NULL_COMMAND			0x00
//#define UM6_GET_VERSION			0xAA
//#define UM6_FLASH_COMMIT		0xAB
//#define UM6_ZERO_GYROS			0xAC
//#define UM6_RESET_EFK			0xAD
//#define SET_ACCEL_REF			0xAF
//#define UM6_SET_MAG_REF			0xB0
//#define UM6_SET_HOME_POSITION	0xB3


    }


}
