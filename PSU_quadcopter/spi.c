/*
 * spi.c
 *
 * Created: 3/1/2014 6:08:20 PM
 *  Author: Justin
 */ 


#include "spi.h"
#include "spi_driver.h"


unsigned char spiPC_write_read(unsigned char);
unsigned char spiIMU_write_read(unsigned char);
void spi_set_up();


/* Instantiate pointer to ssPort. */
PORT_t *ssPortF = &PORTF;
PORT_t *ssPortE = &PORTE;

/* Global variables */
SPI_Master_t spiMasterF;
SPI_Master_t spiMasterE;


/**********************************************************************************************.3000000000*************
INPUT:
OUTPUT:
DISCRIPTION:  SPI write read function
			  Load the register, this will start a transfer on MOSI
			  Wait until the interrupt flag is set	
			  Read the data from the data register, this was on MISO
*********************************************************************************************************** */

unsigned char spiPC_write_read(unsigned char spi_data1)
{
	SPIE.DATA = spi_data1;
	while(!(SPIE.STATUS & SPI_IF_bm)); // Wait until the data transfer is complete
	_delay_us(6);
	return SPIE.DATA;
}



/***********************************************************************************************************
INPUT:
OUTPUT:
DISCRIPTION:  SPI write read function
			  Load the register, this will start a transfer on MOSI
			  Wait until the interrupt flag is set	
			  Read the data from the data register, this was on MISO
*********************************************************************************************************** */
unsigned char spiIMU_write_read(unsigned char spi_data)
{
	SPIF.DATA = spi_data;
	while(!(SPIF.STATUS & SPI_IF_bm)); // Wait until the data transfer is complete
	_delay_us(3);
	return SPIF.DATA;
}


/*
	PF4 SS
	PF5 MOSI
	PF6	MISO
	PF7	SCK
*/

/***********************************************************************************************************
INPUT:
OUTPUT:
DISCRIPTION:    Initialize SPI master on port F for the IMU communication.
			    Initialize SPI master on port F for the PC communication
*********************************************************************************************************** */
	
void spi_set_up()
{
	/* Initialize SPI master on port F for the IMU communication. */
	/* Init SS pin as output with wired AND and pull-up. */
	//*************************************************************
	PORTF.DIRSET = PIN4_bm;
	PORTF.PIN4CTRL = PORT_OPC_WIREDANDPULL_gc;

	/* Set SS output to high. (No slave addressed). */
	PORTF.OUTSET = PIN4_bm;

	/* Initialize SPI master on port F for the IMU communication. */
	SPI_MasterInit(&spiMasterF,
		&SPIF,
		&PORTF,
		false,
		SPI_MODE_0_gc,							//The UM6 SPI clock (SCK) is active high, with data clocked in on the first rising edge1
		SPI_INTLVL_OFF_gc,
		false,									// false to double clock mode
		SPI_PRESCALER_DIV128_gc);					//32mHz /128 = 250kHz  MAX rate is 400kHz but there is not prescaler

		SPIF.DATA = 0x00;						// init SPI data register
		
		
		/* Initialize SPI master on port F for the PC communication */
		//*************************************************************
		/* Init SS pin as output with wired AND and pull-up. */
		PORTE.DIRSET = PIN4_bm;
		PORTE.PIN4CTRL = PORT_OPC_WIREDANDPULL_gc;

		/* Set SS output to high. (No slave addressed). */
		PORTE.OUTSET = PIN4_bm;

		SPI_MasterInit(&spiMasterE,
		&SPIE,
		&PORTE,
		false,
		SPI_MODE_0_gc,							//The UM6 SPI clock (SCK) is active high, with data clocked in on the first rising edge1
		SPI_INTLVL_OFF_gc,
		false,									// false to double clock mode
		SPI_PRESCALER_DIV64_gc);				//32mHz /64 = 500,000 bps
		SPIE.DATA = 0x00;						// init SPI data register

}


