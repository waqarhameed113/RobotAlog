using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboSoccer
{
    public class strategic
    {
        Calculation cal;
        Skills kicking;
        public double[] X, Y,XRev;
        public double[] OpponentX, OpponentY;
        public double RobotX, RobotY,BallX,Bally;
        double distance;
        public int myGoalKee,targetGoal;
        public int i = 0;
        public strategic(int goalkeePosition)
        {
        //    X = new double[8] { -1000,-800,-500,-200,-100, 100,500,1000  }; 
          //  Y = new double[8] { 500, 500, 500, 500, 500, 500, 500, 500};
            RobotX = new double();
            RobotY = new double();
            cal = new Calculation();
            myGoalKee = goalkeePosition;
            targetGoal = -goalkeePosition;
            kicking = new Skills();

        }

        public void Getopponent(double[] opponentX, double[] opponentY)
        {
            OpponentX = opponentX;
            OpponentY = opponentY;
            kicking.getData(opponentX, opponentY);
        }
        public void GetStredgy (double[] x, double[] y)
        {
            X = x;
            Y = y;
            XRev =new double[x.Length];
            for (int i = 0; i < x.Length; i++)
                XRev[i] = x[i] * -1;
        }
        public void Stregedy1()
        {
            if (RobotY<0)
            {
                for (int i = 0; i < Y.Length; i++)
                {
                    Y[i] = Y[i] * -1;
                }
            }
           
            
         
            if (i<=Y.Length)
            distance = cal.Distances(Y[i], X[i], RobotY, RobotX);
            
            if (distance< 150 )
            {
               
               i++;
               
                    
            }
        
        }

      


    }

    
}
