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

#define TESTPORT_A PORTA


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

uint8_t systemState = SYSTEM_STATE_STARTUP;
uint8_t dummy_read;
uint16_t int16counter;
int16_t command;




/***********************************************************************************************************
INPUT:
OUTPUT:
DISCRIPTION: Main is used for set up and then just an infinite loop
*********************************************************************************************************** */
int main()
{
		PORTA.OUTTGL = 0x0F;
		systemState = SYSTEM_STATE_STARTUP;
		init32MHzClock();
		intiLoopTimer();
		initSystem();

		while(1)
		{
			nop();
		}
}





/***********************************************************************************************************
INPUT:
OUTPUT:
DISCRIPTION:   Determine what to do based on systemState.  Used to initializes and arm the quad.
*********************************************************************************************************** */
void State()
{
	
	//sendUM6_Data();
	switch(systemState)
	{
		case SYSTEM_STATE_STARTUP:
			PORTA.OUTTGL = 0x00;
			PORTA.OUTTGL = PIN2_bm;
			if (initSystem() ==  1)
			{
				systemState = SYSTEM_ZERO;
			}
			break;
		
		case SYSTEM_ZERO:
			// read commands from the PC and zero the system as necessary
			// PC transaction, read in command and send out sensor data
			UpdateEulerAngles();
		
			//PORTA.OUTTGL = PIN3_bm;
			//  get the last command sent form the PC, either zero the IMU or get ready to arm the system
			if (WriteToPC_SPI() == SYSTEM_ZERO)
			{
				zeroSensor();
			}
			
			if (WriteToPC_SPI() == SYSTEM_ARM)
			{
				systemState = SYSTEM_ARM;
			}
		
			break;
		
		case SYSTEM_ARM:
			//Arm the system by enabling the PWM
			PORTA.OUTTGL = PIN2_bm;
			initPWM();
			systemState = SYSTEM_STATE_FLY;
			PORTA.OUTTGL = PIN2_bm;
			_delay_ms(1000);
			PORTA.OUTTGL = PIN2_bm;
			_delay_ms(1000);
			PORTA.OUTTGL = PIN2_bm;
			break;
		
		case SYSTEM_STATE_FLY:
			// run the control loop
			PORTA.OUTTGL = 0x00;
			PORTA.OUTTGL = PIN3_bm;
			ControlLoop();
			break;
		
		case SYSTEM_DISARM:
			// run the control loop
			//PORTA.OUTTGL = 0x0F;
		
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
	SetPulseWidths();
	pid_rate(&pitchAxis);
	if (int16counter >= 20)

	{
		WriteToPC_SPI();
		//sendUM6_Data();
		int16counter = 0;
	}
	
}

void SetPulseWidths()
{
	// check the signs
	if(throttleAxis.thrust > 2000 && throttleAxis.thrust <= 4095)
	{
		doPWM(
		throttleAxis.thrust * SCALE_THROTTLE - pitchAxis.pid_total,
		throttleAxis.thrust * SCALE_THROTTLE - rollAxis.pid_total,
		throttleAxis.thrust * SCALE_THROTTLE + pitchAxis.pid_total,
		throttleAxis.thrust * SCALE_THROTTLE + rollAxis.pid_total

		
		//throttleAxis.thrust * SCALE_THROTTLE - pitchAxis.pid_total- yawAxis.pid_total,
		//throttleAxis.thrust * SCALE_THROTTLE - rollAxis.pid_total,
		//throttleAxis.thrust * SCALE_THROTTLE + pitchAxis.pid_total,
		//throttleAxis.thrust * SCALE_THROTTLE + rollAxis.pid_total + yawAxis.pid_total
	
		);
		
	}
	else
	{
		doPWM(0,0,0,0);
	}
}


void intPID_gains()
{


	
	rollAxis.Kp_rate = 5;
	rollAxis.Ki_rate =0;
	rollAxis.Kd_rate =5;
	
	pitchAxis.Kp_rate = 5;
	pitchAxis.Ki_rate =0;
	pitchAxis.Kd_rate =5;
	
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

		sendData_int16_t(0xCCCC);					//0xCCCC is the heade
		sendData_int16_t(command);
		//sendData_int16_t(pitchAxis.Kp);
		//sendData_int16_t(pitchAxis.Ki);
	
		


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
	
	throttleAxis.thrust = spiPC_write_read(upperByte16(throttleAxis.thrust )) << 8;						
	throttleAxis.thrust += spiPC_write_read(lowerByte16(throttleAxis.thrust ));							
	
	rollAxis.attitude_command = spiPC_write_read(upperByte16(pitchAxis.attitude_feedback)) << 8;
	rollAxis.attitude_command  += spiPC_write_read(lowerByte16(pitchAxis.attitude_feedback));
	
	pitchAxis.attitude_command = spiPC_write_read(upperByte16(rollAxis.attitude_feedback)) << 8;
	pitchAxis.attitude_command += spiPC_write_read(lowerByte16(rollAxis.attitude_feedback));

	yawAxis.attitude_command = spiPC_write_read(upperByte16(yawAxis.attitude_feedback)) << 8;
	yawAxis.attitude_command += spiPC_write_read(lowerByte16(yawAxis.attitude_feedback));
		
	pitchAxis.Kp = (spiPC_write_read(upperByte16(pitchAxis.rate_feedback ))) << 8;					
	pitchAxis.Kp += spiPC_write_read(lowerByte16(pitchAxis.rate_feedback ));							
	
	pitchAxis.Ki = (spiPC_write_read(upperByte16(pitchAxis.rate_error ))) << 8;					
	pitchAxis.Ki += spiPC_write_read(lowerByte16(pitchAxis.rate_error));							
		
	pitchAxis.Kd= (spiPC_write_read(upperByte16(pitchAxis.pid_total))) << 8;					
	pitchAxis.Kd+= spiPC_write_read(lowerByte16(pitchAxis.pid_total));			
	
	command= (spiPC_write_read(upperByte16(yawAxis.rate_feedback))) << 8;
	command+= spiPC_write_read(lowerByte16(yawAxis.rate_feedback));			
	
	dummy_read = spiPC_write_read(END_PACKET_CHAR);													
	dummy_read = spiPC_write_read(END_PACKET_CHAR);			
	
	//rollAxis.Kp = pitchAxis.Kp;
	//rollAxis.Ki = rollAxis.Ki;
	//rollAxis.Kd_rate = rollAxis.Kd_rate;										
	
	
	PORTE.OUTSET = PIN4_bm;
	
	return command;
	
}

/***********************************************************************************************************
INPUT:
OUTPUT:
DISCRIPTION:   Read in data from the IMU.  Most of the IMU data are 16 bits sorted in 32 bit registers, see
data sheet for read commands
*********************************************************************************************************** */
void UpdateEulerAngles()
{

	PORTF.OUTCLR = PIN4_bm;

	uint8_t dummy_read = 0x00;
	//psi = yaw  phi = roll    theta = pitch
	dummy_read = spiIMU_write_read(READ_COMMAND);
	dummy_read = spiIMU_write_read(UM6_EULER_PHI_THETA);
	
	//MSB first
	rollAxis.attitude_feedback = (spiIMU_write_read(DUMMY_READ)<< 8) | spiIMU_write_read(DUMMY_READ);

	pitchAxis.attitude_feedback = (spiIMU_write_read(DUMMY_READ)<< 8) | spiIMU_write_read(UM6_EULER_PSI);
	
	yawAxis.attitude_feedback = (spiIMU_write_read(DUMMY_READ)<< 8) | spiIMU_write_read(DUMMY_READ);

	dummy_read = spiIMU_write_read(DUMMY_READ);     dummy_read =  spiIMU_write_read(UM6_GYRO_PROC_XY);			// reserved bytes
	
	pitchAxis.rate_feedback  = (spiIMU_write_read(DUMMY_READ)<< 8) | spiIMU_write_read(DUMMY_READ);

	rollAxis.rate_feedback = (spiIMU_write_read(DUMMY_READ)<< 8) | spiIMU_write_read(UM6_GYRO_PROC_Z);
	
	yawAxis.rate_feedback = (spiIMU_write_read(DUMMY_READ)<< 8) | spiIMU_write_read(DUMMY_READ);

	dummy_read = spiIMU_write_read(DUMMY_READ);     dummy_read =  spiIMU_write_read(UM6_GPS_ALTITUDE);			// reserved bytes
	


	PORTF.OUTSET = PIN4_bm;
	//
	//rollAxis.attitude_feedback = 0x0001;
	//pitchAxis.attitude_feedback = 0x0002;
	//yawAxis.attitude_feedback = 0x0003;
	//
	//rollAxis.rate_feedback = 0x0100;
	//pitchAxis.rate_feedback = 0x0100;
	//yawAxis.rate_feedback = 0x0006;
}



/***********************************************************************************************************
  INPUT:
  OUTPUT:
  DISCRIPTION:  Do all the pre control loop set up tasks
*********************************************************************************************************** */
uint8_t initSystem()
{
	PORTA.DIRSET = 0xFF;			//  LEDS
	sei();
	initUART();
	spi_set_up();
	intPID_gains();
	zeroSensor();
	_delay_ms(2000);
	zeroSensor();
	PORTA.DIRSET = 0x00;			//  LEDS
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

	
	// 23000 counts set f = 300HZ by trial and error
	//TCD0.PER = 38686;
	//TCD0.PER = 30000;
	TCD0.PER = 24000;
	//TCD0.PER = 40000;
	//TCD0.PER = 8000;
	//TCD0.PER = 5000;
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
	
	//ControlLoop();
	 State();
	TCD0.CNT = 0;
	
}






uint8_t zeroSensor()
{
	
	PORTF.OUTCLR = PIN4_bm;

	//zero gyros is 0xAC
	//zero accel 0xAF
	//zero mad 0xB0
	
	uint8_t dummy_read = 0x00;
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
