/*
 * main.h
 *
 * Created: 3/2/2014 10:28:10 AM
 *  Author: Justin
 */ 


#ifndef MAIN_H_
#define MAIN_H_


PID_data rollAxis,yawAxis,pitchAxis,throttleAxis;


#define PI 3.14159265f
#define rtoDeg 57.2957795

#define START_COMMAND_YEI		0xF6
#define EULER_YEI				0x01
#define GYRO_NORMILIZED_YEI		0x21
#define DUMMY_READY_YEI			0xFF
#define END_PACKET_CHAR			0xCC


/*! Define that selects the UASRT used in example. */
#define F_CPU 					32000000UL
#define XBEE_USART				USARTD1
#define	IMU_USART				USARTC1
#define DMA_RX_Channel			&DMA.CH1
#define MASK_TOP_BYTE			0x00FF
#define NUM_CMD_BYTES		    32
#define SCALE_THROTTLE			4
#define END_PACKET_CHAR			0xCC
#define upperByte16(x) (MASK_TOP_BYTE & (x >> 8))		// get the top 8 bits of a 16 bit word
#define lowerByte16(x) (MASK_TOP_BYTE & x)				// get the lower 8 bits of a 16 bit word
#define byte_4_32(x) (x >> 24)							// get the TOP 8 bits of a 16 bit word
#define byte_3_32(x) (x >> 16)							// get  bits of MIDDLE TOP a 16 bit word
#define byte_2_32(x) (x >> 8)							// get the top MIDDLE BOTTOM bits of a 16 bit word
#define byte_1_32(x) (x)								// get the BOTTOM 8 bits of a 16 bit word
#define tbi(x,y) x ^= _BV(y)
#define SCALE_GYRO_TO_INT_DPS 469.36709353f

/*  IMU commands */
/*! \brief The number of test data bytes. */
#define NUM_BYTES				2

#define READ_COMMAND			0x00
#define DUMMY_READ				0x00
#define WRITE_COMMAND			0x01
//UM6commands
#define READ_UM6_SW_REV			0xAA
#define UM6_GYRO_PROC_XY		0x5C
#define UM6_GYRO_PROC_Z			0x5D
#define UM6_EULER_PHI_THETA		0x62		// returns roll and pitch as 16 bit signed integers
#define UM6_EULER_PSI			0x63
#define ZERO_GYROS				0xAC
#define SET_ACCEL_REF			0xAF
#define SET_MAG_REF				0xB0
#define UM6_RESET_EKF			0XAD
#define UM6_MAG_RAW_XY			0x5A
#define UM6_MAG_RAW_Z			0x5B
#define UM6_GPS_POSITION_N		0x7A
#define UM6_GPS_POSITION_E		0x7B
#define UM6_GPS_POSITION_H 		0x7C
#define UM6_GPS_COURSE_SPEED	0x7D
#define UM6_GPS_ALTITUDE		0x79

///states
#define SYSTEM_STATE_STARTUP	0x0000
#define SYSTEM_ZERO				0x0001
#define SYSTEM_ARM				0x0002
#define SYSTEM_STATE_FLY		0x0004
#define SYSTEM_DISARM			0x0005

#define CONVERT_GYRO_REG_TO_DPS 16		//  1/ 0.0610352  ~ 16.34  ~16
#define  CONVERT_EULER_TO_DEG   91		// 1/ 0.0109863 = 91.002 ~ 91



#endif /* MAIN_H_ */

///***********************************************************************************************************
//INPUT:
//OUTPUT:
//DISCRIPTION:   Read in data from the IMU.  The data are float so the should be read in and converted to
//int16.
//*********************************************************************************************************** */
//void UpdateEulerAngles_YEI()
//{
//
//test.fp = PI/180 * 10;
//uint16_t uSecDelay =2;
//PORTF.OUTCLR = PIN4_bm;
//
////PORTA.OUTTGL = 0b00000001;
//dummy_read = spiIMU_write_read(0xF6);
//dummy_read = spiIMU_write_read(0x01);
//
//while(dummy_read != 0x01)
//{
//
//dummy_read = spiIMU_write_read(0xFF);
//delay_us(uSecDelay);
//}
//
////delay_us(uSecDelay);
////PORTA.OUTTGL = 0b00000001;
//////put_USART_PC_char(dummy_read);
////PORTA.OUTTGL = 0b00000001;
////dummy_read = spiIMU_write_read(0xFF);
////delay_us(uSecDelay);
//////put_USART_PC_char(dummy_read);
////dummy_read = spiIMU_write_read(0xFF);
////delay_us(uSecDelay);
//////put_USART_PC_char(dummy_read);
////dummy_read = spiIMU_write_read(0xFF);
////delay_us(uSecDelay);
////put_USART_PC_char(dummy_read);
////dummy_read = spiIMU_write_read(0xFF);
////delay_us(uSecDelay);
//////put_USART_PC_char(dummy_read);
////dummy_read = spiIMU_write_read(0xFF);
////delay_us(uSecDelay);
//////put_USART_PC_char(dummy_read);
////dummy_read = spiIMU_write_read(0xFF);
////delay_us(uSecDelay);
//////put_USART_PC_char(dummy_read);
////dummy_read = spiIMU_write_read(0xFF);
////delay_us(uSecDelay);
//////put_USART_PC_char(dummy_read);
////dummy_read = spiIMU_write_read(0xFF);
////delay_us(uSecDelay);
//////put_USART_PC_char(dummy_read);
////dummy_read = spiIMU_write_read(0xFF);
////delay_us(uSecDelay);
//////put_USART_PC_char(dummy_read);
////dummy_read = spiIMU_write_read(0xFF);
////put_USART_PC_char(dummy_read);
////delay_us(uSecDelay);
//////PORTA.OUTTGL = 0b00000001;
//
//p.b[3] = spiIMU_write_read(0xFF);
//put_USART_PC_char(p.b[3]);
//p.b[2] = spiIMU_write_read(0xFF);
//put_USART_PC_char(p.b[2]);
//p.b[1] = spiIMU_write_read(0xFF);
//put_USART_PC_char(p.b[1]);
//p.b[0] = spiIMU_write_read(0xFF);
//put_USART_PC_char(p.b[0]);
//
////p.b[3] = test.b[3];
////p.b[2] = test.b[2];
////p.b[1] = test.b[1];
////p.b[0] = test.b[0];
//
//y.b[3] = spiIMU_write_read(0xFF);
//put_USART_PC_char(y.b[3]);
//y.b[2] = spiIMU_write_read(0xFF);
//put_USART_PC_char(y.b[2]);
//y.b[1] = spiIMU_write_read(0xFF);
//put_USART_PC_char(y.b[1] );
//y.b[0] = spiIMU_write_read(0xFF);
//put_USART_PC_char(y.b[0]);
//
//r.b[3] = spiIMU_write_read(0xFF);
//put_USART_PC_char(r.b[3]);
//r.b[2] = spiIMU_write_read(0xFF);
//put_USART_PC_char(r.b[2]);
//r.b[1] = spiIMU_write_read(0xFF);
//put_USART_PC_char(r.b[1] );
//r.b[0] = spiIMU_write_read(0xFF);
//put_USART_PC_char(r.b[0]);
//
//
//PORTF.OUTSET = PIN4_bm;
////put_USART_PC_char(0xCC);
////put_USART_PC_char(r.b[0]);
////put_USART_PC_char(r.b[1]);
////put_USART_PC_char(r.b[2]);
////put_USART_PC_char(r.b[3]);
//
//put_USART_PC_char(0x00);
////put_USART_PC_char(0x00);
////put_USART_PC_char(0x00);
////put_USART_PC_char(0x00);
//
////pitchAxis.attitude_feedback = (int16_t)(p.fp*10000);
//pitchAxis.attitude_feedback = (int16_t)((p.fp*180/PI)+0.5);;
//yawAxis.attitude_feedback = (int16_t)((y.fp*180/PI)+0.5);
////yawAxis.attitude_feedback = ((94));
//rollAxis.attitude_feedback = (int16_t)((r.fp*180/PI)+0.5);
////sendData_int16_t(yawAxis.attitude_feedback);
//
////sendData_int16_t(yawAxis.attitude_feedback);
////sendUM6_Data();
////char * s = (char *) &y;
////put_USART_PC_char(0xCC);
////put_USART_PC_char(s[3]);
////put_USART_PC_char(s[2]);
////put_USART_PC_char(s[1]);
////put_USART_PC_char(s[0]);
//////
////sendData_int16_t();
//
//}
//