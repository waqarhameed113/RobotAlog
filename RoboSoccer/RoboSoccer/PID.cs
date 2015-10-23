using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboSoccer
{
    public class PID
    {
        public double error, KP, KI, KD, Input, Output,Setpoint,errorSum,Lasterror;
        public PID ()
            {
            error = new double();
            KP = new double();
            KI = new double();
            KD = new double();
            Output = new double();
            Input = new double();
            Setpoint = new double();
            errorSum = new double();
            Lasterror = new double();
            }
        public double PID_Output(double _kp,double _ki, double _kd,double _distance,int  _new)

        {
            double diff;
            KP = _kp;
            KI = _ki;
            KD = _kd;
            if (_new==1)
            {
                error = 0;
                errorSum = 0;
                diff = 0;
            }
            error = _distance;
            errorSum += error*0.01;
            diff = (error - Lasterror)/0.01;

            Output = (KP * error) + (KI * errorSum )+ (KD * diff);
            Lasterror = error;
            if (Output >= 45)
                Output = 45;
            else if (Output < 0)
                Output = 0;
            return Output;
        }
    }
}
