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

        
        
        public GoalKee goalkeeobj;
        public ObstacleTrejectory motionPlaning;
        public PathFollower pathFollower;
        public PathPlanner StrikerPathPlanning;
   
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

            goalkeeobj = new GoalKee(Goalkey);
            StrikerPathPlanning = new PathPlanner(noofbots, striker, goalkey, team);
            
             

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
            StrikerPathPlanning.setBlueBots(id, robotX, robotY, robotOrient);
            
        }


       
        public void setYellowBots(int id, double robotX, double robotY, double robotOrient)
        {
            yellowRobotX[id] = robotX;
            yellowRobotY[id] = robotY;
            yellowRobotOrient[id] = robotOrient;
            StrikerPathPlanning.setYellowBots(id, robotX, robotY, robotOrient);
           

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

            StrikerPathPlanning.targetX = Target_x;
            StrikerPathPlanning.targetY = Target_y;
        }
          public void PathChanger()
        {
            if (StrikerPathPlanning.newPathComplete==1)
            {
                if (i == 0)
                {
                    pathFollower.pathfollower.Start();
                    i = 1;
                }

                pathFollower.newpathIndication = 1;
                pathFollower.X = new double[StrikerPathPlanning.motionPlaning.x.Length];
                pathFollower.Y = new double[StrikerPathPlanning.motionPlaning.x.Length];
                pathFollower.X = StrikerPathPlanning.motionPlaning.x;
                pathFollower.Y = StrikerPathPlanning.motionPlaning.y;
                pathFollower.robotX = blueRobotX[Striker];
                pathFollower.robotY = blueRobotY[Striker];
                pathFollower.robotOrientation = blueRobotOrient[Striker];
                pathFollower.TotalDistance = StrikerPathPlanning.motionPlaning.Totaldistance;

                StrikerPathPlanning.newPathComplete = 0;
               
            }
           
        }
       



    }
}

