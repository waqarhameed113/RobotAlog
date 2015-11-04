using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace RoboSoccer
{
    public  class PathFollower
    {
        public double[] X, Y;
        public double robotX, robotY,robotOrientation,TotalDistance,pointDistance,angle;
        public Thread pathfollower;
        public int newpathIndication,speed,angularSpeed;
        public double angularError;
        
        Calculation cal;
        public int i = 1;
       
        PID pid;
        PID angular;

        public PathFollower()
            {
            pid = new PID();
            pathfollower = new Thread(pathRunner);
            angular = new PID();
            cal = new Calculation();
            
            
            }


        public void pathRunner()
        {
            Thread.Sleep(5000);
            while (true)
            {
                if (newpathIndication == 1)
                {
                   i = 1;
                    newpathIndication = 0;
                }

                if (pointDistance < 600 && i < X.Length-1)
                {
                    i++;
                    pointDistance = cal.Distances(Y[i], X[i], robotX, robotY);


                    angle = cal.Angle(Y[i], X[i], robotY, robotX, robotOrientation);
                }
               
                   
                if (TotalDistance < 200)
                    speed = (int)pid.PID_Output(1, 3, 0.0001, TotalDistance, 1,45);
                else
                    speed = (int)pid.PID_Output(0.1, 0.3, 0.0001, TotalDistance, 0,45);

                angularError = 100000 / TotalDistance;
                    if (angularError>350)
                angularSpeed = (int)angular.PID_Output(0.01, 0.009, 0.0001, angularError, 1,35);
                     else
                    angularSpeed = (int)angular.PID_Output(0.1,0.09, 0.0001, angularError, 0,35);

                    
                Thread.Sleep(8);

            }
        }
    }
}
