using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace IMU
{
    class clsCommand
    {

        int intAutoCommandCounter;
        double startFrequency, endFrequency, numberOfCyclesPerFrequency;
        double f,A,t, delta_t;

        public int AutoCommandCounter
        {
            get
            {
                return this.intAutoCommandCounter;
            }
            set
            {
                this.intAutoCommandCounter = value;
            }
        }
        public double StartFrequency
        {
            get
            {
                return this.startFrequency;
            }
            set
            {
                this.startFrequency = value;
            }
        }
        public double EndFrequency
        {
            get
            {
                return this.endFrequency;
            }
            set
            {
                this.endFrequency = value;
            }
        }
        public double Delta_T
        {
            get
            {
                return this.delta_t;
            }
            set
            {
                this.delta_t = value;
            }
        }
        public double Amplitude
        {
            get
            {
                return this.A;
            }
            set
            {
                this.A = value;
            }
        }
        public double Frequency
        {
            get
            {
                return this.f;
            }
            set
            {
                this.f = value;
            }
        }
        public double NumberOfCyclesPerFrequency
        {
            get
            {
                return this.numberOfCyclesPerFrequency;
            }
            set
            {
                this.numberOfCyclesPerFrequency = value;
            }
        }



        //private double getFrequency()
        //{
        //    int thisFrequencyCount;



            
        //}



        //public double GetSinSweepCommand()
        //{
        //    double y;
        //    t += delta_t;
        //    getFrequency();
        //    y = A * Math.Sin(2 * Math.PI * t * f);

        //    for (f = startFrequency; f <= endFrequency; f = f + 5)
        //    {
        //        double numberOfPoints = numberOfCyclesPerFrequency / f / this.delta_t;

        //        for (int i = 0; i < numberOfPoints + 1; i++)
        //        {
        //            t = (double)i * this.delta_t;
        //        }
        //    }
        //    return y;
        //}


        public short GetAutoCommand()
        {
            if (this.intAutoCommandCounter < 100)
            {
                return 0;
            }

            else
            {
                return 1500;

            }
        }


        public double GetSinCommand()
        {
            t += delta_t;
            //Debug.WriteLine(t);
            double y;
           
            y = A * Math.Sin(2 * Math.PI * t * f);
            return y;
        }





    }
}
