using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

namespace IMU
{
    public partial class frmBodePlot : Form
    {
        public frmBodePlot()
        {
            InitializeComponent();
            //CreateGraph();
        }

        Bode myBodeFunction = new Bode();




        
       // public event EventHandler Resize;
        //static int numberOfPoints = 150;

        PointPairList a = new PointPairList();
     
        LineItem BodeCurve;
        



        public void CreateGraph()
        {

           // get a reference to the GraphPane
            GraphPane myPane = this.zedGraphControlBode.GraphPane;
            a = this.myBodeFunction.bode(10, 20, 10, 10);
          
            zedGraphControlBode.Invalidate();
            zedGraphControlBode.AxisChange();



            // Generate a red curve with diamond
            // symbols, and "Porsche" in the legend

            // Generate a blue curve with circle
            // symbols, and "Piper" in the legend
             BodeCurve = myPane.AddCurve("Command", this.a, Color.Blue, SymbolType.None);
             this.BodeCurve.Line.Width = 3.0F;
          
            // Tell ZedGraph to refigure the`
            // axes since the data have changed
             myPane.XAxis.Type = AxisType.Linear;

            // myPane.XAxis.Scale.Format = "mm:ss"; // 24 hour clock for HH

             myPane.XAxis.MajorGrid.IsVisible = true;

            zedGraphControlBode.AxisChange();
        }


        // SetSize() is separate from Resize() so we can 
        // call it independently from the Form1_Load() method
        // This leaves a 10 px margin around the outside of the control
        // Customize this to fit your needs
        private void SetSize()
        {
            zedGraphControlBode.Location = new Point(10, 10);
            // Leave a small margin around the outside of the control
            zedGraphControlBode.Size = new Size(ClientRectangle.Width - 20, ClientRectangle.Height - 20);
        }

        private void frmBodePlot_Load(object sender, EventArgs e)
        {
            // Setup the graph
            CreateGraph();
            // Size the control to fill the form with a margin
            SetSize();
        }

        //private void frmPlotChart_ResizeBegin(object sender, EventArgs e)
        //{
        //    SetSize();
        //}
    }
}
