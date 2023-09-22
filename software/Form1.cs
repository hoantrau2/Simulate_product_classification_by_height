using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using S7.Net;
using S7.Net.Types;

namespace CS_to_S7
{
    public partial class Form1 : Form
    {
        byte[] Output;
        byte[] Input;

        Plc PLC;
        public Form1()
        {
            InitializeComponent();
            Running_Label.Visible = false;
            Stop_Label.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            PLC = new Plc(CpuType.S71200, IP.Text,0,0);
            if(PLC.Open() == ErrorCode.NoError)
            {
                MessageBox.Show("Connect Successfully");
                timer1.Start();
            }
            else
            {
                MessageBox.Show("Wrong IP address");
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                Input = PLC.ReadBytes(DataType.Input, 0, 0, 2);
                panel1.BackColor = (Input[0].SelectBit(3) == true) ? Color.Blue : Color.LightGray;
                panel2.BackColor = (Input[0].SelectBit(4) == true) ? Color.Blue : Color.LightGray;
                panel3.BackColor = (Input[0].SelectBit(5) == true) ? Color.Blue : Color.LightGray;
                panel4.BackColor = (Input[0].SelectBit(6) == true) ? Color.Blue : Color.LightGray;
                panel5.BackColor = (Input[0].SelectBit(7) == true) ? Color.Blue : Color.LightGray;

                
                Output = PLC.ReadBytes(DataType.Output, 0, 0, 1);
                //panel14.BackColor = (Output[0].SelectBit(0) == true) ? Color.Blue : Color.LightGray;
                //panel13.BackColor = (Output[0].SelectBit(1) == true) ? Color.Blue : Color.LightGray;
                //panel12.BackColor = (Output[0].SelectBit(2) == true) ? Color.Blue : Color.LightGray;
                //panel11.BackColor = (Output[0].SelectBit(3) == true) ? Color.Blue : Color.LightGray;
                //panel10.BackColor = (Output[0].SelectBit(4) == true) ? Color.Blue : Color.LightGray;
                //panel9.BackColor  = (Output[0].SelectBit(5) == true) ? Color.Blue : Color.LightGray;
                //panel8.BackColor  = (Output[0].SelectBit(6) == true) ? Color.Blue : Color.LightGray;

                Running_Label.Visible = (Output[0].SelectBit(1) == true) ? true : false;
                Stop_Label.Visible = (Output[0].SelectBit(1) == false) ? true : false;

                CTH.Text = PLC.Read("MD0").ToString();
                CTM.Text = PLC.Read("MD4").ToString();
                CTS.Text = PLC.Read("MD8").ToString();
            }
            catch
            {
                PLC.Close();
                PLC.Open();
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (PLC.Open() == ErrorCode.NoError)
            {
                for (int i = 30; i >= 0; i--)
                {
                    PLC.Write("I0.0", 1);
                    PLC.Write("I0.0", 0);
                }
                Running_Label.Visible = true;
                Stop_Label.Visible = false;
            }
            else
            {
                MessageBox.Show("PLC was not connected ");
            }
        }
        private async void button3_Click(object sender, EventArgs e)
        {

            if (PLC.Open() == ErrorCode.NoError)
            {
                for (int i = 30; i >= 0; i--)
                {
                    PLC.Write("I0.1", 1);
                    PLC.Write("I0.1", 0);
                }
                Running_Label.Visible = false;
                Stop_Label.Visible = true;
            }
            else
            {
                MessageBox.Show("PLC was not connected ");
            }
        }
        private async void button4_Click(object sender, EventArgs e)
        {
            if (PLC.Open() == ErrorCode.NoError)
            {
                for (int i = 30; i>=0; i--) {
                    PLC.Write("I0.2", 1);
                    PLC.Write("I0.2", 0);
                }
            }
            else
            {
                MessageBox.Show("PLC was not connected ");
            }
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
