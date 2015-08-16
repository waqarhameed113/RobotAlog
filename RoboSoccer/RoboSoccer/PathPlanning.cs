using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace RoboSoccer
{
    public class PathPlanning
    {
        
      public  double FinalDistance;
        public double FinalAngle,FinalOrient;
        public int Route  ,preRoute;
        public int speed;
        Calculation cal;
        PID pid;
        ObstacleTrejectory Newpath;
        

        public PathPlanning()
            {
            FinalDistance = new double();
            FinalAngle = new double();
            cal = new Calculation();
            pid =new PID();
            Newpath = new ObstacleTrejectory();

            }
        public double routePlaning(double _robot_X,double _robot_Y,double _distance, double _angle, double _orient)
        {

                FinalAngle = _angle;
                if (FinalDistance!=_distance)
                speed = (int)pid.PID_Output(0.05, 0.015, 0.0001, _distance, 1);
                else
                speed = (int)pid.PID_Output(0.05, 0.015, 0.0001, FinalDistance, 0);
                FinalDistance = _distance;
            
                       
            return FinalAngle;
            
        }

        public double routePlaning(double _robot_X, double _robot_Y,double OB_Radius,double OB_X,double OB_Y,double angle_Master,double O_balldistance,double _ballx,double _bally,double r2rdistance)
        {
            
            int i = 0;
            
            Newpath.newPath(OB_Radius, OB_X, OB_Y,angle_Master,O_balldistance,_ballx,_bally);
            double[] anglearray = new double[Newpath.Angle.Count];
            double[] distance = new double[Newpath.Angle.Count];
            while (Newpath.Angle.Count>0)
            {
                anglearray[i] = (int)Newpath.Angle.Peek();
                Newpath.Angle.Dequeue();
                distance[i] = (double)Newpath.Point_distance.Peek();
                Newpath.Point_distance.Dequeue();
                i++;
            }
            Newpath.Angle.Clear();
            Newpath.Point_distance.Clear();
            if (i > 0)
            {
                FinalAngle = anglearray[0];
                FinalDistance = distance[0];
               speed = (int)pid.PID_Output(0.1, 0.115, 0.0001, FinalDistance, 1);
                if (r2rdistance < 600)
                    FinalAngle += 90;
            }

            return FinalAngle;
        }


        }



}

