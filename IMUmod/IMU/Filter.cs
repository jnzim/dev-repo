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
        private double alpha, gama;
        private double x_bar_k_minus_1, x_bar_k;



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






/***********************************************************************************************************
INPUT:
OUTPUT:
DISCRIPTION:  Low pass filter.  There a bug here in the break frequency calculation.
The filter keeps track of the previous sample require for the recursive calculation.
*********************************************************************************************************** */
public double lowPassFilter(short currentSample)
{
	
	int Ts = 1;
    //set up the breakd frequency
	double Tau = 1.0/(1*2.0*Math.PI);			//.9 is the break frequency.... somehow?  need to check the conversion .9Hz does not seem to be correct
	double alpha = Tau/(Tau + Ts);				//.25
	double gama = 1-alpha;						//.75

    //  filter current sample
	x_bar_k =  (alpha  * (double)x_bar_k_minus_1) +  (gama*(currentSample));
    //  save the current sample for the next filter calculation
	x_bar_k_minus_1 = x_bar_k;

	return x_bar_k;

}




  
    }
}