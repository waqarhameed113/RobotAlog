using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Math;

 

namespace RoboSoccer
{
    public class KalmanFilter
    {
       double dt = 0.4;
        double u = 0;
        double position = 1;
        double velocity = 0;
        public double[,] XT, tempX, tempPCT, AP, APAT,CT ,ZT,K,XTT,A,B,C,EX,AT,PT,CPCT,tempK;
               public double processNoise = 0.5;
        public double measureNoise = 20;
       public   double S_;
         public double input = 0;
        
        public double error;
        public double I_KC;
        //public double TimeOn, DTime;


        public KalmanFilter()
        {
            XT = new double[2, 1] { { position }, { velocity } };
            tempX = new double[2, 1] { { 0 }, { 0 } };
            tempPCT = new double[2, 1];
            AP = new double[2, 2];
            APAT = new double[2, 2];
            CPCT = new double[2, 1];
            CT = new double[2, 1] { { 2 }, { 1 } };
            ZT = new double[2,1];
            K = new double[2, 1] { { 0.1 }, { 0 } };
            tempK = new double[2, 1];
            A = new double[2, 2] { { 1, dt }, { 0, 1 } }; ;
            B = new double[2, 1] { { u * Math.Pow(dt, 2) / 2 }, { u * dt } }; ;
            C = new double[1, 2] { { 1,0} };
            EX = new double[2, 2] { { Math.Pow(processNoise, 2) * (Math.Pow(dt, 4) / 4), Math.Pow(processNoise, 2) * (Math.Pow(dt, 3) / 2) }, { Math.Pow(processNoise, 2) * (Math.Pow(dt, 3) / 2), Math.Pow(processNoise, 2) * (Math.Pow(dt, 2)) } }; ;
            AT = new double[2, 2] { { 1, 0 }, { dt, 1 } }; ;
            PT = new double[2, 2];
            
           
              }

        public double KALMANOUT(double Input)
        {


            A[0, 0] = 1;
            A[0,1]= dt;
            A[1, 0] = 0;
            A[1, 1] = 1;//{ { 1, dt }, { 0, 1 } };
            B[0, 0] = u * Math.Pow(dt, 2) / 2;
            B[1, 0] = u * dt;
            C[0, 0] = 1;
            C[0, 1] = 0;
       //     XT[0, 0] = position;
         //   XT[1, 0] = velocity;
             // double[,] B = { { u * Math.Pow(dt, 2) / 2 }, { u * dt } };
            //  double[,] C = { { 1 } , { 0 } };
            //       double[,] XT= { { position }, { velocity} };
        //     double[,] EX = { { Math.Pow(processNoise, 2) * (Math.Pow(dt, 4) / 4), Math.Pow(processNoise, 2) * (Math.Pow(dt, 3) / 2) }, { Math.Pow(processNoise, 2) * (Math.Pow(dt, 3) / 2), Math.Pow(processNoise, 2) * (Math.Pow(dt, 2)) } }; ;
        //    double[,] AT = { { 1, 0 }, { dt, 1 } };

            input = Input;
            A.Multiply(XT, tempX);
            //      double[,] tempX=Accord.Math.Matrix.Multiply(A, XT);
           XT= tempX.Add(B);
            //   double[,] XT=Accord.Math.Matrix.Add(tempX, B);
            C.Multiply(XT, ZT);
            A.Multiply(PT, AP);
            AP.Multiply(AT, APAT);
            PT=APAT.Add(EX);
            PT.Multiply(CT, tempPCT);
            C.Multiply(tempPCT, CPCT);
            S_ = 1 / (CPCT[0, 0] + measureNoise);
            tempPCT.Multiply(S_,K);

            error = Input - ZT[0, 0];
            K.Multiply(error,tempK);
            XTT = XT.Add(tempK);
            XT = XTT;
            position = XT[0, 0];
            velocity = XT[1, 0];

            I_KC = 1 - K[0,0];
            PT.Multiply(I_KC, PT);


            return XT[0, 0];
           
                
        }         
       
    }   
}

                           