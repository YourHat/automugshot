using Emgu.CV;
using Emgu.CV.Dai;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.IO.Ports;
using System.Xml.Linq;
using Emgu.CV.Dnn;
using System.Numerics;
using System.IO.Packaging;

namespace automugshot
{
    public partial class BrightnessSettings : Form
    {

        VideoCapture vcc;
        int onoff;
        public BrightnessSettings(VideoCapture vc)
        {
            InitializeComponent();
            vcc = vc;
        }

        private void BrightnessSettings_Load(object sender, EventArgs e)
        {


            System.Diagnostics.Debug.WriteLine(Settings1.Default.controllername);
            using (var sp = new System.IO.Ports.SerialPort(Settings1.Default.controllername, 9600, Parity.None, 8, StopBits.One))
            {// zoom out when the menu is loaded
                sp.Open();
                sp.Write(new byte[] { 0x81, 0x01, 0x04, 0x47, 0x00, 0x00, 0x00, 0x00, 0xFF }, 0, 9);
                sp.Write(new byte[] { 0x81, 0x01, 0x04, 0x39, 0x03, 0xFF }, 0, 6);
                sp.Close();
            }
            higherflbutton.Enabled = false;
            lowerflbutton.Enabled = false;
            higherssbutton.Enabled = false;
            lowerssbutton.Enabled = false;
            saveasdefaultbutton.Enabled = false;
            this.ControlBox = false;
            onoff = 0;

            // show video
            var timer1 = new System.Windows.Forms.Timer();
            timer1.Interval = 50;
            timer1.Tick += timer1_Tick;
            timer1.Start();
        }

        void timer1_Tick(object sender, EventArgs e)
        {

                pictureBox1.Image = new Bitmap(vcc.QueryFrame().ToBitmap(), new Size(960, 540))?? null;

        }



        private void button1_Click(object sender, EventArgs e)
        {
         
            using (var sp = new System.IO.Ports.SerialPort(Settings1.Default.controllername, 9600, Parity.None, 8, StopBits.One))
            {
                sp.Open();
                sp.Write(new byte[] { 0x81, 0x01, 0x04, 0x0C, 0x02, 0xFF }, 0, 6);
                sp.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var sp = new System.IO.Ports.SerialPort(Settings1.Default.controllername, 9600, Parity.None, 8, StopBits.One))
            {
                sp.Open();
                sp.Write(new byte[] { 0x81, 0x01, 0x04, 0x0C, 0x03, 0xFF }, 0, 6);
                sp.Close();
            }
        }


        private void AdminButton_Click(object sender, EventArgs e)
        {
            string promptValue = Prompt.ShowDialog("Admin Login", "Please type password in and click Okay");
            if (promptValue == "mugpro24")
            {
                higherflbutton.Enabled = true;
                lowerflbutton.Enabled = true;
                higherssbutton.Enabled = true;
                lowerssbutton.Enabled = true;
                saveasdefaultbutton.Enabled = true;
            }

        }

        private void lowerssbutton_Click(object sender, EventArgs e)
        {
            using (var sp = new System.IO.Ports.SerialPort(Settings1.Default.controllername, 9600, Parity.None, 8, StopBits.One))
            {
                sp.Open();
                sp.Write(new byte[] { 0x81, 0x01, 0x04, 0x0A, 0x03, 0xFF }, 0, 6);
                sp.Close();
            }
        }

        private void higherssbutton_Click(object sender, EventArgs e)
        {
            using (var sp = new System.IO.Ports.SerialPort(Settings1.Default.controllername, 9600, Parity.None, 8, StopBits.One))
            {
                sp.Open();
                sp.Write(new byte[] { 0x81, 0x01, 0x04, 0x0A, 0x02, 0xFF }, 0, 6);
                sp.Close();
            }
        }

        private void lowerflbutton_Click(object sender, EventArgs e)
        {
            using (var sp = new System.IO.Ports.SerialPort(Settings1.Default.controllername, 9600, Parity.None, 8, StopBits.One))
            {
                sp.Open();
                sp.Write(new byte[] { 0x81, 0x01, 0x04, 0x0B, 0x03, 0xFF }, 0, 6);
                sp.Close();
            }
        }

        private void higherflbutton_Click(object sender, EventArgs e)
        {
            using (var sp = new System.IO.Ports.SerialPort(Settings1.Default.controllername, 9600, Parity.None, 8, StopBits.One))
            {
                sp.Open();
                sp.Write(new byte[] { 0x81, 0x01, 0x04, 0x0B, 0x02, 0xFF }, 0, 6);
                sp.Close();
            }
        }
        
        private void saveasdefaultbutton_Click(object sender, EventArgs e)
        {

            using (var sp = new System.IO.Ports.SerialPort(Settings1.Default.controllername, 9600, Parity.None, 8, StopBits.One))
            {
                sp.Open();
                       sp.Write(new byte[] { 0x81, 0x01, 0x06, 0x06, 0x03, 0xFF }, 0, 6);
                if (onoff == 0)
                {
                   
                    sp.Write(new byte[] { 0x81, 0x01, 0x06, 0x06, 0x02, 0xFF }, 0, 6);
                    sp.Write(new byte[] { 0x81, 0x01, 0x7E, 0x01, 0x02, 0x00, 0x01, 0xFF }, 0, 8);
                    onoff = 1;
                }
                else
                {
                    sp.Write(new byte[] { 0x81, 0x01, 0x06, 0x06, 0x03, 0xFF }, 0, 6);
                    onoff = 0;
                }
                sp.Close();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (var sp = new System.IO.Ports.SerialPort(Settings1.Default.controllername, 9600, Parity.None, 8, StopBits.One))
            {
                sp.Open();
                sp.Write(new byte[] { 0x81, 0x01, 0x06, 0x06, 0x03, 0xFF }, 0, 6);
                sp.Close();
            }
            this.Close();
        }
    }

    public static class Prompt
    {
        public static string ShowDialog(string text, string caption)
        {// for typing admin passwokrd
            Form prompt = new Form()
            {
                Width = 300,
                Height = 200,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 200 };
            Button confirmation = new Button() { Text = "Ok", Left = 150, Width = 100, Top =100, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }
    }

}
