/*
 * uart.h
 *
 * Created: 3/1/2014 7:51:13 PM
 *  Author: Justin
 */ 


#ifndef UART_H_
#define UART_H_


void initUART();

void sendData_int16_t(int16_t);
void put_USART_PC_char(uint8_t);

#endif /* UART_H_ */