using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProtoBuf;
using System.IO;
using System.Threading;

namespace RoboSoccer
{
    public class FeedBack 
    {
        
        public const int Mybotblue = 1;
        public  int Striker = 1;
        public  int Goalkee = 0;
        public const int radius =1500;
        public UdpClient sock = new UdpClient(10002);
        public protos.tutorial.messages_robocup_ssl_wrapper.SSL_WrapperPacket pakt = new protos.tutorial.messages_robocup_ssl_wrapper.SSL_WrapperPacket();
        IPEndPoint iep;
        public double ballx, bally;
        public double[] BluerobotX, BluerobotY, BluerobotID,BluerobotOrient,Bluedistance, Blueangle, Blueorient_Ball,Blueorient_Goal,R2R_distance ;
        public double[] YellowrobotX, YellowrobotY, YellowrobotID, YellowrobotOrient, Yellowdistance, Yellowangle, Yelloworient_Ball, Yelloworient_Goal;
        public int[] speed ;
        const int noOfRobot = 4;
        const int noOfRobotBlue = 2;
        const int noOfRobotYellow = 2;
        public int c_radius;
        public Calculation calc;
          public Control Controller;
        public int goalkeyPositonX=2600;
       public KalmanFilter kalmanballX,KalmanBally;
        public KalmanNoiseFilter FilterBlueX0, FilterBlueY0, FilterBlue0Orient, FilterBlueX1, FilterBlueY1, FilterBlue1Orient, FilterYellowX0, FilterYellowY0, FilterYellow0Orient, FilterYellowX1, FilterYellowY1, FilterYellow1Orient;

        public int line,a;
            
        public Thread KalmanThread;
        public FeedBack()
        {
            sock.JoinMulticastGroup(IPAddress.Parse("224.5.23.2"), 50);
             iep =  new IPEndPoint(IPAddress.Any, 0);
            c_radius = new int();
            a = new int();
            BluerobotID = new double[noOfRobot]; BluerobotX = new double[noOfRobot]; BluerobotY = new double[noOfRobot];
            ///////////////////////////////////
            YellowrobotID = new double[noOfRobot]; YellowrobotX = new double[noOfRobot]; YellowrobotY = new double[noOfRobot];
            ////////////////////////////////////
            R2R_distance = new double[noOfRobot];   speed = new int[noOfRobot];
            /////////////////////////////////////////
              BluerobotOrient = new double[noOfRobot];Bluedistance = new double[noOfRobot]; Blueangle = new double[noOfRobot];
            Blueorient_Ball = new double[noOfRobot];
            Blueorient_Goal = new double[noOfRobot];
            //////////////////////////////////////////
            YellowrobotOrient = new double[noOfRobot]; Yellowdistance = new double[noOfRobot]; Yellowangle = new double[noOfRobot];
            Yelloworient_Ball = new double[noOfRobot];
            Yelloworient_Goal = new double[noOfRobot];

            /////////////////////////////////////////
            calc = new Calculation();
           
            Controller = new Control(noOfRobot,Striker,Goalkee,Mybotblue, noOfRobotBlue,noOfRobotYellow, goalkeyPositonX);
            kalmanballX = new KalmanFilter();
            KalmanBally = new KalmanFilter();

            KalmanThread = new Thread(kalmanrunning);

            FilterBlueX0 = new KalmanNoiseFilter();
            FilterBlueX1 = new KalmanNoiseFilter();
            FilterBlue0Orient = new KalmanNoiseFilter();

            FilterBlueY0 = new KalmanNoiseFilter();
            FilterBlueY1 = new KalmanNoiseFilter();
            FilterBlue1Orient = new KalmanNoiseFilter();

            FilterYellowX0 = new KalmanNoiseFilter();
            FilterYellowX1 = new KalmanNoiseFilter();
            FilterYellow1Orient = new KalmanNoiseFilter();


            FilterYellowY0 = new KalmanNoiseFilter();
            FilterYellowY1 = new KalmanNoiseFilter();
            FilterYellow0Orient = new KalmanNoiseFilter();






        }
public  void getFeedback()
        {
           
                getData();
            Controller.StrikerPlan.PathChanger();
            Controller.GoalkeePlan.PathChanger();
            Controller.updateGoalKEE();
          Controller.SetTarget();


        }
     
    
        void getData()
        {
            byte[] data = sock.Receive(ref iep);


            pakt = Serializer.Deserialize<protos.tutorial.messages_robocup_ssl_wrapper.SSL_WrapperPacket>(new System.IO.MemoryStream(data));
            Controller.botsInField(pakt.detection.robots_blue.Count, pakt.detection.robots_yellow.Count);
            pakt.detection.t_capture = 1;
           // pakt.detection.camera_id = 0;
            
            if (pakt.detection.balls.Count > 0)
            {
                ballx = pakt.detection.balls[0].x;
                bally = pakt.detection.balls[0].y;

               Controller.setBall(ballx, bally);

            }
            
            for (int i = 0; i < pakt.detection.robots_blue.Count; i++)
            {
                BluerobotID[i] = pakt.detection.robots_blue[i].robot_id;
                int id = Convert.ToInt32(BluerobotID[i]);
                BluerobotX[id] = pakt.detection.robots_blue[i].x;
                BluerobotY[id] = pakt.detection.robots_blue[i].y;
                BluerobotOrient[id] = pakt.detection.robots_blue[i].orientation * 180 / Math.PI;

           //     BluerobotOrient[id] = FilterBlue0Orient.K_Noise_Filter(BluerobotOrient[id], 21.5, 50e-1);
             

                //      Bluedistance[id] = calc.Distances(bally, ballx, BluerobotY[id], BluerobotX[id]);

                Blueorient_Ball[id] = calc.orient(calc.Angle(bally, ballx, BluerobotY[id], BluerobotX[id], BluerobotOrient[id]));
                Blueorient_Goal[id]= calc.orient(calc.Angle(0, (-goalkeyPositonX), BluerobotY[id], BluerobotX[id], BluerobotOrient[id]));
                //      Blueangle[id] = path.routePlaning(BluerobotX[id], BluerobotY[id], Bluedistance[id], Blueangle[id], BluerobotOrient[id]);

                //      Bluedistance[id] = path.FinalDistance;

                //R2R_distance[Striker] = calc.Distances(BluerobotY[0], BluerobotX[0], BluerobotY[Striker], BluerobotX[Striker]);
                //    speed[id] = path.speed;
                //    line = path.Route;

            }
          BluerobotX[0] = FilterBlueX0.K_Noise_Filter(BluerobotX[0]);
          BluerobotX[1] = FilterBlueX1.K_Noise_Filter(BluerobotX[1]);
         BluerobotY[0] = FilterBlueY0.K_Noise_Filter(BluerobotY[0]);
         BluerobotY[1] = FilterBlueY1.K_Noise_Filter(BluerobotY[1]);
            Controller.setBlueBots(0, BluerobotX[0], BluerobotY[0], BluerobotOrient[0]);
            Controller.setBlueBots(1, BluerobotX[1], BluerobotY[1], BluerobotOrient[1]);
         






            //           motion.PathFinding(bally, ballx, BluerobotY[Striker], BluerobotX[Striker],BluerobotOrient[Striker],BluerobotY[Goalkee],BluerobotX[Goalkee], (pakt.detection.robots_blue.Count+ pakt.detection.robots_yellow.Count));



            //  if (pakt.detection.robots_blue.Count>1)
            //  R2R_distance[Striker] = calc.Distances(BluerobotY[0], BluerobotX[0], BluerobotY[Striker], BluerobotX[Striker]);

            for (int i = 0; i < pakt.detection.robots_yellow.Count; i++)
            {
                YellowrobotID[i] = pakt.detection.robots_yellow[i].robot_id;
                int id = Convert.ToInt32(BluerobotID[i]);
                
                YellowrobotX[id] = pakt.detection.robots_yellow[i].x;
                YellowrobotY[id] = pakt.detection.robots_yellow[i].y;
                YellowrobotOrient[id] = pakt.detection.robots_yellow[i].orientation * 180 / Math.PI;

         //       YellowrobotOrient[id] = FilterYellow0Orient.K_Noise_Filter(YellowrobotOrient[id], 21.5, 50e-1);
              
              
                
                Yelloworient_Ball[id] = calc.orient(calc.Angle(bally, ballx, YellowrobotY[id], YellowrobotX[id], YellowrobotOrient[id]));
                Yelloworient_Goal[id] = calc.orient(calc.Angle(0, (-goalkeyPositonX), YellowrobotY[id], YellowrobotX[id], YellowrobotOrient[id]));
                // Yellowdistance[id] = calc.Distances(bally, ballx, YellowrobotY[id], YellowrobotX[id]);
                //  Yellowangle[id] = calc.Angle(bally, ballx, YellowrobotY[id], YellowrobotX[id], YellowrobotOrient[id]);
                //  Yelloworient[id] = calc.orient(Yellowangle[id]);
                //  Yellowangle[id] = path.routePlaning(YellowrobotX[id], YellowrobotY[id], Yellowdistance[id], Yellowangle[id], YellowrobotOrient[id]);


                //speed[id] = path.speed;
                //  line = path.Route;
            }
            
            YellowrobotX[0] = FilterYellowX0.K_Noise_Filter(YellowrobotX[0]);
          YellowrobotX[1] = FilterYellowX1.K_Noise_Filter(YellowrobotX[1]);
            YellowrobotY[0] = FilterYellowY0.K_Noise_Filter(YellowrobotY[0]);
            YellowrobotY[1] = FilterYellowY1.K_Noise_Filter(YellowrobotY[1]);
           
            Controller.setYellowBots(0, YellowrobotX[0], YellowrobotY[0], YellowrobotOrient[0]);
            Controller.setYellowBots(1, YellowrobotX[1], YellowrobotY[1], YellowrobotOrient[1]);
          
              
        }
         void kalmanrunning()
        {
            Thread.Sleep(2000);
            while(true)
            {
                ballx = kalmanballX.KALMANOUT(ballx);
                bally = KalmanBally.KALMANOUT(bally);
                if (ballx > 2000)
                    ballx = 2000;
                else if (ballx < -2000)
                    ballx = -2000;

                if (bally > 1200)
                    bally = 1200;
                else if (bally < -1200)
                    bally = -1200;
             
                Controller.setBall(ballx, bally);
                Thread.Sleep(10);
            }
        }
       






    }
}
