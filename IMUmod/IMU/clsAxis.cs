using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IMU
{
    class clsAxis
    {
       public short Kp_attitude, Ki_attitude, Kd_attitude;
       public short attitude_feedback;
       public short attitude_command;
       public short attitude_pid_out;
       public short attitude_error;
       public short attitude_windupGuard;

       public short Kp_rate, Ki_rate, Kd_rate;
       public short rate_feedback;
       public short rate_command;
       public short rate_pid_out;
       public short rate_error;
       public short rate_windupGuard;
       public short previousRateError0;

       public short attitude_total_error;
       public short rate_total_error;
       public short previousError0;
       public short previousError1;
       public short previousError2;
       public short previousPosition0;
       public short previousPosition1;
       public short previousPosition2;
       public short thrust_cmd;

    }
}
