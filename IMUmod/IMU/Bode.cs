using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZedGraph;
namespace IMU
{
    class Bode
    {

        PointPairList sinList = new PointPairList();
        //PointPairList sinListSet = new PointPairList();
        public PointPairList bode(double startFrequency, double endFrequency, double numberOfFrequency, double numberOfCyclesPerFrequency)
        {
          
            double f,y,t,A;
            A = 1;
            f = startFrequency;  //Hz
            double resoltion = .002; //Sec, decrease for large frequency
            for (f = startFrequency; f <= endFrequency; f = f + 5)
            {
                double numberOfPoints = numberOfCyclesPerFrequency / f / resoltion;

                for (int i = 0; i < numberOfPoints + 1; i++)
                {
                    t = (double)i * resoltion;
                    y = A * Math.Sin(2 * Math.PI * t * f);
                    this.sinList.Add(t, y);   
                }      
            }
            return sinList;
        }


        void compileList()
        {


        }

    }
}
