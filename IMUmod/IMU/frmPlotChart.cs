using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using ZedGraph;


namespace IMU
{
    public partial class frmBodePlotChart : Form
    {

       // public event EventHandler Resize;
        static int numberOfPoints = 150;

        RollingPointPairList pid_List = new RollingPointPairList(numberOfPoints);
        RollingPointPairList cmd_List = new RollingPointPairList(numberOfPoints);
        RollingPointPairList feedback_List = new RollingPointPairList(numberOfPoints);
        RollingPointPairList Error_List = new RollingPointPairList(numberOfPoints);
        RollingPointPairList imu_Yaw_List = new RollingPointPairList(numberOfPoints);
        RollingPointPairList imu_Pitch_List = new RollingPointPairList(numberOfPoints);
        RollingPointPairList imu_Roll_List = new RollingPointPairList(numberOfPoints);
        RollingPointPairList roll_PID_List = new RollingPointPairList(numberOfPoints);
        RollingPointPairList roll_Error_List = new RollingPointPairList(numberOfPoints);
        RollingPointPairList roll_Command_List = new RollingPointPairList(numberOfPoints);
        RollingPointPairList lstRollFeedback = new RollingPointPairList(numberOfPoints);
        RollingPointPairList lstPitchFeedback = new RollingPointPairList(numberOfPoints);
        RollingPointPairList lstYawFeedback = new RollingPointPairList(numberOfPoints);


        
        //RollingPointPairList cmdlistYaw = new RollingPointPairList(numberOfPoints);
        //RollingPointPairList cmdlistRoll = new RollingPointPairList(numberOfPoints);
        //RollingPointPairList cmdlistPitch = new RollingPointPairList(numberOfPoints);
       
        LineItem myPID_curve;
        LineItem myCommand_curve;
        LineItem yawCurve, rollCurve, pitchCurve;
        LineItem myCMD_curve;
        LineItem myFeedback_curve;
        LineItem myError_curve;
        LineItem roll_Rate_PID_curve;
        
        public frmBodePlotChart()
        {
            InitializeComponent();
        }

        private void frmPlotChart_Load(object sender, EventArgs e)
        {
            // Setup the graph
            CreateGraph();
            // Size the control to fill the form with a margin
            SetSize();
        }


        public void UpdateGraph(XDate xValue, double pid, double cmd, double feedback,double error,double rollRatePID)
        {

           
            //imuListYaw.Add(xValue, (double)imuYaw);
            pid_List.Add(xValue, pid);
            //roll_Rate_PID_List.Add(xValue, rollRatePID);
            //imuListPitch.Add(xValue, (double)imuPitch);
            cmd_List.Add(xValue, cmd);
            feedback_List.Add(xValue, feedback);
            Error_List.Add(xValue, error);
            //cmdlistRoll.Add(xValue, (double)cmdRoll);
            zedGraphControl1.Invalidate();
            zedGraphControl1.AxisChange();

        }



        public void UpdateGraph(XDate xValue, double roll, double pitch, double yaw)
        {
             this.imu_Roll_List.Add(xValue, roll);
             this.imu_Pitch_List.Add(xValue, pitch);
             this.imu_Yaw_List.Add(xValue, yaw);

            zedGraphControl1.Invalidate();
            zedGraphControl1.AxisChange();

        }



        //public void UpdateGraph(XDate xValue, double yaw, double pitch, double roll, double rollError, double rollCommand, double rollPID)
        //{

            
            
            
        //    this.lstYawFeedback.Add(xValue, yaw);
        //    this.lstRollFeedback.Add(xValue, roll);
        //    this.lstPitchFeedback.Add(xValue, pitch); 
        //    this.roll_Command_List.Add(xValue, rollCommand);
        //    this.roll_Error_List.Add(xValue, rollError);
        //    this.roll_PID_List.Add(xValue, rollPID);

        //    zedGraphControl1.Invalidate();
        //    zedGraphControl1.AxisChange();

        //}

        public void UpdateGraph(double time, double yaw, double pitch, double roll, double rollError, double rollCommand, double rollPID)
        {

            // Keep the X scale at a rolling 30 second interval, with one
            // major step between the max X value and the end of the axis
            Scale xScale = zedGraphControl1.GraphPane.XAxis.Scale;
            if (time > xScale.Max - xScale.MajorStep)
            {
                xScale.Max = time + xScale.MajorStep;
                xScale.Min = xScale.Max - 15.0;
            }


            this.lstYawFeedback.Add(time, yaw);
            this.lstRollFeedback.Add(time, roll);
            this.lstPitchFeedback.Add(time, pitch);
            this.roll_Command_List.Add(time, rollCommand);
            this.roll_Error_List.Add(time, rollError);
            this.roll_PID_List.Add(time, rollPID);

            zedGraphControl1.Invalidate();
            zedGraphControl1.AxisChange();

        }


        public void UpdateGraph(XDate xValue,  double roll, double pitch, double yaw, double command)
        {

            this.lstYawFeedback.Add(xValue, yaw);
            this.lstRollFeedback.Add(xValue, roll);
            this.lstPitchFeedback.Add(xValue, pitch); 
            this.roll_PID_List.Add(xValue, command);

            zedGraphControl1.Invalidate();
            zedGraphControl1.AxisChange();

        }


        // Build the Chart
        private void CreateGraph()
        {
            // get a reference to the GraphPane
            GraphPane myPane = zedGraphControl1.GraphPane;

            // Set the Titles
            myPane.Title.Text = "Quad Copter Control";
            myPane.XAxis.Title.Text = "Time MM:ss";
            myPane.YAxis.Title.Text = "Angle / Angular rate";

  
            // Generate a red curve with diamond
            // symbols, and "Porsche" in the legend

            // Generate a blue curve with circle
            // symbols, and "Piper" in the legend
            this.myCMD_curve = myPane.AddCurve("Roll", this.lstYawFeedback, Color.Blue, SymbolType.None);
             this.myCMD_curve.Line.Width = 3.0F;
             // Generate a blue curve with circle
             // symbols, and "Piper" in the legend
             this.myFeedback_curve = myPane.AddCurve("Pitch", this.lstPitchFeedback, Color.Green, SymbolType.None);
             this.myFeedback_curve.Line.Width = 3.0F;
             // Generate a red curve with diamond
             // symbols, and "Porsche" in the legend
             this.myPID_curve = myPane.AddCurve("Yaw", this.lstRollFeedback, Color.Yellow, SymbolType.None);
             this.myPID_curve.Line.Width = 3.0F;

             this.myCommand_curve = myPane.AddCurve("Roll Rate", this.roll_PID_List, Color.Black, SymbolType.None);
             this.myCommand_curve.Line.Width = 3.0F;

             this.myError_curve = myPane.AddCurve("Pitch Rate", this.roll_Error_List, Color.Green, SymbolType.None);
             this.myError_curve.Line.Width = 3.0F;

             this.myPID_curve = myPane.AddCurve("Yaw Rate", this.roll_PID_List, Color.Red, SymbolType.None);
             this.myPID_curve.Line.Width = 3.0F;

            //this.roll_Rate_PID_curve = myPane.AddCurve("Rate PID out", this.roll_Rate_PID_List, Color.Orange, SymbolType.None);
            //this.roll_Rate_PID_curve.Line.Width = 3.0F;
          
            // Tell ZedGraph to refigure the`
            // axes since the data have changed
             //myPane.XAxis.Type = AxisType.Date;
             //myPane.XAxis.Scale.MajorUnit = DateUnit.Millisecond;
             //myPane.XAxis.Scale.Format = "T";

             myPane.XAxis.Scale.Min = 0;
             myPane.XAxis.Scale.Max = 15;
             myPane.XAxis.Scale.MinorStep = 1;
             myPane.XAxis.Scale.MajorStep = 1;

             myPane.XAxis.MajorGrid.IsVisible = true;

            zedGraphControl1.AxisChange();
        }


        // SetSize() is separate from Resize() so we can 
        // call it independently from the Form1_Load() method
        // This leaves a 10 px margin around the outside of the control
        // Customize this to fit your needs
        private void SetSize()
        {
            zedGraphControl1.Location = new Point(10, 10);
            // Leave a small margin around the outside of the control
            zedGraphControl1.Size = new Size(ClientRectangle.Width - 20, ClientRectangle.Height - 20);
        }

        private void frmPlotChart_ResizeBegin(object sender, EventArgs e)
        {
            SetSize();
        }



    }
}
