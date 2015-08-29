using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace RoboSoccer
{
    public class Control
    {
        public double Target_x, Target_y;
        public double ball_x, ball_y;
        public  int Striker,Goalkey,Blueteam;

        public int noBlueBots;
        public double[] blueRobotX, blueRobotY, blueRobotOrient;

        public int noYellowBots;
        public double[] yellowRobotX, yellowRobotY, yellowRobotOrient;


        int i = 0;
        public int BluerobotInField;
        public int YellowrobotInField;

        
        
        
        public ObstacleTrejectory motionPlaning;
        public PathFollower pathFollower;
        public PathPlanner drawPath;
        public strategic Strategic;
        
        public Control(int noofbots,int striker,int goalkey,int team)
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
            pathFollower = new PathFollower();
            Strategic = new strategic();
            
            drawPath = new PathPlanner(noofbots, striker, goalkey, team);
             

        }

       
          public void setBall(double _ballx,double _bally)
        {
            ball_x = _ballx; ball_y = _bally;

         //   drawPath.setBall(ball_x, ball_y);
            
        }
      
        public void setBlueBots(int id,double robotX,double robotY,double robotOrient)
        {
            blueRobotX[id] = robotX;
            blueRobotY[id] = robotY;
            blueRobotOrient[id] = robotOrient;
            drawPath.setBlueBots(id, robotX, robotY, robotOrient);
        }


       
        public void setYellowBots(int id, double robotX, double robotY, double robotOrient)
        {
            yellowRobotX[id] = robotX;
            yellowRobotY[id] = robotY;
            yellowRobotOrient[id] = robotOrient;
            drawPath.setYellowBots(id, robotX, robotY, robotOrient);

        }



        public void botsInField(int blue ,int yellow)
        {
            BluerobotInField = blue;
            YellowrobotInField = yellow;
           
        }

        public void SetTarget()
        {
            Strategic.RobotX = blueRobotX[Striker];
            Strategic.RobotY = blueRobotY[Striker];
            Strategic.Stregedy1();
            Target_x = Strategic.X[Strategic.i];
            Target_y = Strategic.Y[Strategic.i];

            drawPath.targetX = Target_x;
            drawPath.targetY = Target_y;
        }
          public void PathChanger()
        {
            if (drawPath.newPathComplete==1)
            {
                if (i == 0)
                {
                    pathFollower.pathfollower.Start();
                    i = 1;
                }

                pathFollower.newpathIndication = 1;
                pathFollower.X = new double[drawPath.motionPlaning.x.Length];
                pathFollower.Y = new double[drawPath.motionPlaning.x.Length];
                pathFollower.X = drawPath.motionPlaning.x;
                pathFollower.Y = drawPath.motionPlaning.y;
                pathFollower.robotX = blueRobotX[Striker];
                pathFollower.robotY = blueRobotY[Striker];
                pathFollower.robotOrientation = blueRobotOrient[Striker];
                pathFollower.TotalDistance = drawPath.motionPlaning.Totaldistance;

                drawPath.newPathComplete = 0;
               
            }
           
        }
       



    }
}

