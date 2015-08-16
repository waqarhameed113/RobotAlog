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
        public double[] x;
        public double[] y;

        public ObstacleTrejectory()
        {
            XCC = new Stack();
            YCC = new Stack();
            Angle = new Queue();
            Point_distance = new Queue();
            cal = new Calculation();
            
                
        }

        public void  newPath(double _radius,double _OBX, double _OBY,double _O_balldistance,double __ballx,double __bally,double __robotx,double __roboty,double __Orient)
        {
            int angle =  (int)cal.Angle(_OBY, _OBX, __roboty, __robotx) +180;
            int test = new int();
            double PointDistanceball = new double();
            
            PointDistanceball = 15000;
            double PointAngle = new double();
            int loopout = 0;

            test = ShortestPath(_OBX, _OBY, _radius,__ballx,__bally,angle);


            while (_O_balldistance<PointDistanceball)
            {
                loopout++;

                if (test == 1)
                {
                    angle +=20;

                    XCC.Push((_OBX + _radius * Math.Cos(angle * Math.PI / 180)));
                    YCC.Push((_OBY + _radius * Math.Sin(angle * Math.PI / 180)));


                    PointDistanceball = cal.Distances(__bally, __ballx, (double)YCC.Peek(), (double)XCC.Peek());

                    PointAngle = cal.Angle((double)YCC.Peek(), (double)XCC.Peek(), __roboty, __robotx, __Orient);

                    Point_distance.Enqueue(PointDistanceball);
                    Angle.Enqueue(PointAngle);
                }
                else
                {
                    angle -= 20;

                    XCC.Push((_OBX + _radius * Math.Cos(angle * Math.PI / 180)));
                    YCC.Push((_OBY + _radius * Math.Sin(angle * Math.PI / 180)));


                    PointDistanceball = cal.Distances(__bally, __ballx, (double)YCC.Peek(), (double)XCC.Peek());

                    PointAngle = cal.Angle((double)YCC.Peek(), (double)XCC.Peek(), __roboty, __robotx, __Orient);
                  
                    Point_distance.Enqueue(PointDistanceball);
                    Angle.Enqueue(PointAngle);
                }

                if (loopout > 10)
                    break;
            }
              x = new double[XCC.Count];
             y = new double[YCC.Count];
            for (int i=XCC.Count-1;i>=0;i--)
            {
                x[i] = (double)XCC.Peek();
                y[i] = (double)YCC.Peek();
                XCC.Pop();
                YCC.Pop();

            }

            XCC.Clear();
            YCC.Clear();

           
            }
        public int ShortestPath(double obx, double oby, double radius,double ballx, double bally , int _angle)
        {
            double distance1 = new double();
            double distance2 = new double();
            int angle = _angle;
            for (int i= 0; i < 10; i++)
            {
                angle += 15;
                XCC.Push((obx + radius * Math.Cos(angle * Math.PI / 180)));
                YCC.Push((oby + radius * Math.Sin(angle * Math.PI / 180)));
                distance1 += cal.Distances(bally, ballx, (double)YCC.Peek(), (double)XCC.Peek());
            }
            angle = 0;
            for (int i = 0; i < 10; i++)
            {
                angle -= 15;
                XCC.Push((obx + radius * Math.Cos(angle * Math.PI / 180)));
                YCC.Push((oby + radius * Math.Sin(angle * Math.PI / 180)));
                distance2 += cal.Distances(bally, ballx, (double)YCC.Peek(), (double)XCC.Peek());
            }
            XCC.Clear();
            YCC.Clear();

            if (distance1 < distance2)
                return 1;
            else
                return 0;
            
            
        }

          
        }
        
    }

