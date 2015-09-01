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


        
        public int BluerobotInField;
        public int YellowrobotInField;

        
        
        
    
         
        public PathPlanner StrikerPlan;
        public PathPlanner GoalkeePlan;
        public strategic Strategic;
        public GoalKee goaler;
        
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
            goaler = new GoalKee(goalkey,-2600);
            Strategic = new strategic();

            
            StrikerPlan = new PathPlanner(noofbots, striker,  team,goalkey);
            GoalkeePlan = new PathPlanner(noofbots, goalkey, team,goalkey);
            
             

        }

       
          public void setBall(double _ballx,double _bally)
        {
            ball_x = _ballx; ball_y = _bally;

            goaler.ballx = ball_x;
            goaler.bally = ball_y;

            StrikerPlan.setBall(ball_x, ball_y);

            GoalkeePlan.setBall(ball_x, ball_y);

        }
      
        public void setBlueBots(int id,double robotX,double robotY,double robotOrient)
        {
            blueRobotX[id] = robotX;
            blueRobotY[id] = robotY;
            blueRobotOrient[id] = robotOrient;

            

            StrikerPlan.setBlueBots(id, robotX, robotY, robotOrient);

            GoalkeePlan.setBlueBots(id, robotX, robotY, robotOrient);
        }


       
        public void setYellowBots(int id, double robotX, double robotY, double robotOrient)
        {
            yellowRobotX[id] = robotX;
            yellowRobotY[id] = robotY;
            yellowRobotOrient[id] = robotOrient;
            StrikerPlan.setYellowBots(id, robotX, robotY, robotOrient);
            GoalkeePlan.setYellowBots(id, robotX, robotY, robotOrient);
        }



        public void botsInField(int blue ,int yellow)
        {
            BluerobotInField = blue;
            YellowrobotInField = yellow;
           
        }
        public void update()
        {
            if (Blueteam == 1)
            {
                goaler.opponentX = yellowRobotX;
                goaler.opponentY = yellowRobotY;
            }
            else
            {
                goaler.opponentX = blueRobotX;
                goaler.opponentY = blueRobotY;
            }

            goaler.goalkeeper();
            GoalkeePlan.setTarget(goaler.goalKeyY, goaler.goalkeyX);
        }
        public void SetTarget()
        {
            Strategic.RobotX = blueRobotX[Striker];
            Strategic.RobotY = blueRobotY[Striker];
            Strategic.Stregedy1();
            Target_x = Strategic.X[Strategic.i];
            Target_y = Strategic.Y[Strategic.i];

            StrikerPlan.targetX = Target_x;
            StrikerPlan.targetY = Target_y;
        }
         
           
        }
       



    }


