/*
 * pid.c
 *
 * Created: 3/2/2014 6:47:13 AM
 *  Author: Justin
 */ 
 

#include <stdbool.h>
#include <avr/io.h>
#include <stdlib.h>
#include "systemData.h"
#include "pid.h"

#define PID_MAX_GAIN 500
#define PID_MIN_GAIN -500
#define MAX_TOTAL_ERROR 32766			
#define MIN_TOTAL_ERROR -32767

int16_t p_rate_divisor = 1; 
int16_t i_rate_divisor =100;
int16_t p_attitude_divisor = 50;
int16_t i_attitude_divisor = 100;
int16_t integral_addup_reducer = 10;
int16_t attitude_loop_out;
int16_t attitude_loop_out;
int16_t i_outMin = -3000; 
int16_t i_outMax = 3000;
uint16_t previousIntegral= 0;
//  pid  position control loop
void pid_attitude_rate(PID_data * pid_data);

/*
MATLAB code:
kpa = 10/100;
kia = 5/500;
kpr = 75/100;
kir = 30/1000;
*/



/***********************************************************************************************************
INPUT:
OUTPUT:
DISCRIPTION:  rate loop, this is used to find the rate gains
*********************************************************************************************************** */
void PI_rate(PID_data *pid_data)
{
	
	
	// keep track of the last 3 error samples, this is needed for rate integration
	pid_data->previousRateError0 = pid_data->previousRateError1;
	pid_data->previousRateError1 = pid_data->previousRateError2;
	pid_data->previousRateError2 = pid_data->rate_error;

	// calculate the current rate error
	//pid_data->rate_error = (0 - pid_data->rate_feedback)/10;
	pid_data->rate_error = (pid_data->attitude_command - pid_data->rate_feedback)/10;
	
	// integrate the rate error
	pid_data->rate_integral = pid_data->rate_error/integral_addup_reducer + pid_data->previousRateError0/integral_addup_reducer + 
	pid_data->previousRateError1/integral_addup_reducer + pid_data->previousRateError2/integral_addup_reducer;
	
	pid_data->p_term_rate = (pid_data->rate_error * pid_data->Kp);
	
	pid_data->i_term_rate = (pid_data->rate_integral  * pid_data->Ki);

	
	pid_data->pid_total =pid_data->p_term_rate + pid_data->i_term_rate;
}


/***********************************************************************************************************
INPUT:
OUTPUT:
DISCRIPTION:  nest PI loops, with the rate loop on the inside
*********************************************************************************************************** */
void PI_attitude_rate(PID_data *pid_data)
{
	
	////  save the last error calculation so we can calculate the derivative
	pid_data->previousError0 = pid_data->previousError1;
	pid_data->previousError1 = pid_data->previousError2;
	pid_data->previousError2 = pid_data->attitude_error;

	// calculate error
	pid_data->attitude_error = (pid_data->attitude_command - pid_data->attitude_feedback);
	//pid_data->attitude_error = (pid_data->attitude_command - 0);

	//backward approximation modified
	//pid_data->i_term_attitude +=(pid_data->attitude_error * pid_data->Ki)/300;
		
	////backward approximation modified 
	//pid_data->i_term_attitude =(pid_data->attitude_error * pid_data->Ki)/300 + (pid_data->previousError2 * pid_data->Ki)/300 +
	//(pid_data->previousError1 * pid_data->Ki)/300 + (pid_data->previousError0 * pid_data->Ki)/300;
	

	//Tustin approximation
	pid_data->i_term_attitude += (pid_data->Ki*(pid_data->attitude_error + pid_data->previousError2))/600;          
	
	
	//pid_data->i_term_attitude = pid_data->i_term_attitude/1000;
	//  limit integral wind up
	if(pid_data->i_term_attitude> i_outMax) pid_data->i_term_attitude= i_outMax;
	else if(pid_data->i_term_attitude< i_outMin) pid_data->i_term_attitude= i_outMin;
	
	//calculate attitude p term
	pid_data->p_term_attitude =(pid_data->attitude_error * pid_data->Kp)/10;

	//  sum the p and i term for the attitude controller (outer loop)
	pid_data->attitude_loop_out = (pid_data->p_term_attitude);  // + pid_data->i_term_attitude);

	//  calculate the rate error, the input to the rate loop is the output of the attitude loop
	pid_data->rate_error = (pid_data->attitude_loop_out- pid_data->rate_feedback)/10;

	//  p term for rate loop
	pid_data->p_term_rate = (pid_data->rate_error * pid_data->Kp_rate);

	pid_data->pid_total =pid_data->p_term_rate;

}




/***********************************************************************************************************
INPUT:
OUTPUT:
DISCRIPTION:  nest PI loops inner loop is proportional, outer is PII to increase system type
*********************************************************************************************************** */
void PII_attitude_rate(PID_data *pid_data)
{

	////  save the last error calculation so we can calculate the derivative
	pid_data->previousError0 = pid_data->previousError1;
	pid_data->previousError1 = pid_data->previousError2;
	pid_data->previousError2 = pid_data->attitude_error;
	//pid_data->previousError0 = pid_data->error;
	////  calculate the new error
	////10 - 23 = -13
	
	pid_data->attitude_error = ((pid_data->attitude_command + pid_data->trim) - pid_data->attitude_feedback);
	
	double pidconst1 = (pid_data->Kp)/3 +(pid_data->Ki)/900;
	double pidconst2 = (pid_data->Kp)/900;
	
	pid_data->attitude_loop_out = ((pidconst1 * pid_data->attitude_error) - pidconst2)  /  (pid_data->attitude_error - 2*pid_data->previousError1 + 1 );
 
	pid_data->rate_error = (pid_data->attitude_loop_out- pid_data->rate_feedback)/10;
	
	pid_data->p_term_rate = (pid_data->rate_error * pid_data->Kp_rate);

	pid_data->pid_total =pid_data->p_term_rate;

}
	
	
	
//void stateSpace(PID_data * pid_data)
//{
	//uint16_t k1 = 25.6;
	//uint16_t k2 = 1531;
	//uint16_t k3 = 1098;
	//
	//uint16_t c_out = pid_data->attitude_command - 
	//
	//
	//
//}
	

void pid(PID_data * pid_data)
{
	
	
	int16_t attitude_loop_out;
	////  save the last error calculation so we can calculate the derivative
	pid_data->previousError0 = pid_data->previousError1;
	pid_data->previousError1 = pid_data->previousError2;
	pid_data->previousError2 = pid_data->attitude_error;
	//pid_data->previousError0 = pid_data->error;
	////  calculate the new error
	////10 - 23 = -13
	pid_data->attitude_command = 0;
	pid_data->attitude_error = (pid_data->attitude_command - pid_data->attitude_feedback);
	
	//  calculate the slope (dt = 1)
	//pid_data->attitude_slope = (pid_data->attitude_error - pid_data->previousError2)  + (pid_data->previousError2 - pid_data->previousError1)
	//+ (pid_data->previousError1 - pid_data->previousError0);
	
	//sum the error for the current and last sample (dt = 1)
	pid_data->attitude_total_error = (pid_data->previousError0/integral_addup_reducer + pid_data->previousError1/integral_addup_reducer + pid_data->previousError2/integral_addup_reducer + pid_data->attitude_error/integral_addup_reducer);
	//pid_data->total_error = Limit_value_signed(pid_data->total_error);

	//p_term = Limit_value_signed(((pid_data->error  *  pid_data->Kp)/100), pid_data->error);
	pid_data->p_term_attitude = (pid_data->attitude_error  *  pid_data->Kp);
	pid_data->i_term_attitude = (pid_data->attitude_total_error  *  pid_data->Ki);
	//calculate integral term
	pid_data->pid_total =pid_data->p_term_attitude + pid_data->i_term_attitude;
	
}







void CalculateRate(PID_data * pid_data)
{
	//  this is a rolling set of attitude errors
	pid_data->previousPosition0 = pid_data->previousPosition1;
	pid_data->previousPosition1 = pid_data->previousPosition2;
	pid_data->previousPosition2 = pid_data->attitude_feedback;


	//  calculate the rate, the 
	pid_data->rate_feedback = (pid_data->attitude_feedback - pid_data->previousPosition2)  + (pid_data->previousPosition2 - pid_data->previousPosition1)
	+ (pid_data->previousPosition1 - pid_data->previousPosition0);
	

}




//Limit_value_signed: limit value of signed Variable between Min_Value and Max_Value
//Supply variable Address, Min and Max value within she has to be limited
//Function return new value if limit take place
int16_t Limit_value_signed ( int16_t Variable,int16_t error)
{
	if (error < 0  && Variable < 0)
	{
		Variable = MAX_TOTAL_ERROR;
	}

	if (error < 0  && Variable >0)
	{
		Variable = MIN_TOTAL_ERROR;
	}
	//if (error < 0  && Variable < MIN_TOTAL_ERROR)
	//{
		//Variable = MAX_TOTAL_ERROR;
	//}
	//if (error > 0  && Variable < MIN_TOTAL_ERROR)
	//{
		//Variable = MAX_TOTAL_ERROR;
	//}
	return Variable;
}


////Limit_value_unsigned: limit value of unsigned Variable between Min_Value and Max_Value
////Supply variable Address, Min and Max value within she has to be limited
////Function return new value if limit take place
//int unsigned Limit_value_unsigned ( int Variable,int Min_Value,int Max_Value)
//{
	//if (Variable < Min_Value)
	//{
		//Variable = Min_Value;
	//}
	//if (Variable > Max_Value)
	//{
		//Variable = Max_Value;
	//}
	//return Variable;
//}
//
////GetPError: Calculate proportional Term for Pid algorithm
////Limiting value between Local_Limit_Low and Local_Limit_High
//void  GetPError (int Local_Error, int Local_Limit_Low, int Local_Limit_High)
//{
	//PTerm = (Kp * Local_Error)/10;
	//if (Local_Error > 0)
	//{    
		//if (PTerm < 0) 
		//{ 
			//PTerm = 0x7FFF;
		//}    
	//}
	//if (Local_Error < 0)
	//{   
		 //if (PTerm > 0) 
		 //{ 
			 //PTerm = 0x8000;
		//}    
	//}
	//PTerm = Limit_value_signed (PTerm, Local_Limit_Low , Local_Limit_High);
	
	
//
//
 //
 ////GetIError: Calculate Integral Term for Pid algorythm
 ////Limiting value between Local_Limit_Low and Local_Limit_High
	//void GetIError (int Local_Error, int Local_Limit_Low, int Local_Limit_High)
	//{
		//IInstant = (Ki * Local_Error)/10;
		//if (Local_Error > 0)
		//{
			//if (IInstant < 0)
			//{
				//IInstant = 0x7FFF;
			//}
		//}
		//if (Local_Error < 0)
		//{
			//if (IInstant > 0)
			//{
				//IInstant = 0x8000;
			//}
		//}
		//ITerm = IInstant + ITerm;
		//ITerm = Limit_value_signed (ITerm, Local_Limit_Low, Local_Limit_High);
	//}
 //
 ////GetPError: Calculate Derivative Term for Pid algorythm
 ////Limiting value between Local_Limit_Low and Local_Limit_High
//void GetDError (int Local_Error, int Local_Limit_Low, int Local_Limit_High)
		//{
			//DTerm = ((Local_Error - OldError)*Kd)/10;
			//if ((Local_Error - OldError) > 0)
			//{
				//if (DTerm < 0)
				//{
					//DTerm = 0x7FFF;
				//}
			//}
			//if ((Local_Error - OldError) < 0)
			//{    if (DTerm > 0) { DTerm = 0x8000;}    }
			//OldError = Error;
			//DTerm = Limit_value_signed (DTerm, Local_Limit_Low, Local_Limit_High);
 //}
//
//
//void Pid_Function    (void)
//{
	//if (PIR1bits.TMR1IF == 1)                                //Monitor for 1ms timebase IRQ (TMR1IRQ)
	//{
		//PIR1bits.TMR1IF=0;                                        //Reset IRQ Flag
		//WriteTimer1 (K_TMR1);                                    //Recharge TMR1
		//
		////Update Output-----------
		//
		//
		//
		//Pid_Out = PTerm + ITerm + DTerm;                        //Update out at once to avoid jitter
		//Pid_Out = Limit_value_signed (Pid_Out, b, a);            //Limit Output
		//PutOutput(Pid_Out);                                        //Call output function
		//
		////Update Input, get Error--------
		//Error = Reference - GetFeedback();                            //get Error
		//
		//if (PidCounter == 0)                                    //PID Period Counter Run out?
		//{                                                    //If yes, do Proportional calc
			//GetPError ( Error, -PLimit , PLimit);                //Call function, supplying data
			//PidCounter = PidPeriod;                                //Recharge Period counter
			//if (IPeriod > 0)                                        //Is IPeriod > 0 (ITerm required?)
			//{                                                    //If yes do I calculation
				//if (ICounter == 0)                                        //I Period Counter Run out?
				//{                                                    //If yes, do Integrative calc
					//GetIError ( Error, b , ILimit);                        //Call funtion, supplying data
					//ICounter = IPeriod;                                    //Recharge period counter
				//}
				//else                                                        //If not, decrease I counter
				//{
					//ICounter--;
				//}
			//}
			//else                                                    //If not, then ITerm=0
			//{
				//ITerm = 0;
			//}
			//if (DPeriod > 0)                                        //Is DPeriod > 0 (DTerm required?)
			//{                                                    //If yes do D calculation
				//if (DCounter == 0)                                        //D Period Counter Run out?
				//{                                                    //If yes, do Derivative calc
					//GetDError ( Error, -DLimit , DLimit);                //Call funtion, supplying data
					//DCounter = DPeriod;                                    //Recharge period counter
				//}
				//else
				//{
					//DCounter--;                                            //If not, decrease D counter
				//}
			//}
			//else
			//{
				//DTerm = 0;                                            //If not, then DTerm=0
			//}
		//}
		//else                                                    //If not, decrease Pid counter
		//{
			//PidCounter--;
		//}
	//}
//}


