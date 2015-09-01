using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboSoccer
{
    public class GoalKee
    {
        public double ballx, bally, goalkeyX, goalKeyY, distance, angle;
        public double[] opponentX, opponentY;
        public int index;
        public int limit = 350;
        
        Calculation cal;

        public GoalKee(int index__,double goalX)
        {
            index = index__;
            cal = new Calculation();
            goalkeyX = new double();
            goalKeyY = new double();
            goalkeyX = goalX;
            
        }
        public void calculateY(int a)
        {
            angle = cal.Angle(bally, ballx, opponentY[a], opponentX[a]);
            goalKeyY = (goalkeyX - ballx) * Math.Tan(angle*Math.PI/180) +  bally;
            
            
             
                
            
        }

        public void goalkeeper()
        {
            
            
            for (int i = 0; i <opponentX.Length; i++)
            {
                distance = cal.Distances(bally, ballx, opponentY[i], opponentX[i]);
                if (distance < 500)
                {
                    calculateY(i);
                }
                
            }

            if (!( goalKeyY<limit && goalKeyY > -limit))
            {
                goalKeyY = bally;
            }

        }
    }
}
