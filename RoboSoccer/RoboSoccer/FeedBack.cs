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
namespace RoboSoccer
{
    public class FeedBack 
    {
        public const int Master = 1;
        public const int radius =1500;
        public UdpClient sock = new UdpClient(10002);
        public protos.tutorial.messages_robocup_ssl_wrapper.SSL_WrapperPacket pakt = new protos.tutorial.messages_robocup_ssl_wrapper.SSL_WrapperPacket();
        IPEndPoint iep;
        public double ballx, bally;
        public double[] robotX, robotY, robotID,robotOrient,distance, angle, orient,R2R_distance ;
        public int speed ;
        const int noOfRobot = 3;
        public int c_radius;
        Calculation calc;
        PathPlanning path;
        
        public int line;
        public FeedBack()
        {
            sock.JoinMulticastGroup(IPAddress.Parse("224.5.23.2"), 50);
             iep =  new IPEndPoint(IPAddress.Any, 0);
            c_radius = new int();
            robotID = new double[noOfRobot];
            robotX = new double[noOfRobot];
            robotY = new double[noOfRobot];
            R2R_distance = new double[noOfRobot];
            speed = new int();
              robotOrient = new double[noOfRobot];
            distance = new double[noOfRobot];
            angle = new double[noOfRobot];
            orient = new double[noOfRobot];
            calc = new Calculation();
            path = new PathPlanning();

            
        }
public  void getFeedback()
        {
            byte[] data = sock.Receive(ref iep);

            
            pakt = Serializer.Deserialize<protos.tutorial.messages_robocup_ssl_wrapper.SSL_WrapperPacket>(new System.IO.MemoryStream(data));
            
           
                if (pakt.detection.balls.Count > 0)
             {
                ballx = pakt.detection.balls[0].x;
                bally = pakt.detection.balls[0].y;
            }           
             for (int i=0;i< pakt.detection.robots_blue.Count; i++)
            {
                robotID[i] = pakt.detection.robots_blue[i].robot_id;
                int id = Convert.ToInt32(robotID[i]);
                robotX[id] = pakt.detection.robots_blue[i].x;
                robotY[id] = pakt.detection.robots_blue[i].y;
                robotOrient[id] = pakt.detection.robots_blue[i].orientation*180/Math.PI;
                distance[id] = calc.Distances(bally,ballx,robotY[id],robotX[id]);
                angle[id] = calc.Angle(bally, ballx, robotY[id], robotX[id],robotOrient[id]);
                orient[id] = calc.orient(angle[id]);
                angle[id] = path.routePlaning(robotX[id], robotY[id], distance[id], angle[id], robotOrient[id]);

                distance[id] = path.FinalDistance;
             /*   if (Master != id)
                {
                    R2R_distance[Master] = calc.Distances(robotY[id], robotX[id], robotY[Master], robotX[Master]);

                    if ((R2R_distance[Master] < radius)&& (distance[Master]>distance[id]))
                        angle[Master] = path.routePlaning(robotX[Master],robotY[Master],radius,robotX[i],robotY[i],angle[Master],distance[i],ballx,bally);
                    distance[Master] = path.FinalDistance;
                    speed = path.speed;
                }
               */ 
                speed = path.speed;
                line = path.Route;               

            }


                R2R_distance[Master] = calc.Distances(robotY[0], robotX[0], robotY[Master], robotX[Master]);
            /*if (R2R_distance[Master] < 1000)
            {
                angle[Master] = 90 + calc.Angle(robotY[0], robotX[0], robotY[Master], robotX[Master], robotOrient[Master]);

            }
           /* else
            { */
                if ((R2R_distance[Master] < radius) && (distance[Master] > distance[0]))
                {
                    angle[Master] = path.routePlaning(robotX[Master], robotY[Master], radius, robotX[0], robotY[0], angle[Master], distance[0], ballx, bally,R2R_distance[Master]);
                    distance[Master] = path.FinalDistance;
             
                speed = path.speed;
                }
             
           // }
            




        }                 



      


    }
}
