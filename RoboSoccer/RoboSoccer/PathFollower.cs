﻿using System;
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
        public int newpathIndication,speed;
        Calculation cal;
        public int i = 1;
        int prei;
        PID pid;

        public PathFollower()
            {
            pid = new PID();
            pathfollower = new Thread(pathRunner);
         //   pathfollower.Start();
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

                if (pointDistance < 100 && i < X.Length-1)
                {
                    i++;

                }
                
                pointDistance = cal.Distances(Y[i], X[i], robotX, robotY);

                angle = cal.Angle(Y[i], X[i], robotY, robotX, robotOrientation);
                if (TotalDistance <= 100)
                    speed = (int)pid.PID_Output(0.05, 0.015, 0.0001, TotalDistance, 1);
                else
                    speed = (int)pid.PID_Output(0.08, 0.015, 0.0001, TotalDistance, 0);

                prei = i;
                Thread.Sleep(20);

            }
        }
    }
}
