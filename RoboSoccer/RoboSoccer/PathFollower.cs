using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace RoboSoccer
{
    public class PathFollower
    {
        public double[] X, Y;
        public double robotX, robotY, robotOrientation, TotalDistance, pointDistance, angle;
        public Thread pathfollower;
        public int newpathIndication, speed;
        Calculation cal;
        public int i = 1;
        public int ID;
        public double obstacleDistabce;
        public double[] blueRobotX, blueRobotY, blueRobotOrient;


        public double[] yellowRobotX, yellowRobotY, yellowRobotOrient;
        int Team;
        PID pid;

        public PathFollower(int id, int team)
        {
            pid = new PID();
            pathfollower = new Thread(pathRunner);
            ID = id;
            cal = new Calculation();
            Team = team;

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

                if (pointDistance < 300 && i < X.Length - 1)
                {
                    i++;

                }

                pointDistance = cal.Distances(Y[i], X[i], robotX, robotY);

                angle = cal.Angle(Y[i], X[i], robotY, robotX, robotOrientation);
                if (ID == 0)
                {
                    speed = (int)pid.PID_Output(1, 0.015, 0.0001, TotalDistance, 1);
                }
                else
                {
                    if (TotalDistance > 1000)
                        speed = (int)pid.PID_Output(0.1, 0.015, 0.0005, TotalDistance, 1);
                    else
                        speed = (int)pid.PID_Output(0.015, 0.04, 0.0011, TotalDistance, 0);
                }

                if (Team == 1)
                {
                    for (int i = 0; i < blueRobotX.Length; i++)
                    {
                        if (i != ID)
                        {
                            obstacleDistabce = cal.Distances(blueRobotY[ID], blueRobotX[ID], blueRobotX[i], blueRobotY[i]);
                            if (obstacleDistabce < 1000)
                            {
                                speed = (int)pid.PID_Output(0.0015, 0.018, 0.0011, TotalDistance, 1);
                                break;
                            }
                        }

                    }


                    for (int i = 0; i < yellowRobotX.Length; i++)
                    {

                        obstacleDistabce = cal.Distances(blueRobotY[ID], blueRobotX[ID], yellowRobotX[i], yellowRobotY[i]);
                        if (obstacleDistabce < 1500)
                        {
                            speed = (int)pid.PID_Output(0.009, 0.05, 0.0011, TotalDistance, 1);
                            break;
                        }

                    }
          
                }

                else if (Team == 0)
                {
                    for (int i = 0; i < blueRobotX.Length; i++)
                    {
                       
                            obstacleDistabce = cal.Distances(yellowRobotY[ID], yellowRobotX[ID], blueRobotX[i], blueRobotY[i]);
                            if (obstacleDistabce < 900)
                            {
                                speed = (int)pid.PID_Output(0.015, 0.018, 0.0011, TotalDistance, 0);
                                break;
                            }
                        

                    }


                    for (int i = 0; i < yellowRobotX.Length; i++)
                    {
                        if (i != ID)
                        {
                            obstacleDistabce = cal.Distances(yellowRobotY[ID], yellowRobotX[ID], yellowRobotX[i], yellowRobotY[i]);
                            if (obstacleDistabce < 900)
                            {
                                speed = (int)pid.PID_Output(0.0015, 0.018, 0.0011, TotalDistance, 0);
                                break;
                            }
                        }
                    }

                }

                Thread.Sleep(10);
            }
        }
    }
}
