using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZedGraph;

namespace IMU
{
    class Filter
    {

        // a=tau / (tau + loop time)
        // newAngle = angle measured with atan2 using the accelerometer
        // newRate = angle measured using the gyro
        // looptime = loop time in millis()


        int nRootMean;
        int nScaledRootMean;
        int nAvgCoeff;
        int nMaxVal;



        float tau = 0.075f;
        float a = 0.0f;
        float x_angleC;

        public float complementaryFilter(float newAccelAngle, float newAccelAngleZ, float newGyroRate, int looptime)
        {
            newAccelAngle = (float)Math.Atan2(newAccelAngle, newAccelAngleZ);

            float Ts = 0.0775f;
            a = tau / (tau + Ts);
            x_angleC = a * (x_angleC + newGyroRate * Ts) + (1 - a) * (newGyroRate);
            return x_angleC;
        }


        public void highPassFilter()
        {
            //Digital high pass filters to remove 1.65V DC offset.
            //filteredI = 0.996*( lastFilteredI + (double)(sampleI-lastSampleI) );
        }




  
    }
}