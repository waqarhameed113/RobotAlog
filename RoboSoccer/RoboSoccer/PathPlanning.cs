using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RoboSoccer
{
    public class PathPlanning
    {
        int[] x = { -2400, -600, -2400};
        int[] y = {  -1400, 0, 1400 };
      public  double FinalDistance;
        public double FinalAngle,FinalOrient;
        public int Route  ,preRoute;
        public int speed;
        Calculation cal;
        PID pid;
        public PathPlanning()
            {
            FinalDistance = new double();
            FinalAngle = new double();
            cal = new Calculation();
            pid =new PID();
            }
        public double routePlaning(double _robot_X,double _robot_Y,double _distance, double _angle, double _orient)
        {
                          

               if (FinalDistance < 300)
                   Route++;           //route change
               if (Route > x.Length-1)
                   Route = 0;         //reset route
               
            
            FinalDistance = cal.Distances(y[Route], x[Route], _robot_Y, _robot_X);
            
            FinalAngle=cal.Angle(y[Route], x[Route], _robot_Y, _robot_X, _orient);

                             if(Route!=preRoute)
                speed = (int)pid.PID_Output(0.05, 0.015, 0.0001, FinalDistance, 1);
                             else
                speed = (int)pid.PID_Output(0.05, 0.015, 0.0001, FinalDistance, 0);

            preRoute = Route;
            return FinalAngle;
            
        }
          

        }
      
        
        
    }

