/*
 * command.c
 *
 * Created: 6/29/2014 8:24:14 PM
 *  Author: Justin
 */ 

#include <avr/io.h>
#include <stdlib.h>
#include <math.h>


#define PI 3.1415926536

double t;
float f;
float delta_t = 1;



float GetSinCommand(int A, int16_t f)
{
	t += delta_t;
	double y;
	float frequency = (float)((f)/100);
	//y = 1500 * sin(2.0* 3.14159 * t * frequency);
	y = 1500 * sin(2.0 * 3.14 * t * frequency);
	return y;
}

float GetCosCommand(int A, int16_t f)
{
	t += delta_t;
	double y;
	float frequency = (float)((f)/100);
	//y = 1500 * sin(2.0* 3.14159 * t * frequency);
	y = 1500 * cos(2.0 * 3.14 * t * frequency);
	return y;
}


double AWGN_generator()
{/* Generates additive white Gaussian Noise samples with zero mean and a standard deviation of 1. */
 
  double temp1;
  double temp2;
  double result;
  int p;

  p = 1;

  while( p > 0 )
  {
        temp2 = ( rand() / ( (double)RAND_MAX ) ); /*  rand() function generates an
                                                       integer between 0 and  RAND_MAX,
                                                       which is defined in stdlib.h.
                                                   */

    if ( temp2 == 0 )
    {// temp2 is >= (RAND_MAX / 2)
      p = 1;
    }// end if
    else
    {// temp2 is < (RAND_MAX / 2)
       p = -1;
    }// end else

  }// end while()

  temp1 = cos( ( 2.0 * (double)PI ) * rand() / ( (double)RAND_MAX ) );
  result = sqrt( -2.0 * log( temp2 ) ) * temp1;

  return result;        // return the generated random sample to the caller

}// end AWGN_generator()