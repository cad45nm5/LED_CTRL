using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using System.Windows.Input;
using System.IO;

namespace uart_control
{
    public partial class Form1 : Form
    {
        int R, G, B;

        string com = "COM12";
        Boolean draw = false;
        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;

        }
        System.Windows.Forms.Button[] Buttons;




        private void button()
        {
            int width, height;
            /*Button1 = new System.Windows.Forms.Button();
            Button1.Location = new Point(100, 100);
            Button1.Size = new Size(100, 100);
            Button1.Text = "Hello!";
            this.Controls.Add(Button1);*/
            width = 20;
            height = 20;
            System.Drawing.Drawing2D.GraphicsPath aCircle = new System.Drawing.Drawing2D.GraphicsPath();
            aCircle.AddEllipse(new Rectangle(0, 0, 20, 20));


            Buttons = new System.Windows.Forms.Button[213];
            for (int i = 0; i < 213; ++i)
            {

                Buttons[i] = new Button();
                Buttons[i].Size = new Size(width, height);
                Buttons[i].Text = "";
                Buttons[i].BackColor = pictureBox1.BackColor;
                this.Controls.Add(Buttons[i]);
                if (i < 42) Buttons[i].Location = new System.Drawing.Point(150 + i * 20, 250);
                else if (i < 83) Buttons[i].Location = new System.Drawing.Point(150 + i * 20 - 840, 230);
                else if (i < 121) Buttons[i].Location = new System.Drawing.Point(170 + i * 20 - 1660, 210);
                else if (i < 153) Buttons[i].Location = new System.Drawing.Point(190 + i * 20 - 2420, 190);
                else if (i < 179) Buttons[i].Location = new System.Drawing.Point(210 + i * 20 - 3060, 170);
                else if (i < 198) Buttons[i].Location = new System.Drawing.Point(230 + i * 20 - 3580, 150);
                else if (i < 209) Buttons[i].Location = new System.Drawing.Point(250 + i * 20 - 3960, 130);
                else if (i < 213) Buttons[i].Location = new System.Drawing.Point(250 + i * 20 - 4180, 110);
                Buttons[i].Region = new Region(aCircle);

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            serialPort1.Open();
            Form1.CheckForIllegalCrossThreadCalls = false;
            button();
            for (int i = 0; i < Buttons.Length; i++)
            {
                int index = i;
                Buttons[i].Click += (sender1, ex) => this.Display2(index + 1);
                Buttons[i].MouseEnter += (sender1, ex) => this.Display(index + 1);
                this.Controls.Add(Buttons[i]);
            }
        }
        public void Display(int i)
        {
            serialPort1.DiscardInBuffer();
            if (draw)
            {
                i--;
                Buttons[i].BackColor = pictureBox1.BackColor;
                Buttons[i].FlatAppearance.BorderColor = pictureBox1.BackColor;
                string r, g, b;
                r = pictureBox1.BackColor.R.ToString("D3");
                g = pictureBox1.BackColor.G.ToString("D3");
                b = pictureBox1.BackColor.B.ToString("D3");
                if (serialPort1.IsOpen) serialPort1.Write("led:" + i.ToString("D3") + r + g + b + "\r\n");
            }



        }

        public void Display2(int i)
        {
            serialPort1.DiscardInBuffer();
            i--;
            Buttons[i].BackColor = pictureBox1.BackColor;
            Buttons[i].FlatAppearance.BorderColor = pictureBox1.BackColor;
            string r, g, b;
            r = pictureBox1.BackColor.R.ToString("D3");
            g = pictureBox1.BackColor.G.ToString("D3");
            b = pictureBox1.BackColor.B.ToString("D3");
            //MessageBox.Show("Button number is " + i);
            if (serialPort1.IsOpen) serialPort1.Write("led:" + i.ToString("D3") + r + g + b + "\r\n");
            // Thread.Sleep(10);

        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {

                string data = serialPort1.ReadLine();
                if (data.Substring(0, 4) == "led:")
                {
                    int led = int.Parse(data.Substring(4, 3));
                    int G = int.Parse(data.Substring(7, 3));
                    int R = int.Parse(data.Substring(10, 3));
                    int B = int.Parse(data.Substring(13, 3));
                    R = R * 3;
                    G = G * 3;
                    B = B * 3;

                    if (R >= 255) R = 255;
                    if (G >= 255) G = 255;
                    if (B >= 255) B = 255;
                    Buttons[led].BackColor = Color.FromArgb(R, G, B);
                    
                }
                richTextBox1.AppendText(data);
            }
            catch
            {

            }

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            serialPort1.DiscardInBuffer();
            for (int i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].BackColor = Form1.DefaultBackColor;
            }
            serialPort1.Write("mode:5\r\n"); Thread.Sleep(10);


        }

        private void label2_Click(object sender, EventArgs e)
        {

        }





        private void button3_Click(object sender, EventArgs e)
        {
            serialPort1.DiscardInBuffer();
            serialPort1.Write("mode:1\r\n"); Thread.Sleep(10);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            serialPort1.DiscardInBuffer();
            serialPort1.Write("mode:0\r\n"); Thread.Sleep(10);
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            serialPort1.DiscardInBuffer();
            serialPort1.Write("brightness:" + (255 - vScrollBar1.Value).ToString("D3") + "\r\n"); Thread.Sleep(10);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            serialPort1.DiscardInBuffer();
            serialPort1.Write("mode:2\r\n"); Thread.Sleep(10);
        }

        private void hScrollBar4_Scroll(object sender, ScrollEventArgs e)
        {
            serialPort1.DiscardInBuffer();
            serialPort1.Write("speed:" + hScrollBar4.Value.ToString("D3") + "\r\n"); Thread.Sleep(10);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen) serialPort1.DiscardInBuffer();
            String[] COMPorts = SerialPort.GetPortNames();
            comboBox1.Items.Clear();

            //將找到之現有COM加入Combo,Text中.
            foreach (string port in COMPorts) { comboBox1.Items.Add(port); }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() != DialogResult.Cancel)
            {
                pictureBox1.BackColor = colorDialog1.Color;  // 回傳選擇顏色，並且設定 Textbox 的背景顏色
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            com = comboBox1.Text;
            serialPort1.PortName = com;
            serialPort1.Open();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            draw = checkBox1.Checked;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (!System.IO.File.Exists("bitmap.txt"))
            {
                FileStream fs1 = new FileStream("bitmap.txt", FileMode.Create, FileAccess.Write);
                fs1.Close();
            }

            StreamWriter sw = new StreamWriter("bitmap.txt");

            for (int i = 0; i < 213; i++)
            {
              sw.WriteLine(Buttons[i].BackColor.R.ToString("D3"));
              sw.WriteLine(Buttons[i].BackColor.G.ToString("D3"));
              sw.WriteLine(Buttons[i].BackColor.B.ToString("D3"));
            }
            
           
            sw.Close();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            
           
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader("bitmap.txt");
                //Read the first line of text
           
              

                for(int i = 0; i < 213; i++)
                {
                    R = int.Parse(sr.ReadLine());
                    G = int.Parse(sr.ReadLine());
                    B = int.Parse(sr.ReadLine());
                   
                    //Thread.Sleep(10);
                    Buttons[i].BackColor = Color.FromArgb(R,G,B);
                }
                //close the file
                sr.Close();
                
            }
            catch 
            {
               
            }

            for (int i = 0; i < 213; i++)
            {
                if (serialPort1.IsOpen) serialPort1.Write("led:" + i.ToString("D3") + Buttons[i].BackColor.R.ToString("D3") + Buttons[i].BackColor.G.ToString("D3") + Buttons[i].BackColor.B.ToString("D3") + "\r\n");
                Thread.Sleep(35);
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
          
            
        }
    }
}
