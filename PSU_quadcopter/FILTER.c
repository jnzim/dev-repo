/*
 * FILTER.c
 *
 * Created: 4/1/2014 8:56:02 PM
 *  Author: Justin
 */ 

#include <avr/io.h>

#include "FILTER.h"
#include "systemData.h"

float alpha;
float gama;
float x_bar_k_minus_1;
float x_bar_k;


/***********************************************************************************************************
INPUT:
OUTPUT:
DISCRIPTION:  Low pass filter.  There a bug here in the break frequency calculation.
The filter keeps track of the previous sample require for the recursive calculation.
*********************************************************************************************************** */
float lowPassFilter(float currentSample)
{
	
	int Ts = 1;

	float Tau = 1.0/(.9*2.0*PI);				//.9 is the break frequency.... somehow?  need to check the conversion .9Hz does not seem to be correct
	float alpha = Tau/(Tau + Ts);
	float gama = 1-alpha;

	x_bar_k =  (alpha * x_bar_k_minus_1) +  (gama*(currentSample));

	x_bar_k_minus_1 = x_bar_k;

	return x_bar_k;

}












//
//float pitch=0, roll=0;
//
//
//float RwAcc[3]={0, 0, 0}; //Projection of normalized gravitation force vector on x/y/z axis, as measured by accelerometer
//float Gyro_ds[3]={0, 0, 0}; //Gyro readings
//
////void initFilter()
////{
	////
	////float Tau = 1/(2*PI*omega_break);
	////alpha = Tau/(Tau + T_sample);
	////gama = T_sample/(T_sample + Tau);
////}
////
////
//int16_t lowPassFilter(PID_data * pid_data)
//{
	//
	//alpha =0.987591053;
	//gama = 1-alpha;
//
	//
	//
	//x_bar_k = (alpha * x_bar_k_minus_1) + gama *pid_data->rate_feedback;
	//
	//x_bar_k_minus_1 = x_bar_k;
	//
	//return (int16_t)x_bar_k;
	//
//}
//
//
//void ComplementaryFilter(uint16_t accData[3], uint16_t gyrData[3], float *pitch, float *roll)
//{
	//float pitchAcc, rollAcc;
	//uint8_t dt = 1;
	//uint8_t GYROSCOPE_SENSITIVITY = 1;
	//
	//// Integrate the gyroscope data -> int(angularSpeed) = angle
	//*pitch += ((float)gyrData[0] / GYROSCOPE_SENSITIVITY) * dt; // Angle around the X-axis
	//*roll -= ((float)gyrData[1] / GYROSCOPE_SENSITIVITY) * dt;    // Angle around the Y-axis
	//
	//// Compensate for drift with accelerometer data if !bullshit
	//// Sensitivity = -2 to 2 G at 16Bit -> 2G = 32768 && 0.5G = 8192
	//int forceMagnitudeApprox = abs(accData[0]) + abs(accData[1]) + abs(accData[2]);
	//if (forceMagnitudeApprox > 8192 && forceMagnitudeApprox < 32768)
	//{
		//// Turning around the X axis results in a vector on the Y-axis
		//pitchAcc = atan2f((float)accData[1], (float)accData[2]) * 180 / PI;
		//*pitch = *pitch * 0.98 + pitchAcc * 0.02;
		//
		//// Turning around the Y axis results in a vector on the X-axis
		//rollAcc = atan2f((float)accData[0], (float)accData[2]) * 180 / PI;
		//*roll = *roll * 0.98 + rollAcc * 0.02;
	//}
//}
//
//void calc_xy_angles(void)
//{
	//// Using x y and z from accelerometer, calculate x and y angles
	//float x_val, y_val, z_val, result;
	////float x2, y2, z2; //24 bit
//
	////tare accelerations
	//x_val = Ax.fp - XzeroOffSet;
	//y_val = Ay.fp - YzeroOffSet;
	//z_val=  Az.fp - ZzeroOffSet;
	////
	////// calculate  the squares
	////x2 = x_val*x_val;
	////y2 = y_val*y_val;
	////z2 = z_val* z_val;
	////
	//
	////  calculate angles in float and scale to int
	////pitchhat = atan(Az.fp/sqrt(y2+x2));
	//pitchhat = atan2(z_val,y_val);
	//// rollhat = atan(Ax.fp/sqrt(y2+z2));
	//rollhat = atan2(x_val,y_val);
	////pitchAxis.hat = 16383*pitchhat;
	//// rollAxis.hat = 16383*rollhat;
	//
//
	////yawAxis.hat	= (int16_t)16838*atan2(Ax.fp,Ay.fp);
//}
//
//void modifiedKalman()
//{
	//int T = 1;
	//unsigned long interval; //interval since previous analog samples
	//int wGyro = 100;
	//static int i,w;
	//static float tmpf,tmpf2;
	//static unsigned long newMicros; //new timestamp
	//static char signRzGyro;
//
	//float RwAcc[3] = {Az.fp,Ax.fp,Ay.fp};
	//
	//normalize3DVector(RwAcc);
	//
	//if (firstSample)
	//{
		//for(w=0;w<=2;w++)
		//{
			//RwEst[w] = RwAcc[w];    //initialize with accelerometer readings
		//}
		//
	//}
	//else
	//{
		////evaluate RwGyro vector
		//if(abs(RwEst[2]) < 0.1)
		//{
			////Rz is too small and because it is used as reference for computing Axz, Ayz it's error fluctuations will amplify leading to bad results
			////in this case skip the gyro data and just use previous estimate
			//for(w=0;w<=2;w++)
			//{
				//RwGyro[w] = RwEst[w];
			//}
		//}
		//else
		//{
			////get angles between projection of R on ZX/ZY plane and Z axis, based on last RwEst
			//for(w=0;w<=1;w++)
			//{
				//tmpf = getInput(w);								//get current gyro rate in deg/ms
				//tmpf *= interval / 1000.0f;                     //get angle change in deg
				//Awz[w] = atan2(RwEst[w],RwEst[2]) * 180 / PI;   //get angle and convert to degrees
				//Awz[w] += tmpf;                                 //get updated angle according to gyro movement
			//}
			//
			////estimate sign of RzGyro by looking in what qudrant the angle Axz is,
			////RzGyro is positive if  Axz in range -90 ..90 => cos(Awz) >= 0
			//signRzGyro = ( cos(Awz[0] * PI / 180) >=0 ) ? 1 : -1;
			//
			////reverse calculation of RwGyro from Awz angles, for formulas deductions see  http://starlino.com/imu_guide.html
			//for(w=0;w<=1;w++)
			//{
				//RwGyro[0] = sin(Awz[0] * PI / 180);
				//RwGyro[0] /= sqrt( 1 + squared(cos(Awz[0] * PI / 180)) * squared(tan(Awz[1] * PI / 180)) );
				//
				//RwGyro[1] = sin(Awz[1] * PI / 180);
				//RwGyro[1] /= sqrt( 1 + squared(cos(Awz[1] * PI / 180)) * squared(tan(Awz[0] * PI / 180)) );
			//}
			//RwGyro[2] = signRzGyro * sqrt(1 - squared(RwGyro[0]) - squared(RwGyro[1]));
		//}
		//
		////combine Accelerometer and gyro readings
		//for(w=0;w<=2;w++)
		//{
			//RwEst[w] = (RwAcc[w] + wGyro* RwGyro[w]) / (1 + wGyro);
		//}
//
		//normalize3DVector(RwEst);
		//
	//}
	//firstSample = 0;
//}
//
////  calculates the square of number
//float squared(float x)
//{
	//return x*x;
//}
//
////  returns x and y gyro
//float getInput(uint8_t w)
//{
	//if (w = 0)
	//{
		//return pr.fp*180/PI*1000;		// convert from rad / Sec to deg/mSec
	//}
	//if (w = 1)
	//{
		//return rr.fp*180/PI*1000;;		// convert from rad / Sec to deg/mSec
	//}
	//if (w = 2)
	//{
		//return yr.fp*180/PI*1000;;		// convert from rad / Sec to deg/mSec
	//}
//}
//
//void normalize3DVector(float* vector)
//{
	//static float R;
	//R = sqrt(vector[0]*vector[0] + vector[1]*vector[1] + vector[2]*vector[2]);
	//vector[0] /= R;
	//vector[1] /= R;
	//vector[2] /= R;
	//
//
//}
//
//int16_t lowPassFilter(PID_data * pid_data)

//
//
//
////uint16_t CalculateTare()
////{
//////float pitchSum,ySum,zSum= 0;
////_delay_ms(50);
////int i=0;
////int samples=5;
////while (i<=samples)
////{
////i++;
////GetCorrectedData_YEI();
////_delay_ms(100);
////pitchAxis.accelTare += Az.fp;
////rollAxis.accelTare +=Ax.fp;
////yawAxis.accelTare +=Ay.fp;
////}
////
////pitchAxis.accelTare =(pitchAxis.accelTare*16383)/samples;
////rollAxis.accelTare =(rollAxis.accelTare*16383)/samples;
////yawAxis.accelTare =(yawAxis.accelTare*16383)/samples;
////
////return  5; //(uint16_t)((xSum*16383)/(samples));
////}
//
//
//void ComplementaryFilter_2(/*int16_t accX, int16_t accY, int16_t accZ, int16_t pitchRate, int16_t rollRate*/)
//{
	//
	//uint16_t Gain = 13;		//arbitrary  gain
	//float alpha = .9;			// time constant this is applied to the integrate gyro
	//float gama = 1-alpha;		// 1= time constant is applied to the accelerometer to prevent drift
	//
	//// calculate the angular position by integrating the gryo measurement
	//gyroPostionPitchHat += (pr.fp);
	//gyroPostionRollHat += yr.fp;
	//
	//// low pass filter the accelerometer
	//acclPitch = lowPassFilter(pitchhat);
	//acclRoll = lowPassFilter(rollhat);
	//
	////// low pass filter the accelerometer
	////acclPitch = (pitchhat);
	////acclRoll = (rollhat);
	//
	//pitchAxis.attitude_feedback=(uint16_t)(Gain*(gyroPostionPitchHat * alpha  + acclPitch * gama));
	//rollAxis.attitude_feedback=(uint16_t)(Gain*(gyroPostionRollHat * alpha  + acclRoll * gama));
//
//}
//
//
//float CalculateTare()
//{
	//float xSum,ySum,zSum= 0;
	//
	//int i=0;
	//int samples=100;
	//while (i<=samples)
	//{
		//i++;
		//GetCorrectedData_YEI();
		//_delay_ms(10);
		//zSum+= Az.fp;
		//xSum+= Ax.fp;
		//ySum+= Ay.fp;
		//
	//}
	//
	//ZzeroOffSet =zSum/(samples);
	//XzeroOffSet =xSum/(samples);
	//YzeroOffSet =1-(ySum/(samples));
//
//}
//