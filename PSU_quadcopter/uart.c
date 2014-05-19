/*
 * uart.c
 *
 * Created: 3/1/2014 7:50:36 PM
 *  Author: Justin
 */ 

#include "usart_driver.h"



#define XBEE_USART				USARTD1
#define	IMU_USART				USARTC1
#define MASK_TOP_BYTE			0x00FF
#define upperByte16(x) (MASK_TOP_BYTE & (x >> 8))		// get the top 8 bits of a 16 bit word
#define lowerByte16(x) (MASK_TOP_BYTE & x)				// get the lower 8 bits of a 16 bit word
#define tbi(x,y) x ^= _BV(y)		


void sendData_int16_t(int16_t);
void put_USART_PC_char(uint8_t);


//  set up the serial port for sending data back and forth to the PC
// via the XBEE radio.
//  8 bits, no parity, 2 stop bits
void initUART()
{
	
	//  XBEE on USARTD1
	/* PD6 (RXD1) input*/
	PORTD.DIRCLR = PIN6_bm;
	/* PD7 (TXD1) as output. */
	PORTD.DIRSET = PIN7_bm;
	
	
	//  IMU on USARTC1
	/* PC6 (RXD1) input*/
	PORTC.DIRCLR = PIN6_bm;
	/* PD7 (TXD1) as output. */
	PORTC.DIRSET = PIN7_bm;
	
	USART_Format_Set(&XBEE_USART, USART_CHSIZE_8BIT_gc, USART_PMODE_DISABLED_gc, true);
	USART_Baudrate_Set(&XBEE_USART, 1047 , -6);			// set for 32MHZ and 115200
	/* Enable both RX and TX. */
	USART_Rx_Enable(&XBEE_USART);
	USART_Tx_Enable(&XBEE_USART);
	

}


//send 16 bit data on USART, 2 bytes
void sendData_int16_t(int16_t sendthis)
{
	put_USART_PC_char( MASK_TOP_BYTE & (sendthis >> 8));
	put_USART_PC_char (MASK_TOP_BYTE & sendthis);
}

void put_USART_PC_char(uint8_t sendThis)
{

	do{

	}while(!USART_IsTXDataRegisterEmpty(&XBEE_USART));
	USART_PutChar(&XBEE_USART, sendThis);
}