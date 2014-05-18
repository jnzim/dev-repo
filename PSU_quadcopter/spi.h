/*
 * spi.h
 *
 * Created: 3/1/2014 6:18:03 PM
 *  Author: Justin
 */ 


#ifndef SPI_H_
#define SPI_H_

unsigned char spiPC_write_read(unsigned char);
unsigned char spiIMU_write_read(unsigned char);
void spi_set_up();


#endif /* SPI_H_ */