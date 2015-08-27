﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace RoboSoccer
{
    public class PathPlanner
    {
        public double ballx, bally;
        public int Striker, Goalkey, Blueteam;

        public int noBlueBots;
        public double[] blueRobotX, blueRobotY, blueRobotOrient;

        public int noYellowBots;
        public double[] yellowRobotX, yellowRobotY, yellowRobotOrient;
        public int pathcomplete = 0;


        public int BluerobotInField;
        public int YellowrobotInField;


        public PathPlanning pointPlaning;
        public ObstacleTrejectory motionPlaning;
        public PathFollower trajectory;
        public Thread abc;

           public PathPlanner(int noofbots, int striker, int goalkey, int team)
                {
            blueRobotX = new double[noofbots];
            blueRobotY = new double[noofbots];
            blueRobotOrient = new double[noofbots];
            yellowRobotX = new double[noofbots];
            yellowRobotY = new double[noofbots];
            yellowRobotOrient = new double[noofbots];
            Striker = striker;
            Goalkey = goalkey;
            Blueteam = team;
            trajectory = new PathFollower();
            motionPlaning = new ObstacleTrejectory();
            abc = new Thread(pathDraw);
            abc.Start();
        }


        public void setBall(double _ballx, double _bally)
        {
            ballx = _ballx; bally = _bally;
        }

        public void setBlueBots(int id, double robotX, double robotY, double robotOrient)
        {
            blueRobotX[id] = robotX;
            blueRobotY[id] = robotY;
            blueRobotOrient[id] = robotOrient;
        }



        public void setYellowBots(int id, double robotX, double robotY, double robotOrient)
        {
            yellowRobotX[id] = robotX;
            yellowRobotY[id] = robotY;
            yellowRobotOrient[id] = robotOrient;


        }



     

        public void pathDraw()
        {                     
            Thread.Sleep(3000);
            while (true)
            {

                //  motion.PathFinding(bally, ballx, BluerobotY[Striker], BluerobotX[Striker], BluerobotOrient[Striker], BluerobotY[Goalkee], BluerobotX[Goalkee], (pakt.detection.robots_blue.Count + pakt.detection.robots_yellow.Count));
                if (Blueteam == 1)
                {
                    motionPlaning.PathFinding(Blueteam, bally, ballx, blueRobotY, blueRobotX, blueRobotOrient, Striker, yellowRobotY, yellowRobotX, BluerobotInField + YellowrobotInField);
                }
                else
                    motionPlaning.PathFinding(Blueteam, bally, ballx, yellowRobotY, yellowRobotX, yellowRobotOrient, Striker, blueRobotY, blueRobotX, BluerobotInField + YellowrobotInField);

               
                pathcomplete = 1;
                Thread.Sleep(200);
            }
        }

    }
}