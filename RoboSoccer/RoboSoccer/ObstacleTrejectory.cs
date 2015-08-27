using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Threading;

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
        public const int stepsize = 100;
        public double angle;
        public double Totaldistance;
        public int Xstep, Ystep;
        int obstacleBlue, obstacleYellow;
        int[] doneBlue, doneYellow;
        double[] P_O_B, P_O_Y;
        double[] obstacal_targetdistance;
        double distanceabc;
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
     //     newPath(400,index,striker, __robotx, __roboty, obstacal_targetdistance[i], target_x, target_y, __robotx[striker], __roboty[striker], _robotOrient[striker]);
        public void  newPath(double _radius, int index, int striker,double[] _OBX, double[] _OBY,double _O_balldistance,double __ballx,double __bally,double __robotx,double __roboty)
        {
            int angle =  (int)cal.Angle(_OBY[index], _OBX[index], __roboty, __robotx) +180;
            int test = new int();
            double PointDistanceball = new double();
            double point_otherObstacle_distance;
            PointDistanceball = 15000;
            doneBlue = new int[_OBX.Length];
          
            int loopout = 0;

            test = ShortestPath(_OBX[index], _OBY[index], _radius,__ballx,__bally,angle,__robotx,__roboty);


            while (_O_balldistance<PointDistanceball)
            {
                loopout++;

                if (test == 1)
                {
                    angle +=20;

                    XCC.Push((_OBX[index] + _radius * Math.Cos(angle * Math.PI / 180)));
                    YCC.Push((_OBY[index] + _radius * Math.Sin(angle * Math.PI / 180)));


                    PointDistanceball = cal.Distances(__bally, __ballx, (double)YCC.Peek(), (double)XCC.Peek());

                   // PointAngle = cal.Angle((double)YCC.Peek(), (double)XCC.Peek(), __roboty, __robotx, __Orient);
//newPath(500, i, striker, __robotx, __roboty, obstacal_targetdistance[i], target_x, target_y, __robotx[striker], __roboty[striker], _robotOrient[striker]);
                    for (int i=0;i<_OBX.Length;i++)
                    {
                        if (i!=striker && i!=index&& doneBlue[i] == 0 && _OBX[i]!=0 && _OBY[i] != 0)
                        {
                            point_otherObstacle_distance = cal.Distances(_OBY[i], _OBX[i], (double)YCC.Peek(), (double)XCC.Peek());
                            distanceabc = cal.Distances(__bally, __ballx, _OBY[i], _OBX[i]);
                                 if (point_otherObstacle_distance<500)
                            {
                                newPath(500, i, striker, _OBX, _OBY, distanceabc, __ballx, __bally, (double)XCC.Peek(), (double)YCC.Peek());
                                doneBlue[i]=1;
                                angle = (int)cal.Angle(__bally, __ballx, (double)YCC.Peek(), (double)XCC.Peek());
                                Xstep = (int)(stepsize * Math.Cos(angle * Math.PI / 180));
                                Ystep = (int)(stepsize * Math.Sin(angle * Math.PI / 180));
                                Xstep = Math.Abs(Xstep);
                                Ystep = Math.Abs(Ystep);
                            }
                            
                        }
                    }
     //               Point_distance.Enqueue(PointDistanceball);
     //               Angle.Enqueue(PointAngle);
                }
                else
                {
                    angle -= 20;

                    XCC.Push((_OBX[index] + _radius * Math.Cos(angle * Math.PI / 180)));
                    YCC.Push((_OBY[index] + _radius * Math.Sin(angle * Math.PI / 180)));


                    PointDistanceball = cal.Distances(__bally, __ballx, (double)YCC.Peek(), (double)XCC.Peek());

              //      PointAngle = cal.Angle((double)YCC.Peek(), (double)XCC.Peek(), __roboty, __robotx, __Orient);
                  
      //              Point_distance.Enqueue(PointDistanceball);
      //              Angle.Enqueue(PointAngle);
                }

                if (loopout > 10)
                    break;
            }
      }
    


        public void PathFinding(int team,double target_y, double target_x, double[] __roboty, double[] __robotx, double[] _robotOrient, int striker ,double[] O_roboty,double[] O_robotx,int robotcount)
        {

            
            if (team == 1)
            {
                obstacleBlue = __robotx.Length - 1;
                obstacleYellow = O_robotx.Length;
                 doneBlue = new int[__robotx.Length];
                 doneYellow = new int[O_robotx.Length];
                 P_O_B = new double[__robotx.Length]; // point to obstacle blue distance
                 P_O_Y = new double[O_robotx.Length]; // point to obstacle yellow distance
            //    double[] d_point_O = new double[__robotx.Length];  // distancepoint with obsticle
               obstacal_targetdistance = new double[__robotx.Length];

            }
            else
            {
                obstacleBlue = __robotx.Length;
                obstacleYellow = O_robotx.Length - 1;
                 doneBlue= new int[O_robotx.Length];
                 doneYellow = new int[__robotx.Length];
                P_O_B = new double[O_robotx.Length]; // point to obstacle blue distance
                P_O_Y = new double[__robotx.Length]; // point to obstacle yellow distance
            //    double[] d_point_O = new double[__robotx.Length];  // distancepoint with obsticle
                 obstacal_targetdistance = new double[__robotx.Length];

            }
           
           
           

            angle = cal.Angle(target_y, target_x, __roboty[striker], __robotx[striker]);
         
            
            
            int obstacal_Count=robotcount-1;
            double strikerDistance;
             Xstep = (int)(stepsize * Math.Cos(angle * Math.PI / 180));
            Ystep = (int)(stepsize * Math.Sin(angle * Math.PI / 180));
            Xstep = Math.Abs(Xstep);
            Ystep = Math.Abs(Ystep);
            double X, Y;
            X = __robotx[striker];
            Y = __roboty[striker];
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

                point_targetdistance = cal.Distances(target_y, target_x, (double)YCC.Peek(), (double)XCC.Peek());
                strikerDistance = cal.Distances(target_y, target_x, __roboty[striker], __robotx[striker]);
               



                ////////////////////////////////////////////
                //Here we get test with point with obstical//
                //////////////////////////////////////////////



                for (int i = 0;i<__robotx.Length;i++)
                {
                   
                        if (i != striker&& doneBlue[i]==0 && __robotx[i] !=0 && __roboty[i] !=0 )
                        {
                            P_O_B[i] = cal.Distances(__roboty[i], __robotx[i], (double)YCC.Peek(), (double)XCC.Peek());
                            obstacal_targetdistance[i] = cal.Distances(target_y, target_x, __roboty[i], __robotx[i]);

                        if (P_O_B[i]<500 && obstacal_targetdistance[i] < strikerDistance)
                             {
                            newPath(500,i,striker, __robotx, __roboty, obstacal_targetdistance[i], target_x, target_y, __robotx[striker], __roboty[striker]);
                            angle = cal.Angle(target_y, target_x, (double)YCC.Peek(), (double)XCC.Peek());
                            Xstep = (int)(stepsize * Math.Cos(angle * Math.PI / 180));
                            Ystep = (int)(stepsize * Math.Sin(angle * Math.PI / 180));
                            Xstep = Math.Abs(Xstep);
                            Ystep = Math.Abs(Ystep);
                            X = (double)XCC.Peek();
                            Y = (double)YCC.Peek();
                            doneBlue[i] = 1;
                             } 
                        }
                 
                }


                



                /*
                               for (int i = 0; i < __robotx.Length; i++)
                               {
                                   if (i != striker)
                                   {
                                       d_point_O[i] = cal.Distances(__roboty[i], __robotx[i], (double)YCC.Peek(), (double)XCC.Peek());
                                       obstacal_targetdistance[i] = cal.Distances(target_y, target_x, __roboty[i], __robotx[i]);
                                       arraycount++;

                                       if (d_point_O[i] < 800 && robotcount > 1 && obstacalBlue>0 && obstacal_targetdistance[i] < strikerDistance)
                                       {
                                           newPath(400, __robotx[i], __roboty[i], obstacal_targetdistance[i], target_x, target_y, __robotx[striker], __roboty[striker], _robotOrient[striker]);
                                           angle = cal.Angle(target_y, target_x, (double)YCC.Peek(), (double)XCC.Peek());
                                           Xstep = (int)(stepsize * Math.Cos(angle * Math.PI / 180));
                                           Ystep = (int)(stepsize * Math.Sin(angle * Math.PI / 180));
                                           Xstep = Math.Abs(Xstep);
                                           Ystep = Math.Abs(Ystep);
                                           X = (double)XCC.Peek();
                                           Y = (double)YCC.Peek();
                                           obstacalBlue--;

                                       }

                                   }

                               }

                               for (int i=arraycount; i<O_robotx.Length+arraycount;i++)
                               {
                                   d_point_O[i] = cal.Distances(O_roboty[i- arraycount], O_robotx[i - arraycount], (double)YCC.Peek(), (double)XCC.Peek());
                                   obstacal_targetdistance[i] = cal.Distances(target_y, target_x, O_roboty[i - arraycount], O_robotx[i - arraycount]);


                                   if (d_point_O[i] < 800 && robotcount > 1 && obstacal_Count > 1 && obstacal_targetdistance[i] < strikerDistance)
                                   {
                                       newPath(400, O_robotx[i - arraycount], O_roboty[i - arraycount], obstacal_targetdistance[i], target_x, target_y, __robotx[striker], __roboty[striker], _robotOrient[striker]);
                                       angle = cal.Angle(target_y, target_x, (double)YCC.Peek(), (double)XCC.Peek());
                                       Xstep = (int)(stepsize * Math.Cos(angle * Math.PI / 180));
                                       Ystep = (int)(stepsize * Math.Sin(angle * Math.PI / 180));
                                       Xstep = Math.Abs(Xstep);
                                       Ystep = Math.Abs(Ystep);
                                       X = (double)XCC.Peek();
                                       Y = (double)YCC.Peek();
                                       obstacal_Count--;

                                   }

                               }
                 */

             
                
        
               
                
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
                    Totaldistance = cal.Distances(y[j], x[j], __roboty[striker], __robotx[striker]);
                 else
                    Totaldistance += cal.Distances(y[j], x[j], y[j-1], x[j-1]);
            }

          



        }


        public int ShortestPath(double obx, double oby, double radius, double ballx, double bally, int _angle, double robotx, double roboty)
        {
            double distance1 = new double();
            double distance2 = new double();
            int angle = _angle;
            double x = new double();
            double y = new double();
            double preX = robotx;
            double preY = roboty;
            for (int i = 0; i < 6; i++)
            {
                angle += 15;
                x = (obx + radius * Math.Cos(angle * Math.PI / 180));
                y = (oby + radius * Math.Sin(angle * Math.PI / 180));
                distance1 += cal.Distances(bally, ballx, y, x);
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
    }
        
    }

