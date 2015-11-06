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
        public double angle_ball;
        public double angledifference;
        public int kick;
        public int buttonSetpoint = 0;
        
        
    
         
        public PathPlanner StrikerPlan;
        public PathPlanner GoalkeePlan;
        public strategic Strategic;
        public GoalKee goaler;
        public Calculation cal;
        double balldistance;
        
        public Control(int noofbots,int striker,int goalkey,int team,int noOfBlueRobot, int noOfYellowRobot,int myGoalkeyPosition)
        {
            blueRobotX = new double[noOfBlueRobot];
            blueRobotY = new double[noOfBlueRobot];
            blueRobotOrient = new double[noOfBlueRobot];
            yellowRobotX = new double[noOfYellowRobot];
            yellowRobotY = new double[noOfYellowRobot];
            yellowRobotOrient = new double[noOfYellowRobot];
            Striker = striker;
            Goalkey = goalkey;
            Blueteam = team;
            goaler = new GoalKee(goalkey,myGoalkeyPosition);
            Strategic = new strategic(myGoalkeyPosition );

            cal = new Calculation();
            StrikerPlan = new PathPlanner(noofbots, striker,  team,goalkey, noOfBlueRobot, noOfYellowRobot,(-myGoalkeyPosition));
            GoalkeePlan = new PathPlanner(noofbots, goalkey, team,goalkey, noOfBlueRobot, noOfYellowRobot,(-myGoalkeyPosition));
            
             

        }

       
          public void setBall(double _ballx,double _bally)
        {
            ball_x = _ballx; ball_y = _bally;

            goaler.ballx = ball_x;
            goaler.bally = ball_y;

            if (Striker==1)
            StrikerPlan.setBall(ball_x, ball_y);
            else
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
        public void updateGoalKEE()
        {
            if (Blueteam == 1)
            {
                goaler.opponentX = yellowRobotX;
                goaler.opponentY = yellowRobotY;
                Strategic.Getopponent(yellowRobotX, yellowRobotY);
               
                
            }
            else
            {
                goaler.opponentX = blueRobotX;
                goaler.opponentY = blueRobotY;
                Strategic.OpponentX = blueRobotX;
                Strategic.OpponentY = blueRobotY;
            }

            goaler.goalkeeper();
            GoalkeePlan.setTarget(goaler.goalKeyY, goaler.goalkeyX);
        }
        public void SetTarget()
        {
            if (Blueteam == 1)
            {
                Strategic.RobotX = blueRobotX[Striker];
                Strategic.RobotY = blueRobotY[Striker];
                Strategic.BallX = ball_x;
                Strategic.Bally = ball_y;
                balldistance = cal.Distances(ball_y, ball_x, blueRobotY[Striker], blueRobotX[Striker]);
                angle_ball = cal.Angle(ball_y, ball_x, blueRobotY[Striker], blueRobotX[Striker], "noofset");
                if (ball_x < blueRobotX[Striker])
                {
                    if (ball_y > blueRobotY[Striker])
                        angle_ball += 180;
                    else
                        angle_ball -= 180;

                }
                angledifference = angle_ball - blueRobotOrient[Striker];

                if ((balldistance < 150) && ((angledifference < 20 && angledifference > -20)))
                {

                    kick = 1;
                    Strategic.Stregedy1();
                    Target_x = Strategic.X[Strategic.i];
                    Target_y = Strategic.Y[Strategic.i];

                    StrikerPlan.targetX = Target_x;
                    StrikerPlan.targetY = Target_y;
                }
                else if (buttonSetpoint == 0)
                {
                    kick = 0;
                    StrikerPlan.targetX = ball_x;
                    StrikerPlan.targetY = ball_y;

                }

            }
            else
            {
                Strategic.RobotX = yellowRobotX[Striker];
                Strategic.RobotY = yellowRobotY[Striker];
                Strategic.BallX = ball_x;
                Strategic.Bally = ball_y;
                balldistance = cal.Distances(ball_y, ball_x, yellowRobotY[Striker], yellowRobotX[Striker]);
                angle_ball = cal.Angle(ball_y, ball_x, yellowRobotY[Striker], yellowRobotX[Striker], "noofset");
                if (ball_x < yellowRobotX[Striker])
                {
                    if (ball_y > yellowRobotY[Striker])
                        angle_ball += 180;
                    else
                        angle_ball -= 180;

                }
                angledifference = angle_ball - yellowRobotOrient[Striker];

                if ((balldistance < 150) && ((angledifference < 20 && angledifference > -20)))
                {

                    kick = 1;
                    Strategic.Stregedy1();
                    Target_x = Strategic.X[Strategic.i];
                    Target_y = Strategic.Y[Strategic.i];

                    StrikerPlan.targetX = Target_x;
                    StrikerPlan.targetY = Target_y;
                }
                else if (buttonSetpoint == 0)
                {
                    kick = 0;
                    StrikerPlan.targetX = ball_x;
                    StrikerPlan.targetY = ball_y;

                }
            }



            }
         
           
        }
       



    }


