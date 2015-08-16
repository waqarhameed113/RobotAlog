using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace RoboSoccer
{
    public class ObstacleTrejectory
    {
       public Stack XCC ;
        public Stack YCC ;
        public Queue Angle;
        public Queue Point_distance;
        Calculation cal;

        public ObstacleTrejectory()
        {
            XCC = new Stack();
            YCC = new Stack();
            Angle = new Queue();
            Point_distance = new Queue();
            cal = new Calculation();
            
                
        }

        public void  newPath(double _radius,double _OBX, double _OBY,double M_B_angle,double _O_balldistance,double __ballx,double __bally)
        {
            int angle =(int) M_B_angle;
            double PointDistanceball = new double();
            PointDistanceball = 1500;
            int loopout = 0;
            while (_O_balldistance<PointDistanceball)
            {
                loopout++;
                
                    angle += 25;
               
                XCC.Push((_OBX + _radius * Math.Cos(angle * Math.PI / 180)));
                YCC.Push((_OBY + _radius * Math.Sin(angle * Math.PI / 180)));
                PointDistanceball = cal.Distances(__bally, __ballx,(double) YCC.Peek(),(double) XCC.Peek());
                
                Point_distance.Enqueue(PointDistanceball);
                Angle.Enqueue(angle);

                if (loopout > 20)
                    break;
            }

            XCC.Clear();
            YCC.Clear();
          
        }
        
    }
}
