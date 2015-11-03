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
        public double[] X, Y;
        public double RobotX, RobotY;
        double distance;
        public int i = 0;
        public strategic()
        {
            X = new double[3] { -1700 ,- 902, -2600  }; 
            Y = new double[3] { -1000, 274, 1700};
            RobotX = new double();
            RobotY = new double();
            cal = new Calculation();

        }

        public void Stregedy1()
        {

            distance = cal.Distances(Y[i], X[i], RobotY, RobotX);
            
            if (distance <500 )
            {
                i++;
                if (i == 3)
                    i = 0;
            }
        }


    }

    
}
