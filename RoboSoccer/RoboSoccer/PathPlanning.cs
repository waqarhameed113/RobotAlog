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
        //   int[] x = { -2400, -600, -2400 };
        // int[] y = { -1400, 0, 1400 };
        public int[] x;
        public int[] y;
        public  double FinalDistance;
        public double FinalAngle;
        public int Route  ,preRoute;
        public int speed,index;
        public int changepath;
        Calculation cal;
        PID pid;
        ObstacleTrejectory Newpath;
        public double[] Xpoint;
        public double[] Ypoint;
        public double Rob_P_dist;
        public int pathcomplete = new int();

        public PathPlanning()
        {
            changepath = new int();
            FinalDistance = new double();
            FinalAngle = new double();
            cal = new Calculation();
            pid =new PID();
            Newpath = new ObstacleTrejectory();
            changepath = 1;
            Rob_P_dist = new double();
            index = new int();
            pathcomplete = 1;
            x = new int[360];
            y = new int[360];

            for (int i=0; i<360;i++)
            {
                x[i] =(int) (-1700 + 800 * Math.Cos((i+180) * Math.PI / 180));
                y[i] = (int)(0 + 800 * Math.Sin((i+180) * Math.PI / 180));
            }

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
        public double LinearAngualPlanning(double _robot_X, double _robot_Y, double _orient, double target_X, double target_Y, double O_robot_x, double O_robot_y, double O_balldistance)
        {




            return FinalAngle;
        }

        public double pointPlaning(double _robot_X, double _robot_Y, double _orient)
        {
           
         
            if (FinalDistance < 300)
                Route++;           //route change
            if (Route > x.Length - 1)
                Route = 0;         //reset route


            FinalDistance = cal.Distances(y[Route], x[Route], _robot_Y, _robot_X);

            FinalAngle = cal.Angle(y[Route], x[Route], _robot_Y, _robot_X, _orient);

            if (Route != preRoute)
                speed = (int)pid.PID_Output(0.3, 0.015, 0.0001, FinalDistance, 1);
            else
                speed = (int)pid.PID_Output(0.3, 0.015, 0.0001, FinalDistance, 0);

            preRoute = Route;
            
            return FinalAngle;
        }

        public double pointPlaning(double _robot_X, double _robot_Y, double _orient, double _ball_x,double _ball_y,double O_robot_x,double O_robot_y,double O_balldistance,int newT)
        {


            // Newpath.newPath(500, -1700, -50, 1200,  -1700 ,-1600,_robot_X,_robot_Y,_orient);
            //Newpath.newPath(600, O_robot_x, O_robot_y, O_balldistance, _ball_x, _ball_y, _robot_X, _robot_Y, _orient);
            if (changepath == 1 || Newpath.Angle.Count == 0 || newT==1)
            { 
                Newpath.Angle.Clear();
            Newpath.Point_distance.Clear();
                Newpath.newPath(1000, O_robot_x, O_robot_y, O_balldistance, _ball_x, _ball_y, _robot_X, _robot_Y, _orient);
                changepath = 0;
                index = 0;
                Xpoint = new double[Newpath.Angle.Count];
                Ypoint = new double[Newpath.Angle.Count];

                for (int i = 0; i < Newpath.Angle.Count ; i++) 
                {
                    Xpoint[i] = Newpath.x[i];
                    Ypoint[i] = Newpath.y[i];
                }
            }
            
            FinalAngle = (double)Newpath.Angle.Peek();
            FinalDistance = (double)Newpath.Point_distance.Peek();
             Rob_P_dist = cal.Distances(Ypoint[index], Xpoint[index], _robot_Y, _robot_X);
            FinalAngle = cal.Angle(Ypoint[index], Xpoint[index], _robot_Y, _robot_X,_orient);

            if (Rob_P_dist<800)
            {
                index++;
                Newpath.Angle.Dequeue();
                Newpath.Point_distance.Dequeue();
            }

             if (index != Route)
            speed = (int)pid.PID_Output(0.15, 0.015, 0.0001, FinalDistance, 1);
             else
                speed = (int)pid.PID_Output(0.15, 0.015, 0.0001, FinalDistance, 0);
            Route = index;
            if (Newpath.Angle.Count == 0)
                pathcomplete = 1;
            else
                pathcomplete = 0;

            return FinalAngle;


        }


    }


        }





