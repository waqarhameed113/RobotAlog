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
        public double point_targetdistance;
        public double strikerDistance;
        public Stack XCC ;
        public Stack YCC ;
   //     public Queue Angle;
   //     public Queue Point_distance;
        Calculation cal;
        public double[] x;
        public double[] y;
        public const int stepsize = 50;
        public double angle;
        public double Totaldistance;
        public int Xstep, Ystep;
        int obstacleBlue, obstacleYellow;
        int[] doneBlue, doneYellow;
        double[] P_O_B, P_O_Y;
        double obstacal_targetdistance;
        public int striker;
        double X, Y;
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
        public void  newPath(double _radius, int index, double[] _OBX, double[] _OBY,double _O_balldistance,double __ballx,double __bally,double __robotx,double __roboty)
        {
            int angle =  (int)cal.Angle(_OBY[index], _OBX[index], __roboty, __robotx) +160;
            int test = new int();
            double PointDistanceball = new double();
          
            PointDistanceball = 15000;
            doneBlue = new int[_OBX.Length];
          
            int loopout = 0;

            test = ShortestPath(index,_OBX, _OBY, _radius,__ballx,__bally,angle,__robotx,__roboty);


            while (_O_balldistance<PointDistanceball)
            {
                loopout++;

                if (test == 1)
                {
                    angle += 15;

                    XCC.Push((_OBX[index] + _radius * Math.Cos(angle * Math.PI / 180)));
                    YCC.Push((_OBY[index] + _radius * Math.Sin(angle * Math.PI / 180)));


                    PointDistanceball = cal.Distances(__bally, __ballx, (double)YCC.Peek(), (double)XCC.Peek());


                    //////////////////////////////////////////////////////////////////////////////

                  





                }
                else
                {
                    angle -= 15;

                    XCC.Push((_OBX[index] + _radius * Math.Cos(angle * Math.PI / 180)));
                    YCC.Push((_OBY[index] + _radius * Math.Sin(angle * Math.PI / 180)));


                    PointDistanceball = cal.Distances(__bally, __ballx, (double)YCC.Peek(), (double)XCC.Peek());


                }

                if (loopout > 10)
                    break;
            }
      }
    


        public void PathFinding(int team,double target_y, double target_x, double[] __roboty, double[] __robotx, double[] _robotOrient, int _striker ,double[] O_roboty,double[] O_robotx,int robotcount)
        {
            striker = _striker;
            obstacal_targetdistance = new double();
            if (team == 1)
            {
                obstacleBlue = __robotx.Length - 1;
                obstacleYellow = O_robotx.Length;
                 doneBlue = new int[__robotx.Length];
                 doneYellow = new int[O_robotx.Length];
                 P_O_B = new double[__robotx.Length]; // point to obstacle blue distance
                 P_O_Y = new double[O_robotx.Length]; // point to obstacle yellow distance
            //    double[] d_point_O = new double[__robotx.Length];  // distancepoint with obsticle
              

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
                

            }
           
           
           

            angle = cal.Angle(target_y, target_x, __roboty[striker], __robotx[striker]);
         
            
            
            int obstacal_Count=robotcount-1;
            
             Xstep = (int)(stepsize * Math.Cos(angle * Math.PI / 180));
            Ystep = (int)(stepsize * Math.Sin(angle * Math.PI / 180));
            Xstep = Math.Abs(Xstep);
            Ystep = Math.Abs(Ystep);
            
            X = __robotx[striker];
            Y = __roboty[striker];
            point_targetdistance = 15000;


            while (point_targetdistance>400)
            {
          
                if (target_x > X && target_y > Y)
                {
                    X += Xstep;
                    Y += Ystep;
                    XCC.Push(X);
                    YCC.Push(Y);

                }
                else if (X > target_x && Y > target_y)
                {
                    X -= Xstep;
                    Y -= Ystep;
                    XCC.Push(X);
                    YCC.Push(Y);
                }
                else if (target_x < X && Y < target_y)
                {
                    X -= Xstep;
                    Y += Ystep;
                    XCC.Push(X);
                    YCC.Push(Y);
                }
                else if (target_x > X && target_y < Y)
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
                            obstacal_targetdistance = cal.Distances(target_y, target_x, __roboty[i], __robotx[i]);
                        
                        if (P_O_B[i]<400 && obstacal_targetdistance < strikerDistance)
                             {
                            doneBlue[i] = 1;
                            newPath(400,i, __robotx, __roboty, obstacal_targetdistance, target_x, target_y, (double)XCC.Peek(), (double)YCC.Peek());
                            angle = cal.Angle(target_y, target_x, (double)YCC.Peek(), (double)XCC.Peek());
                            Xstep = (int)(stepsize * Math.Cos(angle * Math.PI / 180));
                            Ystep = (int)(stepsize * Math.Sin(angle * Math.PI / 180));
                            Xstep = Math.Abs(Xstep);
                            Ystep = Math.Abs(Ystep);
                            X = (double)XCC.Peek();
                            Y = (double)YCC.Peek();
                            
                        } 
                        }
                 
                }


                for (int i = 0; i <O_robotx.Length; i++)
                {

                    if ( doneYellow[i] == 0 && O_robotx[i] != 0 && O_roboty[i] != 0)
                    {
                        P_O_Y[i] = cal.Distances(O_roboty[i], O_robotx[i], (double)YCC.Peek(), (double)XCC.Peek());
                        obstacal_targetdistance = cal.Distances(target_y, target_x, O_roboty[i], O_robotx[i]);

                        if (P_O_Y[i] < 400 && obstacal_targetdistance < strikerDistance)
                        {
                            doneYellow[i] = 1;
                            newPath(400, i,  O_robotx, O_roboty, obstacal_targetdistance, target_x, target_y, (double)XCC.Peek(), (double)YCC.Peek());
                            angle = cal.Angle(target_y, target_x, (double)YCC.Peek(), (double)XCC.Peek());
                            Xstep = (int)(stepsize * Math.Cos(angle * Math.PI / 180));
                            Ystep = (int)(stepsize * Math.Sin(angle * Math.PI / 180));
                            Xstep = Math.Abs(Xstep);
                            Ystep = Math.Abs(Ystep);
                            X = (double)XCC.Peek();
                            Y = (double)YCC.Peek();

                        }
                    }

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
                    Totaldistance = cal.Distances(y[j], x[j], __roboty[striker], __robotx[striker]);
                 else
                    Totaldistance += cal.Distances(y[j], x[j], y[j-1], x[j-1]);
            }

          



        }
     
        public int ShortestPath(int index,double[] obx, double[] oby, double radius, double ballx, double bally, int _angle, double robotx, double roboty)
        {
            double distance1 = new double();
            double distance2 = new double();
            int angle = _angle;
            double x = new double();
            double y = new double();
            double preX = robotx;
            double preY = roboty;
            double RR_distance;
            for (int i = 0; i < 6; i++)
            {
                angle += 15;
                x = (obx[index] + radius * Math.Cos(angle * Math.PI / 180));
                y = (oby[index] + radius * Math.Sin(angle * Math.PI / 180));

             
                distance1 += cal.Distances(bally, ballx, y, x);
                preX = x;
                preY = y;
            }

            angle = _angle;
            for (int i = 0; i < 6; i++)
            {
                angle -= 15;
                x = (obx[index] + radius * Math.Cos(angle * Math.PI / 180));
                y = (oby[index] + radius * Math.Sin(angle * Math.PI / 180));

             
                distance2 += cal.Distances(bally, ballx, y, x);
            }


            if (distance1 < distance2)
                return 1;
            else
                return 0;


        }
    }
        
    }

