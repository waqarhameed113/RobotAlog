using System;
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
        public double targetX, targetY;
        public int RobotID, Goalkey, Blueteam;

        public int noBlueBots;
        public double[] blueRobotX, blueRobotY, blueRobotOrient;

        public int noYellowBots;
        public double[] yellowRobotX, yellowRobotY, yellowRobotOrient;
        public int pathcompleteforGraph = 0;


        public int BluerobotInField;
        public int YellowrobotInField;
        int i = 0;

        
        public ObstacleTrejectory motionPlaning;
        
        public Thread trejectoryPloting;
        public int newPathComplete;
        public PathFollower pathFollower;
           public PathPlanner(int noofbots, int R_ID,  int team,int GoalkeyID,int noOfBlueRobo, int noOfYellowRobo)
                {
            blueRobotX = new double[noOfBlueRobo];
            blueRobotY = new double[noOfBlueRobo];                                    
            blueRobotOrient = new double[noOfBlueRobo];    
            yellowRobotX = new double[noOfYellowRobo];
            yellowRobotY = new double[noOfYellowRobo];
            yellowRobotOrient = new double[noOfYellowRobo];
            RobotID = R_ID;
            Blueteam = team;
            pathFollower = new PathFollower();
            motionPlaning = new ObstacleTrejectory();
            trejectoryPloting = new Thread(pathDraw);
            
            
        }


        public void setBall(double target_x, double target_y)
        {
            targetX = target_x; targetY = target_y;
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


        public void setTarget(double target_y,double target_x)
        {
            targetY = target_y;
            targetX = target_x;
            


        }
        public void PathChanger()
        {
            if (newPathComplete == 1)
            {
                if (i == 0)
                {
                    pathFollower.pathfollower.Start();
                    i = 1;
                }

                pathFollower.newpathIndication = 1;
                pathFollower.X = new double[motionPlaning.x.Length];
                pathFollower.Y = new double[motionPlaning.x.Length];
                pathFollower.X = motionPlaning.x;
                pathFollower.Y = motionPlaning.y;
                if (Blueteam == 1)
                {
                    pathFollower.robotX = blueRobotX[RobotID];
                    pathFollower.robotY = blueRobotY[RobotID];
                    pathFollower.robotOrientation = blueRobotOrient[RobotID];
                }
                else
                {
                    pathFollower.robotX = yellowRobotX[RobotID];
                    pathFollower.robotY = yellowRobotY[RobotID];
                    pathFollower.robotOrientation = yellowRobotOrient[RobotID];
                }
                pathFollower.TotalDistance = motionPlaning.Totaldistance;

                newPathComplete = 0;

            }
        }

        public void pathDraw()
        {                     
            Thread.Sleep(3000);
            while (true)
            {

                //  motion.PathFinding(bally, ballx, BluerobotY[Striker], BluerobotX[Striker], BluerobotOrient[Striker], BluerobotY[Goalkee], BluerobotX[Goalkee], (pakt.detection.robots_blue.Count + pakt.detection.robots_yellow.Count));
                if (Blueteam == 1)
                {
                    motionPlaning.PathFinding(Blueteam, targetY,targetX, blueRobotY, blueRobotX, blueRobotOrient, RobotID, yellowRobotY, yellowRobotX, BluerobotInField + YellowrobotInField);
                }
                else
                    motionPlaning.PathFinding(Blueteam, targetY, targetX, yellowRobotY, yellowRobotX, yellowRobotOrient, RobotID, blueRobotY, blueRobotX, BluerobotInField + YellowrobotInField);



                newPathComplete = 1;
                pathcompleteforGraph = 1;
                Thread.Sleep(100);
            }
        }

    }
}
