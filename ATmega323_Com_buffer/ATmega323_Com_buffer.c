/*
 * ATmega323_Com_buffer.c
 *
 * Created: 10/19/2013 11:11:36 AM
 *  Author: Justin
 */ 


#include <avr/io.h>
#include <util/delay.h>
#include <avr/interrupt.h>
#include <stdbool.h>
#include "AxisData.h"


#define MASK_TOP_BYTE			0x00FF
#define BUFSIZE					18								// BUFSIZE should be set to the number of bytes expected plus as 2 byte header
//#define PACKET_END_CHAR			0xCC
#define PACKET_END_CHAR			0xAA

#define sbi(x,y) x |= _BV(y)									//set bit - using bitwise OR operator
#define cbi(x,y) x &= ~(_BV(y))									//clear bit - using bitwise AND operator
#define tbi(x,y) x ^= _BV(y)									//toggle bit - using bitwise XOR operator
#define is_high(x,y) (x & _BV(y) == _BV(y))						//check if the y'th bit of register 'x' is high ... test if its AND with 1 is 1
#define upperByte(x) (MASK_TOP_BYTE & (x >> 8))					// get the top 8 bits of a 16 bit word
#define lowerByte(x) (MASK_TOP_BYTE & x)						// get the lower 8 bits of a 16 bit word


Axis_t rollAxis,yawAxis,pitchAxis,throttleAxis;
System_data_t systemData;

void USART_init(void);
uint8_t USART_receive(void);
void USART_send( uint8_t );
void SPIinit(void);
void WriteByteSPI(uint8_t );
uint8_t ReadByteSPI(void);
uint8_t SPI_transaction(uint8_t );
void SPIinitSlave(void );
uint8_t received_from_spi(uint8_t);
void USART_sendData_int16(int16_t);
bool IsPacketEnd(uint8_t );
void Send_USART_PC_Data(void);
void Timer_init(void);
uint8_t Read_USART_CommandData(void);
void parse_packet(void);
void USART_send_byte(uint8_t);
void TransactWithPC(void);

int bufferPostion = 0;
uint8_t process = 0;
int dataSendCounter = 0;
uint8_t dummyVariable = 0;
volatile bool gotIMU_Packet = false;
uint8_t rdata, commandPacketIndex, lastByte;
uint8_t cmdIndex;
uint8_t incoming[BUFSIZE];
volatile uint8_t received = 0;
uint16_t command;
bool SPI_IsBusy = false;


/* 
This is a data buffer sending and receiving data from the PC.  The master (XMEGA) and request command data on the SPI interrupt, otherwise this just sits in a loop 
sending data back and forth to the PC.

The SPI transaction send command data for the control loop and quad copter state data to be displayed on the UI.

TODO:  add functionality to send 2 byte command to the XMGEGA for the state machine, exc..


*/


int main()
{
	DDRC=0xff;									//Data direction register on PORTC as OUTPUT
	USART_init();
	SPIinitSlave();
	//Timer_init();	
	while(1) 
	{
	TransactWithPC();		
	}
}

/***********************************************************************************************************
  INPUT:
  OUTPUT:
  DISCRIPTION:  Read in date from the PC on the USART and then send a packet out.
  Refresh the next SPI command packet with the latest PC data.
  
*********************************************************************************************************** */
TransactWithPC()
{
	
			Read_USART_CommandData();
			Send_USART_PC_Data();
			RefreshCommandPacket();
			gotIMU_Packet = false;
	
}


/***********************************************************************************************************
  INPUT:
  OUTPUT:
  DISCRIPTION:  SPI ISR, called every time a byte is received in the SPI data register
*********************************************************************************************************** */
ISR(SPI_STC_vect)
{	
	tbi(PORTC,PC3);
	transmit_SPI_Packet();
	
}




/***********************************************************************************************************
  INPUT:
  OUTPUT:
  DISCRIPTION:  Set up a command packet that will be exchanged with the main uC over the P.
  Place the 16 bit words in a byte array so they can be easily sent over the SPI bus.
  Note, reload the SPDR so that when the first request from master to send data is received, 
  we are ready to reply with relevant data.
*********************************************************************************************************** */
void RefreshCommandPacket()
{

	int i = 0;


		// preload the first byte so the master doesn't get junk on the first transaction
		SPDR = upperByte(throttleAxis.thrust);
		commandPacket[i++] = lowerByte(throttleAxis.thrust);
		
		commandPacket[i++] = upperByte(rollAxis.attitude_command);
		commandPacket[i++] = lowerByte(rollAxis.attitude_command);
		
		commandPacket[i++] = upperByte(pitchAxis.attitude_command);
		commandPacket[i++] = lowerByte(pitchAxis.attitude_command);

		commandPacket[i++] = upperByte(yawAxis.attitude_command);
		commandPacket[i++] = lowerByte(yawAxis.attitude_command);
		
		commandPacket[i++] = upperByte(pitchAxis.Kp_attitude);
		commandPacket[i++] = lowerByte(pitchAxis.Kp_attitude);
		
		commandPacket[i++] = upperByte(pitchAxis.Ki_attitude);
		commandPacket[i++] = lowerByte(pitchAxis.Ki_attitude);
		
		commandPacket[i++] = upperByte(pitchAxis.Kd_attitude);
		commandPacket[i++] = lowerByte(pitchAxis.Kd_attitude);
		
		commandPacket[i++] = upperByte(command);
		commandPacket[i++] = lowerByte(command);

		commandPacket[i++] = PACKET_END_CHAR;
		commandPacket[i++] = PACKET_END_CHAR;

}

/***********************************************************************************************************
  INPUT:
  OUTPUT:
  DISCRIPTION:  Send and received data with XMEGA.  We also looking for the end of packet tokens so that
  we know when to parse out the data.
*********************************************************************************************************** */

void transmit_SPI_Packet()
{
	
	//  check to see if we have found the 2 byte end of packet indicator
	//  bool isEnd = IsPacketEnd(rdata = SPI_transaction(commandPacket[received]));
	bool isEnd = IsPacketEnd(rdata = SPI_transaction(commandPacket[received]));

	//  if we have not found the end of packet and we have not gathered the correct number of bytes, collect a byte 
	if   ((received != (BUFSIZE-1))  && (!isEnd))
	{
		//USART_send_byte(received);
		incoming[received++]  = rdata;
		
		tbi(PORTC,PC5);
	}
	//  if we have found the end of packet and we have gathered the correct number of bytes, parse the data
	//  also we should be able to do a UART transaction before the next SPI frame starts,  depend of PC and Xmega data rates tho
	else if ((received == (BUFSIZE-1))  && isEnd)
	{
		parse_packet();

		received = 0;
		gotIMU_Packet = true;	
		tbi(PORTC,PC4);			// 150mSec
		//TransactWithPC();
	}
	//  we found the the end of packet sequence but did not gather the correct number of bytes, something is wrong
	//  try to sync
	else if ((received != (BUFSIZE-1)) && isEnd)
	{
		received = 0;
		//tbi(PORTC,PC3);
		
	}
	//  got the correct number of byte but no packed is, resync
	else if ((received == (BUFSIZE-1)) && !(isEnd))
	{
		received = 0;
		tbi(PORTC,PC2);
			
	}
	else {}

}




/***********************************************************************************************************
  INPUT:  the byte we just received from the SPI
  OUTPUT: true if we have received the 2nd end of packet byte, else false
  DISCRIPTION:  Check for the 2 end of packet tokens
*********************************************************************************************************** */

bool IsPacketEnd(uint8_t thisByte)
{
	//static uint8_t theLastByte;
	//  both the current byte and the previous byte are PACKET_END_CHAR, got header is true
	if ((thisByte == PACKET_END_CHAR) && (lastByte == PACKET_END_CHAR))
	{
		lastByte = 0x00;
		return true;
	}
	//  the current byte is PACKET_END_CHAR and the last byte is not, maybe the header start
	else if((thisByte == PACKET_END_CHAR) && (lastByte != PACKET_END_CHAR))
	{
		lastByte = PACKET_END_CHAR;
		return false;
	}
	//  the current byte is PACKET_END_CHAR, but the last byte is not, just keep trying
	else if((thisByte != PACKET_END_CHAR) && (lastByte == PACKET_END_CHAR))
	{
		lastByte = 0x00;
		return false;
	}
	else
	{
		// neither  byte is PACKET_END_CHAR
		return false;
	}
	
}



/***********************************************************************************************************
  INPUT: 
  OUTPUT: 
  DISCRIPTION:  Parse out the data that's been received from XMEGA
*********************************************************************************************************** */

void parse_packet()
{
	int i = 0;
	
	
	dummyVariable=(dummyVariable << 8 ) + incoming[i++];					//receive thrust
	dummyVariable=(dummyVariable << 8 ) + incoming[i++];					//receive thrust
	
	rollAxis.attitude_feedback = (rollAxis.attitude_feedback << 8 ) + incoming[i++];
	rollAxis.attitude_feedback = (rollAxis.attitude_feedback << 8 ) + incoming[i++];
	
	pitchAxis.attitude_feedback = (pitchAxis.attitude_feedback << 8 ) + incoming[i++];
	pitchAxis.attitude_feedback = (pitchAxis.attitude_feedback << 8 ) + incoming[i++];

	yawAxis.attitude_feedback = (yawAxis.attitude_feedback << 8 ) + incoming[i++];
	yawAxis.attitude_feedback = (yawAxis.attitude_feedback << 8 ) + incoming[i++];
	
	rollAxis.rate_feedback = (rollAxis.rate_feedback  << 8 ) + incoming[i++];
	rollAxis.rate_feedback =(rollAxis.rate_feedback  << 8 ) + incoming[i++];
	
	pitchAxis.rate_feedback = (pitchAxis.rate_feedback  << 8 ) + incoming[i++];
	pitchAxis.rate_feedback =(pitchAxis.rate_feedback  << 8 ) + incoming[i++];
	
	yawAxis.rate_feedback = (yawAxis.rate_feedback   << 8 ) + incoming[i++];
	yawAxis.rate_feedback = (yawAxis.rate_feedback   << 8 ) + incoming[i++];
	
	
	// dummy read for now, need something to transact for the command
	dummyVariable = (yawAxis.rate_feedback   << 8 ) + incoming[i++];
	dummyVariable = (yawAxis.rate_feedback   << 8 ) + incoming[i++];

}


//  read and write SPI
uint8_t SPI_transaction(uint8_t data)
{
	//  the interrupt flag was set when the ISR was called do just read the data register, then copy the data to be sent to the register,
	//  return the received byte
	uint8_t recivedByte = SPDR;
	SPDR  = data ;
	return recivedByte;
}



//  the number of bytes in begin transfered in this function
// should match the number of bytes the PC expects to receive
void Send_USART_PC_Data()
{
	// send header first, the PC will use the header to parse out the stream

	USART_sendData_int16(0xAAAA);					
	USART_sendData_int16(throttleAxis.thrust);
	USART_sendData_int16(rollAxis.attitude_feedback);
	USART_sendData_int16(pitchAxis.attitude_feedback);
	USART_sendData_int16(yawAxis.attitude_feedback);
	USART_sendData_int16(rollAxis.rate_feedback);
	USART_sendData_int16(pitchAxis.rate_feedback);
	USART_sendData_int16(yawAxis.rate_feedback);
	
}

// yellow blue red green

//250 mSec * 1000mSec / 1 Sec * 1/32,000,000
//  this date is read in on the USART, it's sent from the PC
//  joystick commands, gains...
//this should be call every 42mSec, 
uint8_t Read_USART_CommandData()
{	
		
		bool gotHeader = false;

		//  hold execution until we get the 2 byte header character
		while (gotHeader == false)
		{
			if (USART_receive() == PACKET_END_CHAR)
			{
				if (USART_receive() == PACKET_END_CHAR)
				{
					gotHeader = true;
					//tbi(PORTC,PC3);
				}
			}
		}
		
		throttleAxis.thrust = (USART_receive() << 8) | USART_receive();
		rollAxis.attitude_command = (USART_receive() << 8) | USART_receive();
		pitchAxis.attitude_command = (USART_receive() << 8) | USART_receive();
		yawAxis.attitude_command = (USART_receive() << 8) | USART_receive();
		pitchAxis.Kp_attitude = (USART_receive() << 8) | USART_receive();
		pitchAxis.Ki_attitude = (USART_receive() << 8) | USART_receive();
		pitchAxis.Kd_attitude = (USART_receive() << 8) | USART_receive();
		command = (USART_receive() << 8) | USART_receive();
		
		return 1;
			
}



void USART_init(void)
{
	// UBBRR values, 115200bps @ 11Mhz from Worm food on line calculator
	UBRR0H = 0x00;
	UBRR0L = 0x05;
	UCSR0B = (1<<RXEN0)|(1<<TXEN0);
	UCSR0C = (3<<UCSZ00);
	
}



//read in one byte
uint8_t USART_receive(void)
{
	//  RXCn  is set when the Rx is complete
	while(!(UCSR0A & (1 << RXC0)));
	//  one the RXC0 bit is set, we can load the data register with the byte to send
	return UDR0;
	
}



//  send data
void USART_send( uint8_t data)
{
	// UDREx is set when the register is empty
	while(!(UCSR0A & (1<<UDRE0)));
	UDR0 = data;
	
}




void SPIinitSlave()
{

    //**********************************************************
	volatile char IOReg;
	// Set PB6(MISO) as output
	DDRB    = (1<<PB4);											
	SPCR = (1<<SPIE)|(1<<SPE)|(0<<DORD)|(0<<CPOL)|(0<<CPHA);				// Enable SPI Interrupt and SPI in Slave Mode with SCK = CK/4
	IOReg   = SPSR;															// Clear SPIF bit in SPSR
	IOReg   = SPDR;
	SPDR = 0x00;															// initialize SPI data register
	sei();																	// Enable global interrupt			
	

}


uint8_t received_from_spi(uint8_t data)
{
	SPDR = data;
	return SPDR;
}


//send 16 bit data on USART, 2 bytes
void USART_sendData_int16(int16_t sendthis)
{
	//  send upper byte
	while(!(UCSR0A & (1<<UDRE0)));
	UDR0 = MASK_TOP_BYTE & (sendthis >> 8);
	
	//  send lower byte
	while(!(UCSR0A & (1<<UDRE0)));
	UDR0 = (MASK_TOP_BYTE & sendthis);
	
}

void USART_send_byte(uint8_t sendThis)
{
		//  send lower byte
		while(!(UCSR0A & (1<<UDRE0)));
		UDR0 =  sendThis;
}



//send 16 bit data on USART, 2 bytes
void USART_sendData_int32(int32_t sendthis)
{
	//  send upper byte
	while(!(UCSR0A & (1<<UDRE0)));
	UDR0 = ( sendthis >> 24);
	
	//  send lower byte
	while(!(UCSR0A & (1<<UDRE0)));
	UDR0 = ( sendthis >> 16);
	
	//  send lower byte
	while(!(UCSR0A & (1<<UDRE0)));
	UDR0 = ( sendthis >> 8);
	
	//  send lower byte
	while(!(UCSR0A & (1<<UDRE0)));
	UDR0 = ( sendthis);
	

	
}

//  set up the timer that will be used to send and receive data from the PC / USART
//NOT USED:
void Timer_init()
{

	// initialize Timer1
	cli();          // disable global interrupts
	TCCR1A = 0;     // set entire TCCR1A register to 0
	TCCR1B = 0;     // same for TCCR1B
	    
	// set compare match register to desired timer count:
	OCR1A = 17187;								//17187 is 100mSec with 0 1 1 prescale
	// turn on CTC mode:
	TCCR1B |= (1 << WGM12);
	TCCR1B |= (0 << CS12) | (1 << CS11) | (1 << CS10); // 0 1 1 is clock /64,  11MHz /64 = 171,875
	// enable timer compare interrupt:
	TIMSK1 |= (1 << OCIE1A);
	sei();          // enable global interrupts

}






