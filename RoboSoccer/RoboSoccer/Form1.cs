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
namespace RoboSoccer
{
    public partial class Form1 : Form
    {

        // protos.tutorial.messages_robocup_ssl_wrapper.SSL_WrapperPacket my_pakt = new protos.tutorial.messages_robocup_ssl_wrapper.SSL_WrapperPacket();
       public static FeedBack f1;
        public Form1()
        {
            InitializeComponent();


        }

        private void button1_Click(object sender, EventArgs e)
        {
             timer1.Enabled = true;


        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            f1.getFeedback();
            richTextBox1.Text = "Ball_X "+((int)f1.ballx).ToString()+"\n";
            richTextBox1.Text += "Ball_Y " + ((int)f1.bally).ToString() + "\n";
            richTextBox1.Text += "Robot_Id " + f1.robotID[1].ToString()+'\n';
            richTextBox1.Text += "Robot_X " + ((int)f1.robotX[1]).ToString() + '\n';
            richTextBox1.Text += "Robot_Y " + ((int)f1.robotY[1]).ToString() + '\n';
            richTextBox1.Text += "Distance " + ((int)f1.distance[1]).ToString() + '\n';
            richTextBox1.Text += "Angle " + ((int)f1.angle[1]).ToString() + '\n';
            richTextBox1.Text += "Orient " + ((int)f1.orient[1]).ToString() + '\n';


            //    richTextBox2.Text = "Speed,Rotate,Angle,R_Magnitude,garbage\n";
             richTextBox2.Text = f1.speed.ToString() + ',' + ((int)f1.orient[1]).ToString() + ',' + ((int)f1.angle[1]).ToString() + ",50,0\n"+ f1.line.ToString()+'\n';
           // richTextBox2.Text = f1.speed.ToString() + ',' + ((int)f1.orient[5]).ToString() + ',' + ((int)f1.angle[5]).ToString() + ",60,0";
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
                serialPort1.PortName = "COM3";
                serialPort1.BaudRate = 115200;
                serialPort1.Open();

            }
            else
            {
                try
                {
                    serialPort1.WriteLine("0,0,0,0");
                }
                catch { }
                serialPort1.Close();
            }

            timer2.Enabled = true;

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            if (serialPort1.IsOpen)
            {

                try
                {
                    serialPort1.WriteLine(f1.speed.ToString()+','+((int)f1.orient[1]).ToString()+',' + ((int)f1.angle[1]).ToString() + ",60,0");
         //           richTextBox2.Text = f1.speed.ToString() + ',' + ((int)f1.orient[5]).ToString() + ',' + ((int)f1.angle[5]).ToString() + ",60,0";
                }
                catch { }
            }


            timer2.Enabled = true;
        }
    }
    }

