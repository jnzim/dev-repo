/*
 * systemData.h
 *
 * Created: 3/1/2014 7:40:36 PM
 *  Author: Justin
 */ 
/*
 * systemData.h
 *
 * Created: 3/1/2014 12:19:05 PM
 *  Author: Justin
 */ 

#ifndef SYSTEMDATA_H_
#define SYSTEMDATA_H_


 #include <stdbool.h>




typedef struct PID_data
{
	int16_t rate_integral, attitude_integral;
	int16_t p_term_rate, i_term_rate, d_term_rate;
	int16_t p_term_attitude, i_term_attitude, d_term_attitude;
	int16_t Kp_rate, Ki_rate, Kd_rate;
	int16_t Kp, Ki, Kd;
	int16_t pid_total;
	int16_t attitude_feedback;
	int16_t rate_feedback;
	int16_t attitude_command;
	int16_t attitude_pid_out;
	int16_t rate_pid_out;
	int16_t pid_out;
	int16_t attitude_error;
	int16_t rate_error;
	int16_t attitude_slope;
	int16_t windupGuard;
	int16_t attitude_total_error;
	int16_t rate_total_error;
	int16_t previousError0;
	int16_t previousError1;
	int16_t previousError2;
	int16_t previousRateError0;
	int16_t previousRateError1;
	int16_t previousRateError2;
	int16_t previousPosition0;
	int16_t previousPosition1;
	int16_t previousPosition2;
	int16_t thrust;
	int16_t attitude_loop_out;

	
} PID_data;


typedef struct SystemData
{

	float temperature;
	float GPS_Longitude;
	float GPS_Lattitude;
	float GPS_Altitude;
	float GPS_Position_N;
	float GPS_Position_E;
	float GPS_Position_H;
	float GPS_Course_Speed;
	
} SystemData;




#endif /* SYSTEMDATA_H_ */


///***********************************************************************************************************
//INPUT:
//OUTPUT:
//DISCRIPTION:   Read in data from the IMU.  The data are float so the should be read in and converted to
//int16.
//*********************************************************************************************************** */
//void UpdateEulerAngles_YEI()
//{
//
//
	//PORTF.OUTCLR = PIN4_bm;
//
//
	//PORTA.OUTTGL = 0b00000001;
	//dummy_read = spiIMU_write_read(0xF6);
	//dummy_read = spiIMU_write_read(0x01);
//
//
//
	//put_USART_PC_char(dummy_read);
	//dummy_read = spiIMU_write_read(0xFF);
	//put_USART_PC_char(dummy_read);
	//dummy_read = spiIMU_write_read(0xFF);
	//put_USART_PC_char(dummy_read);
	//dummy_read = spiIMU_write_read(0xFF);
	//put_USART_PC_char(dummy_read);
	//dummy_read = spiIMU_write_read(0xFF);
	//put_USART_PC_char(dummy_read);
	//dummy_read = spiIMU_write_read(0xFF);
	//put_USART_PC_char(dummy_read);
	//dummy_read = spiIMU_write_read(0xFF);
	//put_USART_PC_char(dummy_read);
	//dummy_read = spiIMU_write_read(0xFF);
	//put_USART_PC_char(dummy_read);
	//dummy_read = spiIMU_write_read(0xFF);
	//put_USART_PC_char(dummy_read);
	//dummy_read = spiIMU_write_read(0xFF);
	//put_USART_PC_char(dummy_read);
	//dummy_read = spiIMU_write_read(0xFF);
	//put_USART_PC_char(dummy_read);
	//PORTA.OUTTGL = 0b00000001;
	//
	//p.b[0] = spiIMU_write_read(0xFF);
	//put_USART_PC_char(p.b[0]);
	//p.b[1] = spiIMU_write_read(0xFF);
	//put_USART_PC_char(p.b[1]);
	//p.b[2] = spiIMU_write_read(0xFF);
	//put_USART_PC_char(p.b[2] );
	//p.b[3] = spiIMU_write_read(0xFF);
	//put_USART_PC_char(p.b[3]);
//
	//y.b[0] = spiIMU_write_read(0xFF);
	//put_USART_PC_char(y.b[0]);
	//y.b[1] = spiIMU_write_read(0xFF);
	//put_USART_PC_char(y.b[1]);
	//y.b[2] = spiIMU_write_read(0xFF);
	//put_USART_PC_char(y.b[2] );
	//y.b[3] = spiIMU_write_read(0xFF);
	//put_USART_PC_char(y.b[3]);
//
	//r.b[0] = spiIMU_write_read(0xFF);
	//put_USART_PC_char(r.b[0]);
	//r.b[1] = spiIMU_write_read(0xFF);
	//put_USART_PC_char(r.b[1]);
	//r.b[2] = spiIMU_write_read(0xFF);
	//put_USART_PC_char(r.b[2] );
	//r.b[3] = spiIMU_write_read(0xFF);
	//put_USART_PC_char(r.b[3]);
	//
//
	////
	//PORTF.OUTSET = PIN4_bm;
	////put_USART_PC_char(0xCC);
	////put_USART_PC_char(r.b[0]);
	////put_USART_PC_char(r.b[1]);
	////put_USART_PC_char(r.b[2]);
	////put_USART_PC_char(r.b[3]);
	////
	//pitchAxis.attitude_feedback = (int16_t)((p.fp*180/PI)+0.5);
	//yawAxis.attitude_feedback = (int16_t)((y.fp*180/PI)+0.5);
	//rollAxis.attitude_feedback = (int16_t)((r.fp*180/PI)+0.5);
	////put_USART_PC_char(pitchAxis.attitude_feedback);
	////sendUM6_Data();
//}
