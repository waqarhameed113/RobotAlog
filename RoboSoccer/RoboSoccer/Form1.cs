﻿using System;
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
using System.Threading;
using System.IO.Ports;
namespace RoboSoccer
{
    public partial class Form1 : Form
    {

        // protos.tutorial.messages_robocup_ssl_wrapper.SSL_WrapperPacket my_pakt = new protos.tutorial.messages_robocup_ssl_wrapper.SSL_WrapperPacket();
       public static FeedBack f1;
        
       
        public Form1()
        {
            InitializeComponent();
            getavailablePorts();
            

        }
        void getavailablePorts()
        {
            String[] ports = SerialPort.GetPortNames();
            comboBox1.Items.AddRange(ports);
        }

        private void button1_Click(object sender, EventArgs e)
        {
             
                                                              
            if (timer1.Enabled != true)
            {
                timer1.Enabled = true;

               f1.Controller.StrikerPlan.trejectoryPloting.Start();
                f1.Controller.GoalkeePlan.trejectoryPloting.Start();
                //   f1.KalmanThread.Start();
                
                timer3.Enabled = true;

                double[] XS = { -1000, -800, -500, -200, -100, 100, 500, 1000 };
                double[] YS = { 500, 500, 500, 500, 500, 500, 500, 500 };
                f1.Controller.Strategic.GetStredgy(XS, YS);


            }

            else
            {
                timer3.Enabled = false;
                timer1.Enabled = false;
               

            }
            
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
           
               
             f1.getFeedback();
           
            richTextBox1.Text = "Ball_X "+((int)f1.ballx).ToString()+"\n";
            richTextBox1.Text += "Ball_Y " + ((int)f1.bally).ToString() + "\n";
           richTextBox1.Text += "Robot_Id " + f1.BluerobotID[f1.Striker].ToString()+'\n';

            richTextBox1.Text += "Robot_X " + ((int)f1.BluerobotX[f1.Striker]).ToString() + '\n';
            richTextBox1.Text += "Robot_Y " + ((int)f1.BluerobotY[f1.Striker]).ToString() + '\n';
            richTextBox1.Text += "Robot_Y " + ((int)f1.BluerobotY[f1.Striker]).ToString() + '\n';

            richTextBox1.Text += "Distance " + ((int)f1.Bluedistance[f1.Striker]).ToString() + '\n';
            richTextBox1.Text += "Angle " + ((int)f1.Blueangle[f1.Striker]).ToString() + '\n';
            //richTextBox1.Text += "Orient " + ((int)f1.Blueorient_Ball[1]).ToString() + '\n';
            richTextBox1.Text += "Orient " + f1.BluerobotOrient[1].ToString() + '\n';


            //    richTextBox2.Text = "Speed,Rotate,Angle,R_Magnitude,garbage\n";
            richTextBox2.Text = f1.Controller.StrikerPlan.pathFollower.speed.ToString() + ',' + ((int)f1.Blueorient_Ball[f1.Striker]).ToString() + ',' + ((int)f1.Controller.StrikerPlan.pathFollower.angle).ToString() + "," + ((int)f1.Controller.StrikerPlan.pathFollower.angularSpeed) +","+f1.Controller.kick;
            //   if (f1.Controller.pathFollower.newpathIndication==1)
            // richTextBox4.Text = "X  "+f1.Controller.pathFollower.X[f1.Controller.pathFollower.i].ToString()+"  Y  " + f1.Controller.pathFollower.Y[f1.Controller.pathFollower.i].ToString() + "\nDistance"+ f1.Controller.pathFollower.TotalDistance.ToString();

            richTextBox4.Text = f1.Controller.angledifference.ToString();
        



            timer1.Enabled = true;

            

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            f1 = new FeedBack();
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen)
            {
                
                        serialPort1.PortName = comboBox1.Text;
                        serialPort1.BaudRate = 115200;
                   
                    serialPort1.Open();
                
            }
           if (f1.goalkeyPositonX <0)
            {
                f1.Controller.buttonSetpoint = 1;
                f1.Controller.StrikerPlan.targetX = -350;
                f1.Controller.StrikerPlan.targetY = 0;
            }
           else
            {
                f1.Controller.buttonSetpoint = 1;
                f1.Controller.StrikerPlan.targetX = 350;
                f1.Controller.StrikerPlan.targetY = 0;
            }


            timer2.Enabled = true;

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            if (((int)f1.Blueorient_Ball[f1.Striker]) == 0)
                f1.Controller.StrikerPlan.pathFollower.angle = 0;
            if (serialPort1.IsOpen)
            {

                try
                {   

                    serialPort1.WriteLine(f1.Controller.StrikerPlan.pathFollower.speed.ToString() + ',' + ((int)f1.Blueorient_Ball[f1.Striker]).ToString() + ',' + ((int)f1.Controller.StrikerPlan.pathFollower.angle).ToString() +","+ ((int)f1.Controller.StrikerPlan.pathFollower.angularSpeed)+",0");
                  //  serialPort1.WriteLine(f1.Controller.GoalkeePlan.pathFollower.speed.ToString() + ',' + ((int)f1.Blueorient_Ball[f1.Goalkee]).ToString() + ',' + ((int)f1.Controller.GoalkeePlan.pathFollower.angle).ToString() + ",20,0");
                    //           richTextBox2.Text = f1.speed.ToString() + ',' + ((int)f1.orient[5]).ToString() + ',' + ((int)f1.angle[5]).ToString() + ",60,0";
                }
                catch { }
            }


            timer2.Enabled = true;
        }

        public void chart1_Click(object sender, EventArgs e)
        {
            richTextBox3.Text = "X ";
            richTextBox3.Text += f1.Controller.StrikerPlan.motionPlaning.x[0].ToString() + ' ';//f1.motion.x[0].ToString()+' ';

            for (int i = 1; i < f1.Controller.StrikerPlan.motionPlaning.x.Length; i++)
            {
                richTextBox3.Text += f1.Controller.StrikerPlan.motionPlaning.x[i].ToString() + ' ';

            }

            richTextBox3.Text += "\n\nY ";
            richTextBox3.Text += f1.Controller.StrikerPlan.motionPlaning.y[0].ToString() + ' ';

            for (int i = 1; i < f1.Controller.StrikerPlan.motionPlaning.y.Length; i++)
            {

                richTextBox3.Text += f1.Controller.StrikerPlan.motionPlaning.y[i].ToString() + ' ';
            }

            chart1.Series["Trejectory"].Points.Clear();
            chart1.Series["ball"].Points.Clear();
            chart1.Series["blueBot1"].Points.Clear();
            chart1.Series["blueBot0"].Points.Clear();
            chart1.Series["yellowBot1"].Points.Clear();
            chart1.Series["yellowBot0"].Points.Clear();
            //chart1.Series["Trejectory"].Points.Clear();

            chart1.Series["abc"].Points.AddXY(-3000, 1500);
            chart1.Series["abc"].Points.AddXY(3000, -1500);
            
           

            

            if (f1.pakt.detection.balls.Count>0)

            {
                chart1.Series["ball"].Points.AddXY(f1.ballx, f1.bally);
                chart1.Series["ball"].Label = "ball";
            }
            if (f1.pakt.detection.robots_blue.Count>0)
            {
                if (f1.BluerobotX[1] != 0)
                {
                    chart1.Series["blueBot1"].Points.AddXY(f1.BluerobotX[1], f1.BluerobotY[1]);
                    chart1.Series["blueBot1"].Label = "bot 1";
                    chart1.Series["blueBot1"].Color = Color.Blue;
                }

                if (f1.BluerobotX[0] != 0)
                {
                    chart1.Series["blueBot0"].Points.AddXY(f1.BluerobotX[0], f1.BluerobotY[0]);
                    chart1.Series["blueBot0"].Label = "bot 0";
                    chart1.Series["blueBot0"].Color = Color.Blue;

                }

                if (f1.BluerobotX[2] != 0)
                {
                    chart1.Series["blueBot0"].Points.AddXY(f1.BluerobotX[2], f1.BluerobotY[2]);
                    chart1.Series["blueBot0"].Label = "bot 0";
                    chart1.Series["blueBot0"].Color = Color.Blue;
                }
                if (f1.BluerobotX[3] != 0)
                {
                    chart1.Series["blueBot0"].Points.AddXY(f1.BluerobotX[3], f1.BluerobotY[3]);
                    chart1.Series["blueBot0"].Label = "bot 0";
                    chart1.Series["blueBot0"].Color = Color.Blue;
                }
               
                

                
                
            }

            if (f1.pakt.detection.robots_yellow.Count>0)
            {
                if (f1.YellowrobotX[1] != 0)
                {
                    chart1.Series["yellowBot1"].Points.AddXY(f1.YellowrobotX[1], f1.YellowrobotY[1]);
                    chart1.Series["yellowBot1"].Label = "bot 1";
                    chart1.Series["yellowBot1"].Color = Color.Yellow;
                }
                if (f1.YellowrobotX[0] != 0)
                {
                    chart1.Series["yellowBot0"].Points.AddXY(f1.YellowrobotX[0], f1.YellowrobotY[0]);
                    chart1.Series["yellowBot0"].Label = "bot 0";
                    chart1.Series["yellowBot0"].Color = Color.Yellow;
                }
                
            }
            // chart1.Series["Trejectory"].Points.;
            for (int i = 0; i < f1.Controller.StrikerPlan.motionPlaning.x.Length; i++)
            {
                chart1.Series["Trejectory"].Points.AddXY(f1.Controller.StrikerPlan.motionPlaning.x[i], f1.Controller.StrikerPlan.motionPlaning.y[i]);
            }
            chart1.Series["abc"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            chart1.Series["Trejectory"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            chart1.Series["Trejectory"].Color = Color.Red;
            chart1.Series["ball"].Color = Color.OrangeRed;
            


        }

        
        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void closing(object sender, FormClosingEventArgs e)
        {
            f1.Controller.StrikerPlan.pathFollower.pathfollower.Abort();
            f1.Controller.GoalkeePlan.pathFollower.pathfollower.Abort();
            f1.Controller.StrikerPlan.trejectoryPloting.Abort();
            f1.Controller.GoalkeePlan.trejectoryPloting.Abort();
          // f1.KalmanThread.Abort();
            if (serialPort1.IsOpen)
            {
                try
                {
                    serialPort1.WriteLine("0,0,0,0");
                }
                catch { }
                serialPort1.Close();

            }
            
           

            timer1.Enabled = false;
            timer2.Enabled = false;
           

        }

       

        private void timer3_Tick_1(object sender, EventArgs e)
        {
            if (f1.Controller.StrikerPlan.pathcompleteforGraph == 1 && f1.Controller.GoalkeePlan.pathcompleteforGraph == 1)
            {
                timer3.Enabled = false;
                richTextBox3.Text = "X ";
                richTextBox3.Text += f1.Controller.StrikerPlan.motionPlaning.x[0].ToString() + ' ';//f1.motion.x[0].ToString()+' ';

                for (int i = 1; i < f1.Controller.StrikerPlan.motionPlaning.x.Length; i++)
                {
                    richTextBox3.Text += f1.Controller.StrikerPlan.motionPlaning.x[i].ToString() + ' ';

                }

                richTextBox3.Text += "\n\nY ";
                richTextBox3.Text += f1.Controller.StrikerPlan.motionPlaning.y[0].ToString() + ' ';

                for (int i = 1; i < f1.Controller.StrikerPlan.motionPlaning.y.Length; i++)
                {

                    richTextBox3.Text += f1.Controller.StrikerPlan.motionPlaning.y[i].ToString() + ' ';
                }
                chart1.Series["Trajectory0"].Points.Clear();
                chart1.Series["Trejectory"].Points.Clear();
                chart1.Series["ball"].Points.Clear();
                chart1.Series["blueBot1"].Points.Clear();
                chart1.Series["blueBot0"].Points.Clear();
                chart1.Series["yellowBot1"].Points.Clear();
                chart1.Series["yellowBot0"].Points.Clear();
                //chart1.Series["Trejectory"].Points.Clear();

                chart1.Series["abc"].Points.AddXY(-2000, 1300);
                chart1.Series["abc"].Points.AddXY(2000, -1300);





                if (f1.pakt.detection.balls.Count > 0)

                {
                    chart1.Series["ball"].Points.AddXY(f1.ballx, f1.bally);
                    chart1.Series["ball"].Label = "ball";
                }
                if (f1.pakt.detection.robots_blue.Count > 0)
                {
                    if (f1.BluerobotX[1] != 0)
                    {
                        chart1.Series["blueBot1"].Points.AddXY(f1.BluerobotX[1], f1.BluerobotY[1]);
                        chart1.Series["blueBot1"].Label = "bot 1";
                        chart1.Series["blueBot1"].Color = Color.Blue;
                    }

                    if (f1.BluerobotX[0] != 0)
                    {
                        chart1.Series["blueBot0"].Points.AddXY(f1.BluerobotX[0], f1.BluerobotY[0]);
                        chart1.Series["blueBot0"].Label = "bot 0";
                        chart1.Series["blueBot0"].Color = Color.Blue;

                    }

                    if (f1.BluerobotX[2] != 0)
                    {
                        chart1.Series["blueBot0"].Points.AddXY(f1.BluerobotX[2], f1.BluerobotY[2]);
                        chart1.Series["blueBot0"].Label = "bot 0";
                        chart1.Series["blueBot0"].Color = Color.Blue;
                    }
                    if (f1.BluerobotX[3] != 0)
                    {
                        chart1.Series["blueBot0"].Points.AddXY(f1.BluerobotX[3], f1.BluerobotY[3]);
                        chart1.Series["blueBot0"].Label = "bot 0";
                        chart1.Series["blueBot0"].Color = Color.Blue;
                    }



                }

                if (f1.pakt.detection.robots_yellow.Count > 0)
                {
                    if (f1.YellowrobotX[1] != 0)
                    {
                        chart1.Series["yellowBot1"].Points.AddXY(f1.YellowrobotX[1], f1.YellowrobotY[1]);
                        chart1.Series["yellowBot1"].Label = "bot 1";
                        chart1.Series["yellowBot1"].Color = Color.Yellow;
                    }
                    if (f1.YellowrobotX[0] != 0)
                    {
                        chart1.Series["yellowBot0"].Points.AddXY(f1.YellowrobotX[0], f1.YellowrobotY[0]);
                        chart1.Series["yellowBot0"].Label = "bot 0";
                        chart1.Series["yellowBot0"].Color = Color.Yellow;
                    }

                }
                // chart1.Series["Trejectory"].Points.;
                for (int i = 0; i < f1.Controller.StrikerPlan.motionPlaning.x.Length; i++)
                {
                    chart1.Series["Trejectory"].Points.AddXY(f1.Controller.StrikerPlan.motionPlaning.x[i], f1.Controller.StrikerPlan.motionPlaning.y[i]);
                }
                for (int i = 0; i < f1.Controller.GoalkeePlan.motionPlaning.x.Length; i++)
                {
                    chart1.Series["Trajectory0"].Points.AddXY(f1.Controller.GoalkeePlan.motionPlaning.x[i], f1.Controller.GoalkeePlan.motionPlaning.y[i]);
                }
                chart1.Series["abc"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
                chart1.Series["Trejectory"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
                chart1.Series["Trejectory"].Color = Color.Red;
                chart1.Series["Trajectory0"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
                chart1.Series["Trajectory0"].Color = Color.Red;
                chart1.Series["ball"].Color = Color.OrangeRed;
                    f1.Controller.StrikerPlan.pathcompleteforGraph = 0;
            }
            timer3.Enabled = true;
        }

        private void Strateg1_Click(object sender, EventArgs e)
        {
            f1.Controller.buttonSetpoint = 0;
            double[] XS = { -1000, -800, -500, -200, -100, 100, 500, 1000 };
            double[] YS = { 500, 500, 500, 500, 500, 500, 500, 500 };
            f1.Controller.Strategic.GetStredgy(XS, YS);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void TimeSerialRecieve_Tick(object sender, EventArgs e)
        {
            
           
        }

        private void StopButton(object sender, EventArgs e)
        {

            try
            {
                serialPort1.WriteLine("0,0,0,0,0");
            }
            catch { }
            serialPort1.Close();
        }
    }
    }
    

