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
        public const int Striker = 1;
        public const int Goalkee = 0;
        public const int radius =1500;
        public UdpClient sock = new UdpClient(10002);
        public protos.tutorial.messages_robocup_ssl_wrapper.SSL_WrapperPacket pakt = new protos.tutorial.messages_robocup_ssl_wrapper.SSL_WrapperPacket();
        IPEndPoint iep;
        public double ballx, bally;
        public double[] BluerobotX, BluerobotY, BluerobotID,BluerobotOrient,Bluedistance, Blueangle, Blueorient,R2R_distance ;
        public double[] YellowrobotX, YellowrobotY, YellowrobotID, YellowrobotOrient, Yellowdistance, Yellowangle, Yelloworient;
        public int[] speed ;
        const int noOfRobot = 4;
        const int noOfRobotBlue = 4;
        const int noOfRobotYellow = 4;
        public int c_radius;
        public Calculation calc;
        public PathPlanning path;
        public Control Controller;
        public  ObstacleTrejectory motion;
        public PathPlanner trajectory;
        public int line;
       
        int a = 0;
        public FeedBack()
        {
            sock.JoinMulticastGroup(IPAddress.Parse("224.5.23.2"), 50);
             iep =  new IPEndPoint(IPAddress.Any, 0);
            c_radius = new int();
            BluerobotID = new double[noOfRobot]; BluerobotX = new double[noOfRobot]; BluerobotY = new double[noOfRobot];
            ///////////////////////////////////
            YellowrobotID = new double[noOfRobot]; YellowrobotX = new double[noOfRobot]; YellowrobotY = new double[noOfRobot];
            ////////////////////////////////////
            R2R_distance = new double[noOfRobot];   speed = new int[noOfRobot];
            /////////////////////////////////////////
              BluerobotOrient = new double[noOfRobot];Bluedistance = new double[noOfRobot]; Blueangle = new double[noOfRobot];
            Blueorient = new double[noOfRobot];
            //////////////////////////////////////////
            YellowrobotOrient = new double[noOfRobot]; Yellowdistance = new double[noOfRobot]; Yellowangle = new double[noOfRobot];
            Yelloworient = new double[noOfRobot];
            /////////////////////////////////////////
            calc = new Calculation();
            path = new PathPlanning();
            Controller = new Control(noOfRobot,Striker,Goalkee,Mybotblue);
            trajectory = new PathPlanner(noOfRobot, Striker, Goalkee, Mybotblue);
            motion = new ObstacleTrejectory();
           
            
        }
public  void getFeedback()
        {
           
                getData();
   

        }
     
                
 











        void getData()
        {
            byte[] data = sock.Receive(ref iep);


            pakt = Serializer.Deserialize<protos.tutorial.messages_robocup_ssl_wrapper.SSL_WrapperPacket>(new System.IO.MemoryStream(data));
            Controller.botsInField(pakt.detection.robots_blue.Count, pakt.detection.robots_yellow.Count);

            if (pakt.detection.balls.Count > 0)
            {
                ballx = pakt.detection.balls[0].x;
                bally = pakt.detection.balls[0].y;
                Controller.setBall(ballx, bally);
                trajectory.setBall(ballx, bally);
            }
            
            for (int i = 0; i < pakt.detection.robots_blue.Count; i++)
            {
                BluerobotID[i] = pakt.detection.robots_blue[i].robot_id;
                int id = Convert.ToInt32(BluerobotID[i]);
                BluerobotX[id] = pakt.detection.robots_blue[i].x;
                BluerobotY[id] = pakt.detection.robots_blue[i].y;
                BluerobotOrient[id] = pakt.detection.robots_blue[i].orientation * 180 / Math.PI;

                Controller.setBlueBots(id, BluerobotX[id], BluerobotY[id], BluerobotOrient[id]);
                trajectory.setBlueBots(id, BluerobotX[id], BluerobotY[id], BluerobotOrient[id]);
                //      Bluedistance[id] = calc.Distances(bally, ballx, BluerobotY[id], BluerobotX[id]);
                //      Blueangle[id] = calc.Angle(bally, ballx, BluerobotY[id], BluerobotX[id], BluerobotOrient[id]);
                //      Blueorient[id] = calc.orient(Blueangle[id]);
                //      Blueangle[id] = path.routePlaning(BluerobotX[id], BluerobotY[id], Bluedistance[id], Blueangle[id], BluerobotOrient[id]);

                //      Bluedistance[id] = path.FinalDistance;

                //R2R_distance[Striker] = calc.Distances(BluerobotY[0], BluerobotX[0], BluerobotY[Striker], BluerobotX[Striker]);
                //    speed[id] = path.speed;
                //    line = path.Route;

            }



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

                Controller.setYellowBots(id, YellowrobotX[id], YellowrobotY[id], YellowrobotOrient[id]);
                trajectory.setYellowBots(id, YellowrobotX[id], YellowrobotY[id], YellowrobotOrient[id]);
                // Yellowdistance[id] = calc.Distances(bally, ballx, YellowrobotY[id], YellowrobotX[id]);
                //  Yellowangle[id] = calc.Angle(bally, ballx, YellowrobotY[id], YellowrobotX[id], YellowrobotOrient[id]);
                //  Yelloworient[id] = calc.orient(Yellowangle[id]);
                //  Yellowangle[id] = path.routePlaning(YellowrobotX[id], YellowrobotY[id], Yellowdistance[id], Yellowangle[id], YellowrobotOrient[id]);


                //speed[id] = path.speed;
                //  line = path.Route;
            }
               



        }

       






    }
}
