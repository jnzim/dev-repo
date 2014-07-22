/*
 * pid.h
 *
 * Created: 3/2/2014 6:46:52 AM
 *  Author: Justin
 */ 


#ifndef PID_H_
#define PID_H_


////  pid  position nested rate control loop
void pid_rate(PID_data * pid_data);
void pid_attitude(PID_data *pid_data);
void pid_attitude_rate(PID_data * pid_data);
void pid(PID_data * pid_data);
void PI_rate(PID_data *pid_data);
void PI_attitude_rate(PID_data *pid_data);
void PII_attitude_rate(PID_data *pid_data);




#endif /* PID_H_ */