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
        public BrightnessSettings(ref VideoCapture vc)
        {
            InitializeComponent();
            vcc = vc;
        }

        private void BrightnessSettings_Load(object sender, EventArgs e)
        {


            var timer1 = new System.Windows.Forms.Timer();
            timer1.Interval = 100;
            timer1.Tick += timer1_Tick;
            timer1.Start();

            System.Diagnostics.Debug.WriteLine(Settings1.Default.controllername);
            using (var sp = new System.IO.Ports.SerialPort(Settings1.Default.controllername, 9600, Parity.None, 8, StopBits.One))
            {
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
        }

        void timer1_Tick(object sender, EventArgs e)
        {


            pictureBox1.Image = new Bitmap(vcc.QueryFrame().ToBitmap(), new Size(960, 540));
        }



        private void button1_Click(object sender, EventArgs e)
        {
            // using (var sp = new System.IO.Ports.SerialPort("COM3"))
            using (var sp = new System.IO.Ports.SerialPort(Settings1.Default.controllername, 9600, Parity.None, 8, StopBits.One))
            {
                sp.Open();

                //   sp.Write(new byte[] { 0x81, 0x01, 0x06, 0x01, 0x04, 0x04, 0x01, 0x03, 0xFF }, 0, 9);
                //move to left


                sp.Write(new byte[] { 0x81, 0x01, 0x04, 0x0C, 0x02, 0xFF }, 0, 6);




                //           port.Write(command_buffer[0].raw_serial_data, 0, command_buffer[0].raw_serial_data.Length);
                /*
                             if (dispatched_cmd is pan_tilt_inquiry_command)  // Was a pan/tilt inquiry
        {
            num_cmds_since_inquiry = 0;
            last_inquiry_was_pan_tilt = true;
        }
                  command_buffer.Add(new zoom_jog_command(camera_num, speed, direction));  // Add it to the end of the buffer

                                    get
                {
                    Byte[] serial_data = new Byte[6];
                    serial_data[0] = VISCA_CODE.HEADER;
                    serial_data[0] |= (Byte)command_camera_num;
                    serial_data[1] = VISCA_CODE.COMMAND;
                    serial_data[2] = VISCA_CODE.CATEGORY_CAMERA1;
                    serial_data[3] = VISCA_CODE.ZOOM;
                    if (direction == ZOOM_DIRECTION.IN)
                        serial_data[4] = (byte)(VISCA_CODE.ZOOM_TELE_SPEED | zoom_speed);
                    else  // if (direction == ZOOM_DIRECTION.OUT
                        serial_data[4] = (byte)(VISCA_CODE.ZOOM_WIDE_SPEED | zoom_speed);
                    serial_data[5] = VISCA_CODE.TERMINATOR;

                    return serial_data;
                }
                    port.Write(connect_cmd.raw_serial_data, 0, connect_cmd.raw_serial_data.Length);
          
                    port.Write(inquiry.raw_serial_data, 0, inquiry.raw_serial_data.Length);
                */

                // var readData = sp.ReadLine();
                // Console.WriteLine(readData);
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
            if (promptValue == "mugpro2024")
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
        int onoff = 0;
        private void saveasdefaultbutton_Click(object sender, EventArgs e)
        {

            using (var sp = new System.IO.Ports.SerialPort(Settings1.Default.controllername, 9600, Parity.None, 8, StopBits.One))
            {
                sp.Open();
                if (onoff == 0)
                {
                     sp.Write(new byte[] { 0x81, 0x01, 0x06, 0x06, 0x02, 0xFF }, 0, 6);
                    sp.Write(new byte[] { 0x81, 0x01, 0x7E, 0x01, 0x02, 0x00, 0x01, 0xFF }, 0, 8);
                    //    sp.Write(new byte[] { 0x81, 0x01, 0x7E, 0x04, 0x40, 0x07, 0x00,0x01, 0xFF }, 0, 9);
                    //  sp.Write(new byte[] { 0x81, 0x01, 0x7E, 0x04, 0x40, 0x07, 0x00,0x00, 0xFF }, 0, 9);

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
       



    }

    public static class Prompt
    {
        public static string ShowDialog(string text, string caption)
        {
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
