using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboSoccer
{
    public class GoalKee
    {
        public double ballx, bally, goalkeyX, goalKeyY, angle;
        public double[] opponentX, opponentY, distance;
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
            distance = new double[2];
            
        }
        public void calculateY(int a)
        {
            angle = cal.Angle(bally, ballx, opponentY[a], opponentX[a]);
            goalKeyY = (goalkeyX - ballx) * Math.Tan(angle*Math.PI/180) +  bally;
           




        }

        public void goalkeeper()
        {


            for (int i = 0; i < opponentX.Length; i++)
            {
                distance[i] = cal.Distances(bally, ballx, opponentY[i], opponentX[i]);
            }
                if (distance[0] < 500 || distance[1] < 500)
                {
                if (distance[1] < 500)
                    calculateY(1);
                else
                    calculateY(0);
                }

                 else if (bally > limit)
                    goalKeyY = limit;
                else if (bally < (-limit))
                    goalKeyY = -limit;
                else
                    goalKeyY = bally;
            }
           

            /*    if (!( goalKeyY<limit && goalKeyY > -limit))
                {
                    goalKeyY = bally;
                }
              */
        }
    }
                                                  
