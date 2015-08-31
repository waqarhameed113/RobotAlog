using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboSoccer
{
    public class GoalKee
    {
        double ballx, bally, goalkeyX, goalKeyY, distance, angle;
        double[] opponentX, opponentY;             
        int index;
        int limit = 700;
        
        Calculation cal;

        public GoalKee(int index__)
        {
            index = index__;
            cal = new Calculation();
            goalkeyX = new double();
            goalKeyY = new double();
            goalkeyX = -2700;
        }
        public void calculateY()
        {
            angle = cal.Angle(bally, ballx, opponentY[index], opponentX[index]);
            goalKeyY = (opponentX[index] - ballx) * Math.Tan(angle) + goalKeyY;
        }

        public void goalkeeper(double ball_x,double ball_y,double[] my_robotx , double[] my_roboty,double[] opponectx,double[] opponenty)
        {
            ballx = ball_x;bally = ball_y;   opponentX = opponectx; opponentY = opponenty;
            
            for (int i = 0; i < opponectx.Length; i++)
            {
                distance = cal.Distances(bally, ballx, opponentY[index], opponentX[index]);
                if (distance < 500)
                {
                    calculateY();
                }
                else
                {
                    if (ball_y<limit&& ball_y>-limit)
                    goalKeyY = ball_y;
                  
                }
            }

        }
    }
}
