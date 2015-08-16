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
        public const int stepsize = 200;
        public double angle;
        public double distance;
        public ObstacleTrejectory()
        {
            distance = new double();
            angle = new double();
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


        public void PathFinding (double target_y, double target_x,double __roboty,double __robotx)
        {
            
             angle = cal.Angle(target_y, target_x, __roboty, __robotx);
          
//          distance= cal.Distances(target_y, target_x, __roboty, __robotx);
            int Xstep= (int) (stepsize * Math.Cos(angle * Math.PI / 180));
            int Ystep = (int)(stepsize * Math.Sin(angle * Math.PI / 180));
            int count = 1;

            if ((target_x < __robotx) && Xstep !=0)
            {
                Xstep *= -1;
                while (target_x < __robotx)
                {
                    if (count == 1)
                    {
                        __robotx += Xstep;
                        XCC.Push(__robotx);
                        count = 0;
                    }
                    else
                    {
                        __robotx += Xstep;
                        XCC.Push(__robotx);
                        
                    }
                }

            }
            else  if (Xstep != 0)
            {
                while (target_x > __robotx)
                {
                    if (count == 1)
                    {
                        __robotx +=Xstep;
                        XCC.Push(__robotx);
                        count = 0;
                    }
                    else
                    {
                        __robotx += Xstep;
                        XCC.Push(__robotx);
                       
                    }
                }

            }
            count = 1;

            if (target_y<__roboty && Ystep !=0)
            {
                Ystep *= -1;
                while (target_y < __roboty)
                {
                    if (count == 1)
                    {
                        __roboty += Ystep;
                        YCC.Push(__roboty);
                        count = 0;
                    }
                    else
                    {
                        __roboty += Ystep;
                        YCC.Push((int)YCC.Pop() + Ystep);
                        
                    }
                }


            }
            else if ( Ystep != 0)
            {
                while (target_y > __roboty )
                {
                    if (count == 1)
                    {
                        __roboty += Ystep;
                        YCC.Push(__roboty);
                        count = 0;
                    }
                    else
                    {
                        __roboty += Ystep;
                        YCC.Push(__roboty);
                        
                    }
                }


            }

            if (XCC.Count <=YCC.Count)
            {
                while (XCC.Count != YCC.Count)
                {
                    XCC.Push(__robotx);
                }
            }
            else if (YCC.Count <=XCC.Count)
            {
                while (XCC.Count != YCC.Count)
                {
                    YCC.Push(__roboty);
                }
            }
            
            XCC.Push(target_x);
            YCC.Push(target_y);
            int a=XCC.Count;
            int b = YCC.Count;
            int c = 5;
            XCC.Clear();
            YCC.Clear();
        }
        
     
                  
        }
        
    }

