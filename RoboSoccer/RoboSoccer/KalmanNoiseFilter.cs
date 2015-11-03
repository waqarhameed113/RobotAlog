using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboSoccer
{
    public class KalmanNoiseFilter
    {
        double varVoltR;
        double varProcess;
        double predictCurrentOutput; // bar x(t)
        double KalmanGain ;     // K kalman Gain
        double previousState ;    // prevous State x(t-1)
        double predictedOutput ;  //  x(t)
        double estimatedOutput ; // bar z(t)
        double updatedOutput  ;      // ^xt 
        double xp, xe, zp;
        public KalmanNoiseFilter()
        {
            varVoltR = new double();
            varProcess = new double();
            predictCurrentOutput = new double();
            KalmanGain = new double();
            previousState= new double();
            predictedOutput= new double(); ;
            estimatedOutput= new double();
            updatedOutput= new double();
            xp= new double();
            xe= new double();
            zp= new double();

            varVoltR = 37.90126582;
            varProcess = 50e-2;
            previousState = 1.0;

        }

        public double K_Noise_Filter(double input)
        {
           

            predictCurrentOutput = previousState + varProcess;
            KalmanGain = predictCurrentOutput / (predictCurrentOutput + varVoltR);    //measurement noise
            previousState = (1 - KalmanGain) * predictCurrentOutput;

            xp = xe;
            zp = xp;
            xe = KalmanGain * (input - zp) + xp;
            return xe;
        }

        public double K_Noise_Filter(double input, double var,double Process)
        {


            predictCurrentOutput = previousState + Process;
            KalmanGain = predictCurrentOutput / (predictCurrentOutput + var);    //measurement noise
            previousState = (1 - KalmanGain) * predictCurrentOutput;

            xp = xe;
            zp = xp;
            xe = KalmanGain * (input - zp) + xp;
            return xe;
        }
    }
}
