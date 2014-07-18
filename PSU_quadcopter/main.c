/*
 * PSU_quadcopter.c
 *
 * Created: 3/1/2014 6:03:36 PM
 *  Author: Justin
 */ 

#include <avr/io.h>
#include <stdlib.h>
#include <avr/interrupt.h>
#include <stdbool.h>
#include <math.h>
#include "avr_compiler.h"
#include "systemData.h"
#include "main.h"
#include "uart.h"
#include "pid.h"
#include "PWM.h"
#include "spi.h"
#include "command.h"

#define LEDPORT PORTA


int16_t WriteToPC_SPI();
uint8_t initSystem();
uint8_t zeroSensor();
void ControlLoop();
void init32MHzClock();
void intiLoopTimer();
void sendUM6_Data();
void initIUM();
void SetPulseWidths();
void intiUM6();
void UpdateEulerAngles();
void State();


 void UpdateEulerAngles_YEI_fast();
 void ConvertQuaterionToEuler();
 void  UpdateCorrectedGyroRate_YEI();

uint8_t systemState;
uint8_t dummy_read;
uint16_t int16counter;
int16_t command;
int16_t cmdBytes;	




/***********************************************************************************************************
INPUT:
OUTPUT:
DISCRIPTION: Main is used for set up and then just an infinite loop
*********************************************************************************************************** */
int main()
{
	PORTA.DIRSET = 0x0F;
	systemState = SYSTEM_STATE_STARTUP;
	init32MHzClock();
	initSystem();
	intiLoopTimer();

	
	while(1)
	{
		nop();
	}
}



/**********************************************************************************************************
INPUT:
OUTPUT:
DISCRIPTION:   Determine what to do based on systemState.  Used to initializes and arm the quad.
*********************************************************************************************************** */
void State()
{
	LEDPORT.OUTTGL = PIN0_bm;
	
	
	//UpdateEulerAngles_YEI_fast();
	switch(systemState)
	{
		case SYSTEM_STATE_STARTUP:
			
			
			if (initSystem() ==  1)
			{
				systemState = SYSTEM_ZERO;
			}	
			//LEDPORT.OUTCLR = PIN0_bm;
		break;

		case SYSTEM_ZERO:

			//LEDPORT.OUTTGL = PIN1_bm;
			// read commands from the PC and zero the system as necessary
			// PC transaction, read in command and send out sensor data
			_delay_ms(100);
			UpdateEulerAngles();
		
			//  get the last command sent form the PC, either zero the IMU or get ready to arm the system
			if ((cmdBytes = WriteToPC_SPI()) == SYSTEM_ZERO)
			{
				LEDPORT.OUTSET = PIN1_bm;
				zeroSensor();
				//LEDPORT.OUTCLR = PIN1_bm;
			}

			if (cmdBytes == SYSTEM_ARM)
			{
				systemState = SYSTEM_ARM;
			}

		break;

		case SYSTEM_ARM:
			initPWM();
			systemState = SYSTEM_STATE_FLY;

		break;

		case SYSTEM_STATE_FLY:
			// run the control loop
			LEDPORT.OUTTGL = PIN1_bm;
				if (cmdBytes  == SET_TRIM)
			{
				LEDPORT.OUTTGL = PIN2_bm;
				trim();
			}
			ControlLoop();
			
			
		break;
		
		case 0x0000:
			// The PC is alway outputting a command byte which is zero 0x000
			// when there is no actual command so this is just a 
			// default state
		break;
		
		default:
		break;

	}			// end case
}






/***********************************************************************************************************
INPUT:
OUTPUT:
DISCRIPTION:   Controls execution while the controller  is running.  This function is call on an interrupt
which runs at 300Hz.  The complementary filter can run at 750Hz so we could speed this up in necessary
*********************************************************************************************************** */
void ControlLoop()
{
	
	int16counter++;

	UpdateEulerAngles();

	PI_attitude_rate(&pitchAxis);
	PI_attitude_rate(&yawAxis);
	PI_attitude_rate(&rollAxis);
	
	//PI_rate(&pitchAxis);
	//PI_rate(&yawAxis);
	//PI_rate(&rollAxis);
	//
	SetPulseWidths();
	sendUM6_Data();
	//rollAxis.attitude_command =(int16_t)(GetSinCommand(1500, pitchAxis.Kp));
	//rollAxis.attitude_command =(int16_t)( 2000* AWGN_generator());
	
	//  write to the ATmega com buffer 31*.0033 ~ 100mSec
	if (int16counter >= 31)
	
	{
		cmdBytes = WriteToPC_SPI();	// 400uSec
		int16counter = 0;
		
	}
	

}


/***********************************************************************************************************
INPUT:
OUTPUT:
DISCRIPTION:   Set motor speeds
*********************************************************************************************************** */
void SetPulseWidths()
{
	// check the signs
	if(throttleAxis.thrust > 2000 && throttleAxis.thrust <= 4095)
	{
		doPWM(
		throttleAxis.thrust * SCALE_THROTTLE + rollAxis.pid_total + rollAxis.trim,
		throttleAxis.thrust * SCALE_THROTTLE + pitchAxis.pid_total + pitchAxis.trim,
		throttleAxis.thrust * SCALE_THROTTLE - rollAxis.pid_total - rollAxis.trim,
		throttleAxis.thrust * SCALE_THROTTLE - pitchAxis.pid_total - pitchAxis.trim

		//throttleAxis.thrust * SCALE_THROTTLE + rollAxis.pid_total -  yawAxis.pid_total + rollAxis.trim - yawAxis.trim,
		//throttleAxis.thrust * SCALE_THROTTLE + pitchAxis.pid_total  - yawAxis.pid_total + pitchAxis.trim - yawAxis.trim,
		//throttleAxis.thrust * SCALE_THROTTLE - rollAxis.pid_total + yawAxis.pid_total - rollAxis.trim + yawAxis.trim,
		//throttleAxis.thrust * SCALE_THROTTLE - pitchAxis.pid_total  + yawAxis.pid_total - pitchAxis.trim + - yawAxis.trim
	
		);
		
	}
	else if(throttleAxis.thrust < 2000 || throttleAxis.thrust >= 4095)
	{
		doPWM(0,0,0,0);
	}
}

/***********************************************************************************************************
INPUT:
OUTPUT:
DISCRIPTION:   Default PID gains
*********************************************************************************************************** */
void intPID_gains()
{

		
	//pitchAxis.Kp =6;
	//pitchAxis.Ki =3;
	//pitchAxis.Kp_rate = 4;
	//pitchAxis.Ki_rate =0;
	//
	//
	//yawAxis.Kp = 6;
	//yawAxis.Ki =3;
	//yawAxis.Kp_rate = 4;
	//yawAxis.Ki_rate =0;
	//
	//  standard gains should be used to compare new controllers
	rollAxis.Kp =4;
	rollAxis.Ki =3;
	rollAxis.Kp_rate = 4;
	rollAxis.Ki_rate =0;
	
	//rollAxis.Kp =1;
	//rollAxis.Ki =20;
	//rollAxis.Kp_rate =4;
	//rollAxis.Ki_rate =0;
	
	
	rollAxis.windupGuard = 200;
	pitchAxis.windupGuard = 200;
	yawAxis.windupGuard = 200;
}





/***********************************************************************************************************
INPUT:
OUTPUT:
DISCRIPTION:   Send 16 bit data type over the standard serial port
*********************************************************************************************************** */
void sendUM6_Data()
{

		sendData_int16_t(0xCCCC);					//0xCCCC is the headersendData_int16_t(rollAxis.attitude_feedback);
		sendData_int16_t(rollAxis.attitude_command);
		sendData_int16_t(rollAxis.attitude_feedback);
	

}

/***********************************************************************************************************
  INPUT:
  OUTPUT:
  DISCRIPTION:  Write data packet to the SPI bus connected to the ATmega, 
  the ATmega should be set up to parse this data.
*********************************************************************************************************** */
int16_t WriteToPC_SPI()
{
	PORTE.OUTCLR = PIN4_bm;
	
	throttleAxis.thrust = spiPC_write_read(upperByte16(dummy_read)) << 8;						
	throttleAxis.thrust += spiPC_write_read(lowerByte16(dummy_read));							
	
	//rollAxis.attitude_feedback_15 = throttleAxis.thrust;
	rollAxis.attitude_command = spiPC_write_read(upperByte16(rollAxis.attitude_feedback_15)) << 8;
	rollAxis.attitude_command  += spiPC_write_read(lowerByte16(rollAxis.attitude_feedback_15));
	
	//pitchAxis.attitude_feedback = rollAxis.attitude_command;
	pitchAxis.attitude_command = spiPC_write_read(upperByte16(pitchAxis.attitude_feedback_15)) << 8;
	pitchAxis.attitude_command += spiPC_write_read(lowerByte16(pitchAxis.attitude_feedback_15));
	
	//yawAxis.attitude_feedback = 0;
	yawAxis.attitude_command = spiPC_write_read(upperByte16(yawAxis.attitude_feedback_15)) << 8;
	yawAxis.attitude_command += spiPC_write_read(lowerByte16(yawAxis.attitude_feedback_15));
		
	pitchAxis.Kp = spiPC_write_read(upperByte16(rollAxis.rate_feedback_15 ))<< 8;
	pitchAxis.Kp += spiPC_write_read(lowerByte16(rollAxis.rate_feedback_15 ));							
	
	//pitchAxis.rate_feedback_15 = rollAxis.attitude_command/2;
	pitchAxis.Ki = spiPC_write_read(upperByte16(pitchAxis.rate_feedback_15  )) << 8;					
	pitchAxis.Ki += spiPC_write_read(lowerByte16(pitchAxis.rate_feedback_15 ));							
	
	//yawAxis.rate_feedback = rollAxis.pid_total;
	pitchAxis.Kd= (spiPC_write_read(upperByte16(yawAxis.rate_feedback_15))) << 8;
	pitchAxis.Kd+= spiPC_write_read(lowerByte16(yawAxis.rate_feedback_15));			
	
	//yawAxis.rate_feedback_15 = rollAxis.attitude_command/2;
	command= (spiPC_write_read(upperByte16(yawAxis.rate_feedback_15))) << 8;
	command+= spiPC_write_read(lowerByte16(yawAxis.rate_feedback_15));			
	
	dummy_read = spiPC_write_read(END_PACKET_CHAR);													
	dummy_read = spiPC_write_read(END_PACKET_CHAR);			

	PORTE.OUTSET = PIN4_bm;
	
	//rollAxis.Kp = pitchAxis.Kp;
	//rollAxis.Ki = pitchAxis.Ki;
	//rollAxis.Kd = pitchAxis.Kd;
	//
	//yawAxis.Kp = pitchAxis.Kp;
	//yawAxis.Ki = pitchAxis.Ki;
	//yawAxis.Kd = pitchAxis.Kd;
	
	
	
	return command;
	
	
}





///***********************************************************************************************************
  //INPUT:
  //OUTPUT:
  //DI/SCRIPTION:  Write data packet to the SPI bus connected to the ATmega, 
  //the ATmega should be set up to parse this data.
//*********************************************************************************************************** */
//int16_t WriteToPC_SPI()
//{
	//PORTE.OUTCLR = PIN4_bm;
	//
	//throttleAxis.thrust = spiPC_write_read(upperByte16(dummy_read)) << 8;						
	//throttleAxis.thrust += spiPC_write_read(lowerByte16(dummy_read));							
	//
	////rollAxis.attitude_feedback_15 = throttleAxis.thrust;
	//
	//dummy_read = spiPC_write_read(upperByte16(rollAxis.attitude_feedback_15)) << 8;
	//dummy_read  += spiPC_write_read(lowerByte16(rollAxis.attitude_feedback_15));
	//
	////pitchAxis.attitude_feedback_15 = rollAxis.attitude_command/2;
	////pitchAxis.attitude_feedback_15 = 0;
	//pitchAxis.attitude_command = spiPC_write_read(upperByte16(pitchAxis.attitude_feedback_15)) << 8;
	//pitchAxis.attitude_command += spiPC_write_read(lowerByte16(pitchAxis.attitude_feedback_15));
	//
	////yawAxis.attitude_feedback = 0;
	//yawAxis.attitude_command = spiPC_write_read(upperByte16(yawAxis.attitude_feedback_15)) << 8;
	//yawAxis.attitude_command += spiPC_write_read(lowerByte16(yawAxis.attitude_feedback_15));
	//
	////rollAxis.rate_feedback_15 = rollAxis.attitude_command/2;	
	//pitchAxis.Kp = spiPC_write_read(upperByte16(rollAxis.rate_feedback_15 ))<< 8;
	//pitchAxis.Kp += spiPC_write_read(lowerByte16(rollAxis.rate_feedback_15 ));							
	//
	//pitchAxis.rate_feedback_15 = rollAxis.attitude_command/2;	
	//pitchAxis.Ki = spiPC_write_read(upperByte16(pitchAxis.rate_feedback_15  )) << 8;					
	//pitchAxis.Ki += spiPC_write_read(lowerByte16(pitchAxis.rate_feedback_15 ));							
	//
	////yawAxis.rate_feedback = rollAxis.pid_total;
	//pitchAxis.Kd= (spiPC_write_read(upperByte16(yawAxis.rate_feedback_15))) << 8;
	//pitchAxis.Kd+= spiPC_write_read(lowerByte16(yawAxis.rate_feedback_15));			
	//
	////yawAxis.rate_feedback_15 = rollAxis.attitude_command/2;
	//command= (spiPC_write_read(upperByte16(yawAxis.rate_feedback_15))) << 8;
	//command+= spiPC_write_read(lowerByte16(yawAxis.rate_feedback_15));			
	//
	//dummy_read = spiPC_write_read(END_PACKET_CHAR);													
	//dummy_read = spiPC_write_read(END_PACKET_CHAR);			
//
	//PORTE.OUTSET = PIN4_bm;
	//
	////rollAxis.Kp = pitchAxis.Kp;
	////rollAxis.Ki = pitchAxis.Ki;
	////rollAxis.Kd = pitchAxis.Kd;
	////
	////yawAxis.Kp = pitchAxis.Kp;
	////yawAxis.Ki = pitchAxis.Ki;
	////yawAxis.Kd = pitchAxis.Kd;
	//
	//
	//
	//return command;
	//
	//
//}

/***********************************************************************************************************
INPUT:
OUTPUT:
DISCRIPTION:   Read in data from the IMU.   IMU data are 16 bits sorted in 32 bit registers, see
data sheet for read commands.  Divide all the data by 2 to convert to 15 bit int, this will leave 
room for the header.
*********************************************************************************************************** */
void UpdateEulerAngles()
{
		PORTF.OUTCLR = PIN4_bm;
		
		uint8_t dummy_read;
		//psi = yaw  phi = roll    theta = pitch
		dummy_read = spiIMU_write_read(READ_COMMAND);
		dummy_read = spiIMU_write_read(UM6_EULER_PHI_THETA);
		
		rollAxis.attitude_feedback = (spiIMU_write_read(DUMMY_READ)<< 8) | spiIMU_write_read(DUMMY_READ);
		rollAxis.attitude_feedback_15 = rollAxis.attitude_feedback /2;
		
		pitchAxis.attitude_feedback = (spiIMU_write_read(DUMMY_READ)<< 8) | spiIMU_write_read(UM6_EULER_PSI);
		pitchAxis.attitude_feedback_15 =pitchAxis.attitude_feedback/2;	
		
		yawAxis.attitude_feedback = (spiIMU_write_read(DUMMY_READ)<< 8) | spiIMU_write_read(DUMMY_READ);
		yawAxis.attitude_feedback_15 = yawAxis.attitude_feedback/2;
		
		dummy_read = spiIMU_write_read(DUMMY_READ);     
		dummy_read =  spiIMU_write_read(UM6_GYRO_PROC_XY);			
			
		rollAxis.rate_feedback = (spiIMU_write_read(DUMMY_READ)<< 8) | spiIMU_write_read(DUMMY_READ);
		rollAxis.rate_feedback_15 =rollAxis.rate_feedback/2;
		
		pitchAxis.rate_feedback = (spiIMU_write_read(DUMMY_READ)<< 8) | spiIMU_write_read(UM6_GYRO_PROC_Z);
		pitchAxis.rate_feedback_15 =pitchAxis.rate_feedback/2;	
		
		yawAxis.rate_feedback = (spiIMU_write_read(DUMMY_READ)<< 8) | spiIMU_write_read(DUMMY_READ);
		yawAxis.rate_feedback_15 = yawAxis.rate_feedback/2;
		
		dummy_read = spiIMU_write_read(DUMMY_READ);
		dummy_read =  spiIMU_write_read(DUMMY_READ);
		
		PORTF.OUTSET = PIN4_bm;	
		
		
}

void trim()
{
	rollAxis.trim = rollAxis.attitude_command;
	pitchAxis.trim = pitchAxis.attitude_command;
	pitchAxis.trim = pitchAxis.attitude_command;
	
}


/***********************************************************************************************************
  INPUT:
  OUTPUT:
  DISCRIPTION:  Do all the pre control loop set up tasks
*********************************************************************************************************** */
uint8_t initSystem()
{

	sei();
	initUART();
	spi_set_up();
	intPID_gains();
	zeroSensor();
	_delay_ms(2000);
	zeroSensor();
	yawAxis.trim = 0;
	rollAxis.trim = 0;
	pitchAxis.trim = 0;
	return 1;

}





/***********************************************************************************************************
  INPUT:
  OUTPUT:
  DISCRIPTION:  Set up the 32Mhz clock
*********************************************************************************************************** */
void init32MHzClock(void)
{

	//	Enable the 32 MHz internal RC oscillator.
	OSC.CTRL|=OSC_RC32MEN_bm;
	//	The R32MRFY flag is set when the 32 MHz internal RC oscillator is stable
	//	and ready to be used as the System Clock source.
	while(!(OSC.STATUS &OSC_RC32MRDY_bm ));
	CCP=CCP_IOREG_gc;
	//	Select the 32MHz RC oscillator for the system clock
	CLK.CTRL = CLK_SCLKSEL_RC32M_gc;

	
	  ////Use to output clock to a pin
	  ////this must be done on port pin 7, see data sheet.
	//PORTD.DIRSET =  PIN7_bm;                   /* To output clock, port must be set as output */
	//PORTCFG.CLKEVOUT = PORTCFG_CLKOUT_PD7_gc; /* Output the clock frequency on PD7 to measure on Counter/Scope */
	
}


/***********************************************************************************************************
  INPUT:
  OUTPUT:
  DISCRIPTION:  Set up an interrupt to fire at 300Hz
*********************************************************************************************************** */
void intiLoopTimer()
{
	

	// Set the timer prescale.
	TCD0.CTRLA = TC_CLKSEL_DIV4_gc;

	/* Configure the timer for normal counting. */
	TCD0.CTRLB = TC_WGMODE_NORMAL_gc;

	
	
	//  32MHz / TC_CLKSEL_DIV4_gc
	TCD0.PER = 26666;  //  Need the time to overflow at 300Hz
	
	//TCD0.PER = 60000;
	//Configure timer to generate an interrupt on overflow. */
	TCD0.INTCTRLA = TC_OVFINTLVL_LO_gc;

	/* Enable this interrupt level. */
	PMIC.CTRL |= PMIC_LOLVLEN_bm;

}

/***********************************************************************************************************
  INPUT:
  OUTPUT:
  DISCRIPTION:   Interrupt vector
*********************************************************************************************************** */
ISR(TCD0_OVF_vect)
{
	//LEDPORT.OUTTGL = PIN0_bm;
	TCD0.CNT = 0;
	State();

}






uint8_t zeroSensor()
{
	
	PORTF.OUTCLR = PIN4_bm;

	//zero gyros is 0xAC
	//zero accel 0xAF
	//zero mad 0xB0
	
	//uint8_t dummy_read = 0x00;
	//psi = yaw  phi = roll    theta = pitch
	//0x01 0xAC 0x00 0x00 0x00 0x00
	dummy_read = spiIMU_write_read(WRITE_COMMAND);
	dummy_read = spiIMU_write_read(ZERO_GYROS);
	dummy_read = spiIMU_write_read(DUMMY_READ);
	dummy_read = spiIMU_write_read(DUMMY_READ);
	dummy_read = spiIMU_write_read(DUMMY_READ);
	dummy_read = spiIMU_write_read(DUMMY_READ);
	
	_delay_us(50);
	dummy_read = spiIMU_write_read(WRITE_COMMAND);
	dummy_read = spiIMU_write_read(SET_ACCEL_REF);
	dummy_read = spiIMU_write_read(DUMMY_READ);
	dummy_read = spiIMU_write_read(DUMMY_READ);
	dummy_read = spiIMU_write_read(DUMMY_READ);
	dummy_read = spiIMU_write_read(DUMMY_READ);
	

	_delay_us(50);
	dummy_read = spiIMU_write_read(WRITE_COMMAND);
	dummy_read = spiIMU_write_read(SET_MAG_REF);
	dummy_read = spiIMU_write_read(DUMMY_READ);
	dummy_read = spiIMU_write_read(DUMMY_READ);
	dummy_read = spiIMU_write_read(DUMMY_READ);
	dummy_read = spiIMU_write_read(DUMMY_READ);
	
	_delay_us(50);
	dummy_read = spiIMU_write_read(WRITE_COMMAND);
	dummy_read = spiIMU_write_read(UM6_RESET_EKF);
	dummy_read = spiIMU_write_read(DUMMY_READ);
	dummy_read = spiIMU_write_read(DUMMY_READ);
	dummy_read = spiIMU_write_read(DUMMY_READ);
	dummy_read = spiIMU_write_read(DUMMY_READ);
	

	PORTF.OUTSET = PIN4_bm;
	
	return 1;
	
}


/***********************************************************************************************************
INPUT:
OUTPUT:
DISCRIPTION:   Read in quaternion data, 4 32 bit floats.  Send the start command and then the quaternion command,
then wait until the IMU responds with the read to send command.

*********************************************************************************************************** */
void UpdateEulerAngles_YEI_fast()
{

	//SS
	PORTC.OUTCLR = PIN4_bm;
	
	dummy_read = spi_YEI_IMU_write_read(0xF6);
	dummy_read = spi_YEI_IMU_write_read(0x00);
	
	while(dummy_read != 0x01)
	{

		dummy_read = spi_YEI_IMU_write_read(0xFF);
		//put_USART_PC_char(dummy_read);
		delay_us(3);
	}
	

	
	for (int i = 3; i>=0; i--)
	{
		qx.b[i] = spi_YEI_IMU_write_read(0xFF);
		//put_USART_PC_char(qx.b[i]);
	}
	
	for (int i = 3; i>=0;i--)
	{
		qy.b[i] = spi_YEI_IMU_write_read(0xFF);
		//put_USART_PC_char(qy.b[i]);
	}

	for (int i = 3; i>=0;i--)
	{
		qz.b[i] = spi_YEI_IMU_write_read(0xFF);
		//put_USART_PC_char(qz.b[i]);
	}
	
	for (int i = 3; i>=0;i--)
	{
		qw.b[i] = spi_YEI_IMU_write_read(0xFF);
		//put_USART_PC_char(qw.b[i]);
	}
	//SS
	PORTC.OUTSET = PIN4_bm;
	ConvertQuaterionToEuler();
	UpdateCorrectedGyroRate_YEI();
	
}

/***********************************************************************************************************
INPUT:
OUTPUT:
DISCRIPTION:   Converts Quaternion data to Euler in radians and then scales and converts to int16_t.
The function will set the global pitch roll and yaw angles to a 15 bit scaled radian (2*PI = 2^15)
http://www.euclideanspace.com/maths/geometry/rotations/conversions/quaternionToEuler/
*********************************************************************************************************** */
void ConvertQuaterionToEuler()
{
	//  test to to check if we are close to a singularity so we can take care of it
	double test = qx.fp*qy.fp + qz.fp*qw.fp;
	
	if (test > 0.499) { // singularity at north pole, .499 is 86.3 degrees
		yawAxis.attitude_feedback = 2 * atan2(qx.fp,qw.fp);
		pitchAxis.attitude_feedback =PI/2;
		rollAxis.attitude_feedback = 0;
	}
	else if (test < -0.499) { // singularity at south pole, , .499 is 86.3 degrees
		yawAxis.attitude_feedback = -2 * atan2(qx.fp,qw.fp);
		pitchAxis.attitude_feedback = -PI/2;
		rollAxis.attitude_feedback = 0;
	}
	else
	{
		
		//  faster way to calculate the squares of a number
		float sqx = qx.fp*qx.fp;
		float sqy = qy.fp*qy.fp;
		float sqz = qz.fp*qz.fp;
		
		//  PI (rad) * 2^15/ 2PI = radians scaled to 15 bits and then converts to a singed 16 bit integer
		yawAxis.attitude_feedback = (int16_t)(5215.1891*atan2f(2*qy.fp*qw.fp-2*qx.fp*qz.fp , 1 - 2*sqy - 2*sqz));    // atanf2 returns -PI,PI  (2*PI = 2^15)
		rollAxis.attitude_feedback = (int16_t)(10430.378*asinf(2*test));											//asinf returns -PI/2,PI/2 (PI = 2^15)
		pitchAxis.attitude_feedback = (int16_t)(5215.1891*atan2f(2*qx.fp*qw.fp-2*qy.fp*qz.fp , 1 - 2*sqx - 2*sqz));	// atanf2 returns -PI,PI  (2*PI = 2^15)

	}

}


/***********************************************************************************************************
INPUT:
OUTPUT:
DISCRIPTION:   Read corrected gyro data.  The data are float so the should be read in and converted to
int16.  The natural gyro output  is +/- 2000 deg./Sec maximum, however the data is reported but the IMU in Rad/Sec (float)
CONVERT RAD/SEC to DEG/SEC and scale to +/- 2^15
//2000 (deg/sec) * (PI/180)(Rad/Sec) = 2^15/2 = 16343
//data (16343/34.90658) = 469.36709353
*********************************************************************************************************** */
void UpdateCorrectedGyroRate_YEI()
{

	PORTC.OUTCLR = PIN4_bm;

	dummy_read = spi_YEI_IMU_write_read(0xF6);
	dummy_read = spi_YEI_IMU_write_read(0x21);

	while(dummy_read != 0x01)
	{
	dummy_read = spi_YEI_IMU_write_read(0xFF);
	delay_us(3);
	//put_USART_PC_char(dummy_read);
	//PORTA.OUTTGL = 0b00000010;
	}

	for (int i = 3; i>=0;i--)
	{
	rr.b[i] = spi_YEI_IMU_write_read(0xFF);
	//put_USART_PC_char(yr.b[i]);
	}

	for (int i = 3; i>=0;i--)
	{
	yr.b[i] = spi_YEI_IMU_write_read(0xFF);
	//put_USART_PC_char(pr.b[i]);
	}

	for (int i = 3; i>=0;i--)
	{
	pr.b[i] = spi_YEI_IMU_write_read(0xFF);
	//put_USART_PC_char(rr.b[i]);
	}

	PORTC.OUTSET = PIN4_bm;

	rollAxis.rate_feedback = (int16_t)(pr.fp*SCALE_GYRO_TO_INT_DPS);
	rollAxis.rate_feedback = (int16_t)(rr.fp*SCALE_GYRO_TO_INT_DPS);
	yawAxis.rate_feedback = (int16_t)(yr.fp*SCALE_GYRO_TO_INT_DPS);
}