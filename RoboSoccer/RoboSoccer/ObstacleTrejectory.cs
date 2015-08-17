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
   //     public Queue Angle;
   //     public Queue Point_distance;
        Calculation cal;
        public double[] x;
        public double[] y;
        public const int stepsize = 200;
        public double angle;
        public double Totaldistance;
        
        public ObstacleTrejectory()
        {
            Totaldistance = new double();
            angle = new double();
            XCC = new Stack();
            YCC = new Stack();
 //           Angle = new Queue();
   //         Point_distance = new Queue();
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

            test = ShortestPath(_OBX, _OBY, _radius,__ballx,__bally,angle,__robotx,__roboty);


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

     //               Point_distance.Enqueue(PointDistanceball);
     //               Angle.Enqueue(PointAngle);
                }
                else
                {
                    angle -= 20;

                    XCC.Push((_OBX + _radius * Math.Cos(angle * Math.PI / 180)));
                    YCC.Push((_OBY + _radius * Math.Sin(angle * Math.PI / 180)));


                    PointDistanceball = cal.Distances(__bally, __ballx, (double)YCC.Peek(), (double)XCC.Peek());

                    PointAngle = cal.Angle((double)YCC.Peek(), (double)XCC.Peek(), __roboty, __robotx, __Orient);
                  
      //              Point_distance.Enqueue(PointDistanceball);
      //              Angle.Enqueue(PointAngle);
                }

                if (loopout > 10)
                    break;
            }

            int a = 0;    


           
            }
        public int ShortestPath(double obx, double oby, double radius,double ballx, double bally , int _angle,double robotx,double roboty)
        {
            double distance1 = new double();
            double distance2 = new double();
            int angle = _angle;
            double x = new double();
            double y = new double();
            double preX=robotx;
            double preY = roboty;
            for (int i= 0; i < 6; i++)
            {
                angle += 15;
                x=(obx + radius * Math.Cos(angle * Math.PI / 180));
                y=(oby + radius * Math.Sin(angle * Math.PI / 180));
                distance1 += cal.Distances(bally, ballx,y, x);
                preX = x;
                preY = y;
            }
            
            angle = _angle;
            for (int i = 0; i < 6; i++)
            {
                angle -= 15;
                x = (obx + radius * Math.Cos(angle * Math.PI / 180));
                y = (oby + radius * Math.Sin(angle * Math.PI / 180));
                distance2 += cal.Distances(bally, ballx, y, x);
            }
              

            if (distance1 < distance2)
                return 1;
            else
                return 0;
            
            
        }


        public void PathFinding(double target_y, double target_x, double __roboty, double __robotx,double _robotOrient ,double O_roboty,double O_robotx,int robotcount)
        {
            angle = cal.Angle(target_y, target_x, __roboty, __robotx);
            //distance= cal.Distances(target_y, target_x, __roboty, __robotx);
            double d_point_O;  // distancepoint with obsticle
            double obstacal_targetdistance;
            int obstacal_Count=robotcount;
            double strikerDistance;
            int Xstep = (int)(stepsize * Math.Cos(angle * Math.PI / 180));
            int Ystep = (int)(stepsize * Math.Sin(angle * Math.PI / 180));
            Xstep = Math.Abs(Xstep);
            Ystep = Math.Abs(Ystep);
            double X, Y;
            X = __robotx;
            Y = __roboty;
            double point_targetdistance = 15000;  
           
          while (point_targetdistance>400)
            {
              if (target_x>X && target_y >Y)
                {
                    X += Xstep;
                    Y += Ystep;
                    XCC.Push(X);
                    YCC.Push(Y);

                }
              else if (X>target_x&& Y>target_y)
                {
                    X -= Xstep;
                    Y -= Ystep;
                    XCC.Push(X);
                    YCC.Push(Y);
                }
                 else if( target_x<X && Y<target_y)
                {
                    X -= Xstep;
                    Y += Ystep;
                    XCC.Push(X);
                    YCC.Push(Y);
                }
              else if (target_x>X && target_y<Y)
                {
                    X += Xstep;
                    Y -= Ystep;                                           
                    XCC.Push(X);
                    YCC.Push(Y);
                }
                ////////////////////////////////////////////
                //Here we get test with point with obstical//
                //////////////////////////////////////////////
                point_targetdistance = cal.Distances(target_y, target_x, (double)YCC.Peek(), (double)XCC.Peek());
                d_point_O = cal.Distances(O_roboty, O_robotx,(double) YCC.Peek(),(double) XCC.Peek());
                obstacal_targetdistance = cal.Distances(target_y, target_x, O_roboty, O_robotx);
                strikerDistance = cal.Distances(target_y, target_x, __roboty, __robotx);
                if (d_point_O<800 && robotcount>1&& obstacal_Count>1 && obstacal_targetdistance<strikerDistance)
                {
                    newPath(400, O_robotx, O_roboty, obstacal_targetdistance, target_x, target_y, __robotx, __roboty, _robotOrient);
                    angle = cal.Angle(target_y, target_x, (double)YCC.Peek(),(double)XCC.Peek());
                    Xstep = (int)(stepsize * Math.Cos(angle * Math.PI / 180));
                     Ystep = (int)(stepsize * Math.Sin(angle * Math.PI / 180));
                    Xstep = Math.Abs(Xstep);
                    Ystep = Math.Abs(Ystep);
                    X = (double)XCC.Peek();
                    Y = (double)YCC.Peek();
                    obstacal_Count--;
                }
               
                
            }

            XCC.Push(target_x);
            YCC.Push(target_y);
            x = new double[XCC.Count];
            y = new double[YCC.Count];
            for (int i = XCC.Count - 1; i >= 0; i--)
            {
                x[i] = (double)XCC.Peek();
                y[i] = (double)YCC.Peek();
                     
                x[i] = (int)x[i];
                y[i] = (int)y[i];
                XCC.Pop();
                YCC.Pop();

            }
            for (int j = 0;j<x.Length;j++)
            {
                if (j == 0)
                    Totaldistance = cal.Distances(y[j], x[j], __roboty, __robotx);
                 else
                    Totaldistance += cal.Distances(y[j], x[j], y[j-1], x[j-1]);
            }

            int a = 0;



        }
        
     
                  
        }
        
    }

