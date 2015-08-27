using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Threading;
namespace RoboSoccer
{
    public class Control
    {
        public double ballx, bally;
        public  int Striker,Goalkey,Blueteam;

        public int noBlueBots;
        public double[] blueRobotX, blueRobotY, blueRobotOrient;

        public int noYellowBots;
        public double[] yellowRobotX, yellowRobotY, yellowRobotOrient;


      
        public int BluerobotInField;
        public int YellowrobotInField;

        
        
        public PathPlanning pointPlaning;
        public ObstacleTrejectory motionPlaning;
        public PathFollower pathFollower;
        public PathPlanner drawPath;
        
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
            motionPlaning = new ObstacleTrejectory();
            drawPath = new PathPlanner(noofbots, striker, goalkey, team);
          

        }

       
          public void setBall(double _ballx,double _bally)
        {
            ballx = _ballx; bally = _bally;

            drawPath.setBall(ballx, bally);
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


       



    }
}

