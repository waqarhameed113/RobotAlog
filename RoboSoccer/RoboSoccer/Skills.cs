using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboSoccer
{
    public class Skills
    {
        double[] opponentX, opponentY;
        public Skills()
        {

        }
        public void getData(double[] X, double[] Y)
            {
            opponentX = X;
            opponentY = Y;

            }

    }
}
