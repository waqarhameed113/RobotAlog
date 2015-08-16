using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboSoccer
{
    public class Calculation
    {
        public double Distances(double y2,double x2,double y1,double x1)
        {
            double distances = 0;
            distances = Math.Sqrt((y2 - y1)* (y2 - y1) + (x2 - x1)* (x2 - x1));
           

            return distances;
        }
        public double Angle(double y2, double x2, double y1, double x1,double Orient_angle)
        {
            double angle = 0;
            angle = Math.Atan2((y2 - y1), (x2 - x1));
            angle = angle * 180 / Math.PI;
                angle = 360 + angle - Orient_angle;
            return angle;
        }
        public double Angle(double y2, double x2, double y1, double x1)
        {
            double angle = 0;
            angle = Math.Atan2((y2 - y1), (x2 - x1));
            angle = angle * 180 / Math.PI;
            angle += 360;
            
            return angle;
        }

        public double orient (double _angle)
        {
           
             if (_angle < 355 && _angle >175|| _angle >555 && _angle <715)
                return 2; /// 2 clock wise

            else if (_angle > 365 && _angle <545|| _angle>5 && _angle<165)
                return 1;    // 1 anticlock
            else
            return 0;
        }
      

}

    }

