using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboSoccer
{
    class Control
    {
        public double ballx, bally, R2Rdistance;
        public double[] robotx, roboty, robotorient, balldistance, ballangle, desire_Angle, desire_distance;
        public int[] speed, desire_speed;
        public PathPlanning pointPlaning;
        public int routeNo;
        public int trejectory = 0;

        public Control()
        {
            ballx = new double(); bally = new double(); robotx = new double[2]; roboty = new double[2]; robotorient = new double[2];
            balldistance = new double[2]; ballangle = new double[2]; speed = new int[2]; R2Rdistance = new double(); desire_Angle = new double[2];
            desire_distance = new double[2]; routeNo = new int(); desire_speed = new int[2];
            pointPlaning = new PathPlanning();

        }

        public void setvalue(double _ballx, double _bally, int Player, double _robotx, double _roboty, double _robotorient, double _ballangle, double _balldistance, int _speed, double bot_botdistance)
        {

            ballx = _ballx; bally = _bally; robotx[Player] = _robotx; roboty[Player] = _roboty; robotorient[Player] = _robotorient;
            balldistance[Player] = _balldistance; ballangle[Player] = _ballangle; speed[Player] = _speed;
            R2Rdistance = bot_botdistance;
        }

        public void pathDecision()
        {



            if ((R2Rdistance < 1000 && balldistance[1] > balldistance[0] && R2Rdistance > 0)|| pointPlaning.pathcomplete==0)
            {
                desire_Angle[1] = pointPlaning.pointPlaning(robotx[1], roboty[1], robotorient[1], ballx, bally, robotx[0], roboty[0], balldistance[0], trejectory);
               
                desire_distance[1] = pointPlaning.FinalDistance;
                desire_speed[1] = pointPlaning.speed;
                routeNo = pointPlaning.Route;
                trejectory = 0;
            }
            else
            {
                //  desire_Angle[1] = pointPlaning.pointPlaning(robotx[1], roboty[1], robotorient[1]);

                desire_Angle[1] = ballangle[1];
                desire_speed[1] = speed[1];
                desire_distance[1] = balldistance[1];
                trejectory = 1;
            }

        }



    }
}

