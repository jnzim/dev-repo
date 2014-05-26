namespace IMU
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textIMUPitch = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textIMURoll = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.textIMUYaw = new System.Windows.Forms.TextBox();
            this.comboBoxComPort = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxcmdThrolttle = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxcmdYaw = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxcmdRoll = new System.Windows.Forms.TextBox();
            this.textBoxcmdPitch = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxYawRate = new System.Windows.Forms.TextBox();
            this.textBoxRollRate = new System.Windows.Forms.TextBox();
            this.textBoxPitchRate = new System.Windows.Forms.TextBox();
            this.textBoxUImessage = new System.Windows.Forms.TextBox();
            this.checkBoxPlotForm = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBoxYawPIDout = new System.Windows.Forms.TextBox();
            this.textBoxPitchPIDout = new System.Windows.Forms.TextBox();
            this.textBoxYawReadBack = new System.Windows.Forms.TextBox();
            this.textBoxPitchReadBack = new System.Windows.Forms.TextBox();
            this.textBoxRollReadBack = new System.Windows.Forms.TextBox();
            this.textBoxRollPIDout = new System.Windows.Forms.TextBox();
            this.textBoxYawError = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.textBoxPitchError = new System.Windows.Forms.TextBox();
            this.textBoxRollError = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label20 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.numericUpDownKi_Roll = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownKd_Roll = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownKp_Roll = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.numericUpDownKi_Pitch = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownKd_Pitch = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownKp_Pitch = new System.Windows.Forms.NumericUpDown();
            this.buttonUpdateStepCommand = new System.Windows.Forms.Button();
            this.checkBoxStepCommandOn = new System.Windows.Forms.CheckBox();
            this.textBoxStepCommand = new System.Windows.Forms.TextBox();
            this.textBoxLargestNeg = new System.Windows.Forms.TextBox();
            this.textBoxLargestPos = new System.Windows.Forms.TextBox();
            this.listCommands = new System.Windows.Forms.ListBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.textBox_GPS_SPD = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_GPS_N = new System.Windows.Forms.TextBox();
            this.textBox_GPS_A = new System.Windows.Forms.TextBox();
            this.textBox_GPS_E = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownKi_Roll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownKd_Roll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownKp_Roll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownKi_Pitch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownKd_Pitch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownKp_Pitch)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 105);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Yaw";
            // 
            // textIMUPitch
            // 
            this.textIMUPitch.Enabled = false;
            this.textIMUPitch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textIMUPitch.Location = new System.Drawing.Point(61, 72);
            this.textIMUPitch.Margin = new System.Windows.Forms.Padding(2);
            this.textIMUPitch.Name = "textIMUPitch";
            this.textIMUPitch.Size = new System.Drawing.Size(70, 26);
            this.textIMUPitch.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 75);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Pitch";
            // 
            // textIMURoll
            // 
            this.textIMURoll.Enabled = false;
            this.textIMURoll.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textIMURoll.Location = new System.Drawing.Point(61, 42);
            this.textIMURoll.Margin = new System.Windows.Forms.Padding(2);
            this.textIMURoll.Name = "textIMURoll";
            this.textIMURoll.Size = new System.Drawing.Size(70, 26);
            this.textIMURoll.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(21, 45);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Roll";
            // 
            // buttonStart
            // 
            this.buttonStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStart.Location = new System.Drawing.Point(18, 249);
            this.buttonStart.Margin = new System.Windows.Forms.Padding(2);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(145, 27);
            this.buttonStart.TabIndex = 6;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStop.Location = new System.Drawing.Point(18, 289);
            this.buttonStop.Margin = new System.Windows.Forms.Padding(2);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(145, 28);
            this.buttonStop.TabIndex = 7;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // textIMUYaw
            // 
            this.textIMUYaw.Enabled = false;
            this.textIMUYaw.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textIMUYaw.Location = new System.Drawing.Point(61, 102);
            this.textIMUYaw.Margin = new System.Windows.Forms.Padding(2);
            this.textIMUYaw.Name = "textIMUYaw";
            this.textIMUYaw.Size = new System.Drawing.Size(70, 26);
            this.textIMUYaw.TabIndex = 9;
            // 
            // comboBoxComPort
            // 
            this.comboBoxComPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxComPort.FormattingEnabled = true;
            this.comboBoxComPort.Location = new System.Drawing.Point(64, 172);
            this.comboBoxComPort.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxComPort.Name = "comboBoxComPort";
            this.comboBoxComPort.Size = new System.Drawing.Size(100, 28);
            this.comboBoxComPort.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(14, 175);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 20);
            this.label6.TabIndex = 19;
            this.label6.Text = "COM";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxcmdThrolttle);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.textBoxcmdYaw);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.textBoxcmdRoll);
            this.groupBox1.Controls.Add(this.textBoxcmdPitch);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.Desktop;
            this.groupBox1.Location = new System.Drawing.Point(18, 10);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(151, 149);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Command";
            // 
            // textBoxcmdThrolttle
            // 
            this.textBoxcmdThrolttle.AcceptsReturn = true;
            this.textBoxcmdThrolttle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxcmdThrolttle.Location = new System.Drawing.Point(70, 27);
            this.textBoxcmdThrolttle.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxcmdThrolttle.Name = "textBoxcmdThrolttle";
            this.textBoxcmdThrolttle.Size = new System.Drawing.Size(76, 26);
            this.textBoxcmdThrolttle.TabIndex = 28;
            this.textBoxcmdThrolttle.Text = "0";
            this.textBoxcmdThrolttle.UseWaitCursor = true;
            this.textBoxcmdThrolttle.Click += new System.EventHandler(this.textBoxcmdThrolttle_Click);
            this.textBoxcmdThrolttle.Enter += new System.EventHandler(this.textBoxcmdThrolttle_Enter);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(32, 117);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(36, 20);
            this.label10.TabIndex = 3;
            this.label10.Text = "Roll";
            // 
            // textBoxcmdYaw
            // 
            this.textBoxcmdYaw.Enabled = false;
            this.textBoxcmdYaw.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxcmdYaw.Location = new System.Drawing.Point(70, 54);
            this.textBoxcmdYaw.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxcmdYaw.Name = "textBoxcmdYaw";
            this.textBoxcmdYaw.Size = new System.Drawing.Size(76, 26);
            this.textBoxcmdYaw.TabIndex = 27;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(24, 86);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 20);
            this.label9.TabIndex = 2;
            this.label9.Text = "Pitch";
            // 
            // textBoxcmdRoll
            // 
            this.textBoxcmdRoll.Enabled = false;
            this.textBoxcmdRoll.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxcmdRoll.Location = new System.Drawing.Point(70, 113);
            this.textBoxcmdRoll.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxcmdRoll.Name = "textBoxcmdRoll";
            this.textBoxcmdRoll.Size = new System.Drawing.Size(76, 26);
            this.textBoxcmdRoll.TabIndex = 26;
            // 
            // textBoxcmdPitch
            // 
            this.textBoxcmdPitch.Enabled = false;
            this.textBoxcmdPitch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxcmdPitch.Location = new System.Drawing.Point(70, 84);
            this.textBoxcmdPitch.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxcmdPitch.Name = "textBoxcmdPitch";
            this.textBoxcmdPitch.Size = new System.Drawing.Size(76, 26);
            this.textBoxcmdPitch.TabIndex = 25;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(23, 57);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 20);
            this.label8.TabIndex = 1;
            this.label8.Text = "Yaw";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(7, 31);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 20);
            this.label7.TabIndex = 0;
            this.label7.Text = "Throttle";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxYawRate);
            this.groupBox2.Controls.Add(this.textBoxRollRate);
            this.groupBox2.Controls.Add(this.textBoxPitchRate);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.textIMUPitch);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.textIMURoll);
            this.groupBox2.Controls.Add(this.textIMUYaw);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(181, 11);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(216, 156);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Feedback";
            // 
            // textBoxYawRate
            // 
            this.textBoxYawRate.Enabled = false;
            this.textBoxYawRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxYawRate.Location = new System.Drawing.Point(135, 102);
            this.textBoxYawRate.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxYawRate.Name = "textBoxYawRate";
            this.textBoxYawRate.Size = new System.Drawing.Size(70, 26);
            this.textBoxYawRate.TabIndex = 12;
            // 
            // textBoxRollRate
            // 
            this.textBoxRollRate.Enabled = false;
            this.textBoxRollRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxRollRate.Location = new System.Drawing.Point(135, 42);
            this.textBoxRollRate.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxRollRate.Name = "textBoxRollRate";
            this.textBoxRollRate.Size = new System.Drawing.Size(70, 26);
            this.textBoxRollRate.TabIndex = 11;
            // 
            // textBoxPitchRate
            // 
            this.textBoxPitchRate.Enabled = false;
            this.textBoxPitchRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPitchRate.Location = new System.Drawing.Point(135, 72);
            this.textBoxPitchRate.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxPitchRate.Name = "textBoxPitchRate";
            this.textBoxPitchRate.Size = new System.Drawing.Size(70, 26);
            this.textBoxPitchRate.TabIndex = 10;
            // 
            // textBoxUImessage
            // 
            this.textBoxUImessage.Enabled = false;
            this.textBoxUImessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxUImessage.HideSelection = false;
            this.textBoxUImessage.Location = new System.Drawing.Point(814, 25);
            this.textBoxUImessage.Multiline = true;
            this.textBoxUImessage.Name = "textBoxUImessage";
            this.textBoxUImessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxUImessage.Size = new System.Drawing.Size(228, 306);
            this.textBoxUImessage.TabIndex = 24;
            // 
            // checkBoxPlotForm
            // 
            this.checkBoxPlotForm.AutoSize = true;
            this.checkBoxPlotForm.Checked = true;
            this.checkBoxPlotForm.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxPlotForm.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxPlotForm.Location = new System.Drawing.Point(44, 214);
            this.checkBoxPlotForm.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxPlotForm.Name = "checkBoxPlotForm";
            this.checkBoxPlotForm.Size = new System.Drawing.Size(96, 24);
            this.checkBoxPlotForm.TabIndex = 25;
            this.checkBoxPlotForm.Text = "Plot Form";
            this.checkBoxPlotForm.UseVisualStyleBackColor = true;
            this.checkBoxPlotForm.CheckedChanged += new System.EventHandler(this.checkBoxPlotForm_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBoxYawPIDout);
            this.groupBox3.Controls.Add(this.textBoxPitchPIDout);
            this.groupBox3.Controls.Add(this.textBoxYawReadBack);
            this.groupBox3.Controls.Add(this.textBoxPitchReadBack);
            this.groupBox3.Controls.Add(this.textBoxRollReadBack);
            this.groupBox3.Controls.Add(this.textBoxRollPIDout);
            this.groupBox3.Controls.Add(this.textBoxYawError);
            this.groupBox3.Controls.Add(this.label22);
            this.groupBox3.Controls.Add(this.label21);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.textBoxPitchError);
            this.groupBox3.Controls.Add(this.textBoxRollError);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.SystemColors.Desktop;
            this.groupBox3.Location = new System.Drawing.Point(401, 11);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(313, 152);
            this.groupBox3.TabIndex = 26;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Control";
            // 
            // textBoxYawPIDout
            // 
            this.textBoxYawPIDout.Enabled = false;
            this.textBoxYawPIDout.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxYawPIDout.Location = new System.Drawing.Point(223, 107);
            this.textBoxYawPIDout.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxYawPIDout.Name = "textBoxYawPIDout";
            this.textBoxYawPIDout.Size = new System.Drawing.Size(76, 26);
            this.textBoxYawPIDout.TabIndex = 43;
            this.textBoxYawPIDout.UseWaitCursor = true;
            // 
            // textBoxPitchPIDout
            // 
            this.textBoxPitchPIDout.Enabled = false;
            this.textBoxPitchPIDout.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPitchPIDout.Location = new System.Drawing.Point(143, 108);
            this.textBoxPitchPIDout.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxPitchPIDout.Name = "textBoxPitchPIDout";
            this.textBoxPitchPIDout.Size = new System.Drawing.Size(76, 26);
            this.textBoxPitchPIDout.TabIndex = 41;
            this.textBoxPitchPIDout.UseWaitCursor = true;
            // 
            // textBoxYawReadBack
            // 
            this.textBoxYawReadBack.Enabled = false;
            this.textBoxYawReadBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxYawReadBack.Location = new System.Drawing.Point(223, 47);
            this.textBoxYawReadBack.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxYawReadBack.Name = "textBoxYawReadBack";
            this.textBoxYawReadBack.Size = new System.Drawing.Size(76, 26);
            this.textBoxYawReadBack.TabIndex = 39;
            this.textBoxYawReadBack.UseWaitCursor = true;
            // 
            // textBoxPitchReadBack
            // 
            this.textBoxPitchReadBack.Enabled = false;
            this.textBoxPitchReadBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPitchReadBack.Location = new System.Drawing.Point(143, 47);
            this.textBoxPitchReadBack.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxPitchReadBack.Name = "textBoxPitchReadBack";
            this.textBoxPitchReadBack.Size = new System.Drawing.Size(76, 26);
            this.textBoxPitchReadBack.TabIndex = 38;
            this.textBoxPitchReadBack.UseWaitCursor = true;
            // 
            // textBoxRollReadBack
            // 
            this.textBoxRollReadBack.Enabled = false;
            this.textBoxRollReadBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxRollReadBack.Location = new System.Drawing.Point(63, 47);
            this.textBoxRollReadBack.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxRollReadBack.Name = "textBoxRollReadBack";
            this.textBoxRollReadBack.Size = new System.Drawing.Size(76, 26);
            this.textBoxRollReadBack.TabIndex = 37;
            this.textBoxRollReadBack.UseWaitCursor = true;
            // 
            // textBoxRollPIDout
            // 
            this.textBoxRollPIDout.Enabled = false;
            this.textBoxRollPIDout.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxRollPIDout.Location = new System.Drawing.Point(63, 108);
            this.textBoxRollPIDout.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxRollPIDout.Name = "textBoxRollPIDout";
            this.textBoxRollPIDout.Size = new System.Drawing.Size(76, 26);
            this.textBoxRollPIDout.TabIndex = 36;
            this.textBoxRollPIDout.UseWaitCursor = true;
            // 
            // textBoxYawError
            // 
            this.textBoxYawError.Enabled = false;
            this.textBoxYawError.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxYawError.Location = new System.Drawing.Point(223, 77);
            this.textBoxYawError.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxYawError.Name = "textBoxYawError";
            this.textBoxYawError.Size = new System.Drawing.Size(76, 26);
            this.textBoxYawError.TabIndex = 35;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(219, 25);
            this.label22.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(40, 20);
            this.label22.TabIndex = 34;
            this.label22.Text = "Yaw";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(143, 26);
            this.label21.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(44, 20);
            this.label21.TabIndex = 33;
            this.label21.Text = "Pitch";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(63, 25);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(36, 20);
            this.label13.TabIndex = 32;
            this.label13.Text = "Roll";
            // 
            // textBoxPitchError
            // 
            this.textBoxPitchError.Enabled = false;
            this.textBoxPitchError.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPitchError.Location = new System.Drawing.Point(143, 78);
            this.textBoxPitchError.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxPitchError.Name = "textBoxPitchError";
            this.textBoxPitchError.Size = new System.Drawing.Size(76, 26);
            this.textBoxPitchError.TabIndex = 30;
            // 
            // textBoxRollError
            // 
            this.textBoxRollError.Enabled = false;
            this.textBoxRollError.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxRollError.Location = new System.Drawing.Point(63, 79);
            this.textBoxRollError.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxRollError.Name = "textBoxRollError";
            this.textBoxRollError.Size = new System.Drawing.Size(76, 26);
            this.textBoxRollError.TabIndex = 29;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(16, 112);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 20);
            this.label5.TabIndex = 2;
            this.label5.Text = "PID";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(8, 81);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(44, 20);
            this.label12.TabIndex = 1;
            this.label12.Text = "Error";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(4, 53);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(48, 20);
            this.label14.TabIndex = 0;
            this.label14.Text = "Read";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label20);
            this.groupBox4.Controls.Add(this.numericUpDown1);
            this.groupBox4.Controls.Add(this.numericUpDown2);
            this.groupBox4.Controls.Add(this.numericUpDown3);
            this.groupBox4.Controls.Add(this.label19);
            this.groupBox4.Controls.Add(this.label18);
            this.groupBox4.Controls.Add(this.numericUpDownKi_Roll);
            this.groupBox4.Controls.Add(this.numericUpDownKd_Roll);
            this.groupBox4.Controls.Add(this.numericUpDownKp_Roll);
            this.groupBox4.Controls.Add(this.label17);
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.numericUpDownKi_Pitch);
            this.groupBox4.Controls.Add(this.numericUpDownKd_Pitch);
            this.groupBox4.Controls.Add(this.numericUpDownKp_Pitch);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(181, 172);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(277, 159);
            this.groupBox4.TabIndex = 28;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Gain";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(200, 31);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(40, 20);
            this.label20.TabIndex = 16;
            this.label20.Text = "Yaw";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(195, 85);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(72, 26);
            this.numericUpDown1.TabIndex = 15;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(195, 117);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(72, 26);
            this.numericUpDown2.TabIndex = 14;
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Location = new System.Drawing.Point(195, 54);
            this.numericUpDown3.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(72, 26);
            this.numericUpDown3.TabIndex = 13;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(35, 31);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(44, 20);
            this.label19.TabIndex = 12;
            this.label19.Text = "Pitch";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(123, 31);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(36, 20);
            this.label18.TabIndex = 11;
            this.label18.Text = "Roll";
            // 
            // numericUpDownKi_Roll
            // 
            this.numericUpDownKi_Roll.Location = new System.Drawing.Point(117, 86);
            this.numericUpDownKi_Roll.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownKi_Roll.Name = "numericUpDownKi_Roll";
            this.numericUpDownKi_Roll.Size = new System.Drawing.Size(72, 26);
            this.numericUpDownKi_Roll.TabIndex = 8;
            // 
            // numericUpDownKd_Roll
            // 
            this.numericUpDownKd_Roll.Location = new System.Drawing.Point(117, 119);
            this.numericUpDownKd_Roll.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownKd_Roll.Name = "numericUpDownKd_Roll";
            this.numericUpDownKd_Roll.Size = new System.Drawing.Size(72, 26);
            this.numericUpDownKd_Roll.TabIndex = 7;
            // 
            // numericUpDownKp_Roll
            // 
            this.numericUpDownKp_Roll.Location = new System.Drawing.Point(117, 54);
            this.numericUpDownKp_Roll.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownKp_Roll.Name = "numericUpDownKp_Roll";
            this.numericUpDownKp_Roll.Size = new System.Drawing.Size(72, 26);
            this.numericUpDownKp_Roll.TabIndex = 6;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(8, 124);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(28, 20);
            this.label17.TabIndex = 5;
            this.label17.Text = "Kd";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(8, 88);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(22, 20);
            this.label16.TabIndex = 4;
            this.label16.Text = "Ki";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 52);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(28, 20);
            this.label15.TabIndex = 3;
            this.label15.Text = "Kp";
            this.label15.Click += new System.EventHandler(this.label15_Click);
            // 
            // numericUpDownKi_Pitch
            // 
            this.numericUpDownKi_Pitch.Location = new System.Drawing.Point(39, 88);
            this.numericUpDownKi_Pitch.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownKi_Pitch.Name = "numericUpDownKi_Pitch";
            this.numericUpDownKi_Pitch.Size = new System.Drawing.Size(72, 26);
            this.numericUpDownKi_Pitch.TabIndex = 2;
            this.numericUpDownKi_Pitch.ValueChanged += new System.EventHandler(this.numericUpDownKi_Pitch_ValueChanged);
            // 
            // numericUpDownKd_Pitch
            // 
            this.numericUpDownKd_Pitch.Location = new System.Drawing.Point(40, 119);
            this.numericUpDownKd_Pitch.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownKd_Pitch.Name = "numericUpDownKd_Pitch";
            this.numericUpDownKd_Pitch.Size = new System.Drawing.Size(72, 26);
            this.numericUpDownKd_Pitch.TabIndex = 1;
            // 
            // numericUpDownKp_Pitch
            // 
            this.numericUpDownKp_Pitch.Location = new System.Drawing.Point(39, 56);
            this.numericUpDownKp_Pitch.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownKp_Pitch.Name = "numericUpDownKp_Pitch";
            this.numericUpDownKp_Pitch.Size = new System.Drawing.Size(72, 26);
            this.numericUpDownKp_Pitch.TabIndex = 0;
            // 
            // buttonUpdateStepCommand
            // 
            this.buttonUpdateStepCommand.Location = new System.Drawing.Point(29, 331);
            this.buttonUpdateStepCommand.Name = "buttonUpdateStepCommand";
            this.buttonUpdateStepCommand.Size = new System.Drawing.Size(100, 23);
            this.buttonUpdateStepCommand.TabIndex = 29;
            this.buttonUpdateStepCommand.Text = "Step";
            this.buttonUpdateStepCommand.UseVisualStyleBackColor = true;
            this.buttonUpdateStepCommand.Visible = false;
            this.buttonUpdateStepCommand.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // checkBoxStepCommandOn
            // 
            this.checkBoxStepCommandOn.AutoSize = true;
            this.checkBoxStepCommandOn.Location = new System.Drawing.Point(242, 337);
            this.checkBoxStepCommandOn.Name = "checkBoxStepCommandOn";
            this.checkBoxStepCommandOn.Size = new System.Drawing.Size(98, 17);
            this.checkBoxStepCommandOn.TabIndex = 30;
            this.checkBoxStepCommandOn.Text = "Step Command";
            this.checkBoxStepCommandOn.UseVisualStyleBackColor = true;
            this.checkBoxStepCommandOn.Visible = false;
            // 
            // textBoxStepCommand
            // 
            this.textBoxStepCommand.Location = new System.Drawing.Point(135, 333);
            this.textBoxStepCommand.Name = "textBoxStepCommand";
            this.textBoxStepCommand.Size = new System.Drawing.Size(100, 20);
            this.textBoxStepCommand.TabIndex = 32;
            this.textBoxStepCommand.Text = "0";
            this.textBoxStepCommand.Visible = false;
            // 
            // textBoxLargestNeg
            // 
            this.textBoxLargestNeg.Enabled = false;
            this.textBoxLargestNeg.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLargestNeg.Location = new System.Drawing.Point(334, 397);
            this.textBoxLargestNeg.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxLargestNeg.Name = "textBoxLargestNeg";
            this.textBoxLargestNeg.Size = new System.Drawing.Size(76, 26);
            this.textBoxLargestNeg.TabIndex = 34;
            // 
            // textBoxLargestPos
            // 
            this.textBoxLargestPos.Enabled = false;
            this.textBoxLargestPos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLargestPos.Location = new System.Drawing.Point(424, 397);
            this.textBoxLargestPos.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxLargestPos.Name = "textBoxLargestPos";
            this.textBoxLargestPos.Size = new System.Drawing.Size(76, 26);
            this.textBoxLargestPos.TabIndex = 35;
            // 
            // listCommands
            // 
            this.listCommands.FormattingEnabled = true;
            this.listCommands.Items.AddRange(new object[] {
            "Zero Sensors",
            "ARM",
            "DISARM",
            "Set GPS Home",
            "Write to Flash",
            "SET ACCEL REF",
            "SET MAG REF",
            "ZERO GYROS"});
            this.listCommands.Location = new System.Drawing.Point(464, 181);
            this.listCommands.Name = "listCommands";
            this.listCommands.Size = new System.Drawing.Size(112, 147);
            this.listCommands.TabIndex = 36;
            this.listCommands.SelectedIndexChanged += new System.EventHandler(this.listCommands_SelectedIndexChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.textBox_GPS_SPD);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.textBox_GPS_N);
            this.groupBox5.Controls.Add(this.textBox_GPS_A);
            this.groupBox5.Controls.Add(this.textBox_GPS_E);
            this.groupBox5.Controls.Add(this.label24);
            this.groupBox5.Controls.Add(this.label25);
            this.groupBox5.Controls.Add(this.label26);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.ForeColor = System.Drawing.SystemColors.Desktop;
            this.groupBox5.Location = new System.Drawing.Point(591, 179);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox5.Size = new System.Drawing.Size(183, 152);
            this.groupBox5.TabIndex = 37;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "GPS";
            // 
            // textBox_GPS_SPD
            // 
            this.textBox_GPS_SPD.Enabled = false;
            this.textBox_GPS_SPD.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_GPS_SPD.Location = new System.Drawing.Point(102, 111);
            this.textBox_GPS_SPD.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_GPS_SPD.Name = "textBox_GPS_SPD";
            this.textBox_GPS_SPD.Size = new System.Drawing.Size(76, 26);
            this.textBox_GPS_SPD.TabIndex = 39;
            this.textBox_GPS_SPD.UseWaitCursor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(4, 112);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 20);
            this.label4.TabIndex = 38;
            this.label4.Text = "GND SPD";
            // 
            // textBox_GPS_N
            // 
            this.textBox_GPS_N.Enabled = false;
            this.textBox_GPS_N.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_GPS_N.Location = new System.Drawing.Point(102, 20);
            this.textBox_GPS_N.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_GPS_N.Name = "textBox_GPS_N";
            this.textBox_GPS_N.Size = new System.Drawing.Size(76, 26);
            this.textBox_GPS_N.TabIndex = 37;
            this.textBox_GPS_N.UseWaitCursor = true;
            // 
            // textBox_GPS_A
            // 
            this.textBox_GPS_A.Enabled = false;
            this.textBox_GPS_A.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_GPS_A.Location = new System.Drawing.Point(102, 81);
            this.textBox_GPS_A.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_GPS_A.Name = "textBox_GPS_A";
            this.textBox_GPS_A.Size = new System.Drawing.Size(76, 26);
            this.textBox_GPS_A.TabIndex = 36;
            this.textBox_GPS_A.UseWaitCursor = true;
            // 
            // textBox_GPS_E
            // 
            this.textBox_GPS_E.Enabled = false;
            this.textBox_GPS_E.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_GPS_E.Location = new System.Drawing.Point(102, 51);
            this.textBox_GPS_E.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_GPS_E.Name = "textBox_GPS_E";
            this.textBox_GPS_E.Size = new System.Drawing.Size(76, 26);
            this.textBox_GPS_E.TabIndex = 29;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(48, 84);
            this.label24.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(38, 20);
            this.label24.TabIndex = 2;
            this.label24.Text = "ALT";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(28, 55);
            this.label25.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(58, 20);
            this.label25.TabIndex = 1;
            this.label25.Text = "GPS E";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(28, 23);
            this.label26.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(58, 20);
            this.label26.TabIndex = 0;
            this.label26.Text = "GPS N";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1079, 397);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.listCommands);
            this.Controls.Add(this.textBoxLargestPos);
            this.Controls.Add(this.textBoxLargestNeg);
            this.Controls.Add(this.textBoxStepCommand);
            this.Controls.Add(this.checkBoxStepCommandOn);
            this.Controls.Add(this.buttonUpdateStepCommand);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.checkBoxPlotForm);
            this.Controls.Add(this.textBoxUImessage);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.comboBoxComPort);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.buttonStop);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmMain";
            this.Text = "PSU ME Quadcopter Control";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownKi_Roll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownKd_Roll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownKp_Roll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownKi_Pitch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownKd_Pitch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownKp_Pitch)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textIMUPitch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textIMURoll;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonStop;
        private System.IO.Ports.SerialPort _serialPort;
        private System.Windows.Forms.TextBox textIMUYaw;
        private System.Windows.Forms.ComboBox comboBoxComPort;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxUImessage;
        private System.Windows.Forms.TextBox textBoxcmdThrolttle;
        private System.Windows.Forms.TextBox textBoxcmdYaw;
        private System.Windows.Forms.TextBox textBoxcmdRoll;
        private System.Windows.Forms.TextBox textBoxcmdPitch;
        private System.Windows.Forms.CheckBox checkBoxPlotForm;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.NumericUpDown numericUpDownKi_Roll;
        private System.Windows.Forms.NumericUpDown numericUpDownKd_Roll;
        private System.Windows.Forms.NumericUpDown numericUpDownKp_Roll;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.NumericUpDown numericUpDownKi_Pitch;
        private System.Windows.Forms.NumericUpDown numericUpDownKd_Pitch;
        private System.Windows.Forms.NumericUpDown numericUpDownKp_Pitch;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button buttonUpdateStepCommand;
        private System.Windows.Forms.CheckBox checkBoxStepCommandOn;
        private System.Windows.Forms.TextBox textBoxStepCommand;
        private System.Windows.Forms.TextBox textBoxYawPIDout;
        private System.Windows.Forms.TextBox textBoxPitchPIDout;
        private System.Windows.Forms.TextBox textBoxYawReadBack;
        private System.Windows.Forms.TextBox textBoxPitchReadBack;
        private System.Windows.Forms.TextBox textBoxRollReadBack;
        private System.Windows.Forms.TextBox textBoxRollPIDout;
        private System.Windows.Forms.TextBox textBoxYawError;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBoxPitchError;
        private System.Windows.Forms.TextBox textBoxRollError;
        private System.Windows.Forms.TextBox textBoxLargestNeg;
        private System.Windows.Forms.TextBox textBoxLargestPos;
        private System.Windows.Forms.ListBox listCommands;
        private System.Windows.Forms.TextBox textBoxYawRate;
        private System.Windows.Forms.TextBox textBoxRollRate;
        private System.Windows.Forms.TextBox textBoxPitchRate;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox textBox_GPS_SPD;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_GPS_N;
        private System.Windows.Forms.TextBox textBox_GPS_A;
        private System.Windows.Forms.TextBox textBox_GPS_E;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
    }
}

