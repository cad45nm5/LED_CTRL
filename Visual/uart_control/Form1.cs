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
namespace uart_control
{
    public partial class Form1 : Form
    {



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
                //Buttons[i].Click += (sender1, ex) => this.Display(index + 1);
                Buttons[i].MouseEnter += (sender1, ex) => this.Display(index + 1);
                this.Controls.Add(Buttons[i]);
            }
        }
        public void Display(int i)
        {
            i--;
            Buttons[i].BackColor = pictureBox1.BackColor;
            Buttons[i].FlatAppearance.BorderColor = pictureBox1.BackColor;
            string r, g, b;
            r = hScrollBar3.Value.ToString("D3");
            g = hScrollBar2.Value.ToString("D3");
            b = hScrollBar1.Value.ToString("D3");
            //MessageBox.Show("Button number is " + i);
            serialPort1.Write("led:" + i.ToString("D3") + r + g + b + "\r\n");
            Thread.Sleep(10);

        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(50);
            string data = serialPort1.ReadLine();
            richTextBox1.AppendText(data);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].BackColor = Form1.DefaultBackColor;
            }
            serialPort1.Write("mode:5\r\n"); Thread.Sleep(10);


        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void hScrollBar3_Scroll(object sender, ScrollEventArgs e)
        {
            Color C = Color.FromArgb(hScrollBar3.Value, hScrollBar2.Value, hScrollBar1.Value);
            pictureBox1.BackColor = C;
        }

        private void hScrollBar2_Scroll(object sender, ScrollEventArgs e)
        {
            Color C = Color.FromArgb(hScrollBar3.Value, hScrollBar2.Value, hScrollBar1.Value);
            pictureBox1.BackColor = C;
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

            Color C = Color.FromArgb(hScrollBar3.Value, hScrollBar2.Value, hScrollBar1.Value);
            pictureBox1.BackColor = C;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            serialPort1.Write("mode:1\r\n"); Thread.Sleep(10);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            serialPort1.Write("mode:0\r\n"); Thread.Sleep(10);
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            serialPort1.Write("brightness:"+(255-vScrollBar1.Value).ToString("D3")+"\r\n"); Thread.Sleep(10);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            serialPort1.Write("mode:2\r\n"); Thread.Sleep(10);
        }

        private void hScrollBar4_Scroll(object sender, ScrollEventArgs e)
        {
            serialPort1.Write("speed:" +hScrollBar4.Value.ToString("D3") + "\r\n"); Thread.Sleep(10);
        }
    }
}
