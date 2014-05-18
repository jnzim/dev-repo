/*
 * AxisData.h
 *
 * Created: 10/20/2013 5:04:38 PM
 *  Author: Justin
 */ 


#ifndef AXISDATA_H_
#define AXISDATA_H_
#define PACKETSIZE_PLUSHEAD 12

typedef struct Axis
{
	
	int16_t Kp_attitude, Ki_attitude, Kd_attitude;
	int16_t attitude_feedback;
	int16_t attitude_command;
	int16_t attitude_pid_out;
	int16_t attitude_error;
	int16_t attitude_windupGuard;
	
	int16_t Kp_rate, Ki_rate, Kd_rate;
	int16_t rate_feedback;
	int16_t rate_command;
	int16_t rate_pid_out;
	int16_t rate_error;
	int16_t rate_windupGuard;
	int16_t previousRateError0;
	
	int16_t attitude_total_error;
	int16_t rate_total_error;
	int16_t previousError0;
	int16_t previousError1;
	int16_t previousError2;
	int16_t previousPosition0;
	int16_t previousPosition1;
	int16_t previousPosition2;
	int16_t thrust;
	
} Axis_t ;

typedef struct SystemData
{
	
	float temperature;
	float GPS_Longitude;
	int32_t GPS_Lattitude;
	float GPS_Altitude;
	float GPS_Position_N;
	float GPS_Position_E;
	float GPS_Position_H;
	float GPS_Course_Speed;
		
	

} System_data_t;

uint8_t commandPacket[PACKETSIZE_PLUSHEAD];


#endif /* AXISDATA_H_ */