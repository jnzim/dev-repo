namespace IMU
{
    partial class frmBodePlot
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
            this.components = new System.ComponentModel.Container();
            this.zedGraphControlBode = new ZedGraph.ZedGraphControl();
            this.SuspendLayout();
            // 
            // zedGraphControlBode
            // 
            this.zedGraphControlBode.Location = new System.Drawing.Point(36, 1);
            this.zedGraphControlBode.Margin = new System.Windows.Forms.Padding(4);
            this.zedGraphControlBode.Name = "zedGraphControlBode";
            this.zedGraphControlBode.ScrollGrace = 0D;
            this.zedGraphControlBode.ScrollMaxX = 0D;
            this.zedGraphControlBode.ScrollMaxY = 0D;
            this.zedGraphControlBode.ScrollMaxY2 = 0D;
            this.zedGraphControlBode.ScrollMinX = 0D;
            this.zedGraphControlBode.ScrollMinY = 0D;
            this.zedGraphControlBode.ScrollMinY2 = 0D;
            this.zedGraphControlBode.Size = new System.Drawing.Size(773, 438);
            this.zedGraphControlBode.TabIndex = 20;
            // 
            // frmBodePlot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 465);
            this.Controls.Add(this.zedGraphControlBode);
            this.Name = "frmBodePlot";
            this.Text = "Bode Plot";
            this.Load += new System.EventHandler(this.frmBodePlot_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ZedGraph.ZedGraphControl zedGraphControlBode;
    }
}