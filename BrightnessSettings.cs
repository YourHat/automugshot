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
using System.Diagnostics;

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

            pictureBox1.Image = new Bitmap(vcc.QueryFrame().ToBitmap(), new Size(960, 540)) ?? null;

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

        private void button1_Click_1(object sender, EventArgs e)
        {

            FaceDetectorYN InitializeFaceDetectionModel(Size inputSize) => new FaceDetectorYN(
                      model: "face_detection_yunet_2023mar.onnx",
                      config: string.Empty,
                      inputSize: inputSize,
                      scoreThreshold: 0.8f,
                      nmsThreshold: 0.3f,
                      topK: 5000,
                      backendId: Emgu.CV.Dnn.Backend.Default,
                      targetId: Target.Cpu
                      );
            bool inmiddle = false;
            Bitmap bm;
            Mat mugshotface;
            var facefeatures = new Mat();
            float[] face;
            float diff = 0f;
            int rpp = 0;
            using (var sp = new System.IO.Ports.SerialPort(Settings1.Default.controllername, 9600, Parity.None, 8, StopBits.One))
            {
                sp.Open();
                sp.Write(new byte[] { 0x81, 0x01, 0x04, 0x47, 0x00, 0x00, 0x00, 0x00, 0xFF }, 0, 9);
                sp.Write(new byte[] { 0x81, 0x01, 0x06, 0x04, 0xFF }, 0, 5);
                Thread.Sleep(500);
                //  while (!inmiddle)
                //  {// move left and right
                bm = vcc.QueryFrame().ToBitmap();

                mugshotface = bm.ToMat();
                using var model = InitializeFaceDetectionModel(new Size(mugshotface.Width, mugshotface.Height));
                model.Detect(mugshotface, facefeatures);

                var facedata = (float[,])facefeatures.GetData(jagged: true);

                if (facedata != null)
                {
                    face = new float[] { facedata[0, 0], facedata[0, 1], facedata[0, 2], facedata[0, 3] };
                    if ((mugshotface.Width / 2) < face[0] + (face[2] / 2f))
                    {
                        float tempfacemiddle = face[0] + (face[2] / 2f);
                        // if (rpp != 0 && rpp != 1) inmiddle = true;
                        sp.Write(new byte[] { 0x81, 0x01, 0x06, 0x03, 0x18, 0x18, 0x00, 0x00, 0x04, 0x08, 0x00, 0x00, 0x00, 0x00, 0xFF }, 0, 15);
                        Thread.Sleep(500);
                        bm = vcc.QueryFrame().ToBitmap();
                        mugshotface = bm.ToMat();
                        using var model2 = InitializeFaceDetectionModel(new Size(mugshotface.Width, mugshotface.Height));
                        model2.Detect(mugshotface, facefeatures);
                        facedata = (float[,])facefeatures.GetData(jagged: true);
                        face = new float[] { facedata[0, 0], facedata[0, 1], facedata[0, 2], facedata[0, 3] };
                        float diffx = (((face[0] + (face[2] / 2f)) - (mugshotface.Width / 2f)) / ((tempfacemiddle - (face[0] + (face[2] / 2f))) / 5f) * 12.4f);
                        Debug.WriteLine(diffx.ToString());
                        var bytelist = getbfromi((short)diffx);
                        sp.Write(new byte[] { 0x81, 0x01, 0x06, 0x03, 0x18, 0x18, bytelist[4], bytelist[5], bytelist[6], bytelist[7], 0x00, 0x00, 0x00, 0x00, 0xFF }, 0, 15);
                    }
                    else
                    {
                        float tempfacemiddle = face[0] + (face[2] / 2);

                        sp.Write(new byte[] { 0x81, 0x01, 0x06, 0x03, 0x18, 0x18, 0x0F, 0x0F, 0x0B, 0x08, 0x00, 0x00, 0x00, 0x00, 0xFF }, 0, 15);
                        Thread.Sleep(500);
                        bm = vcc.QueryFrame().ToBitmap();
                        mugshotface = bm.ToMat();
                        using var model2 = InitializeFaceDetectionModel(new Size(mugshotface.Width, mugshotface.Height));
                        model2.Detect(mugshotface, facefeatures);
                        facedata = (float[,])facefeatures.GetData(jagged: true);
                        face = new float[] { facedata[0, 0], facedata[0, 1], facedata[0, 2], facedata[0, 3] };
                        float diffx = ((mugshotface.Width / 2f) - (face[0] + (face[2] / 2f))) / (((face[0] + (face[2] / 2f)) - tempfacemiddle) / 5f);
                        Debug.WriteLine(diffx.ToString());
                        var bytelist = getbfromi((short)(65535 - (diffx * 12.4f)));
                        sp.Write(new byte[] { 0x81, 0x01, 0x06, 0x03, 0x18, 0x18, bytelist[4], bytelist[5], bytelist[6], bytelist[7], 0x00, 0x00, 0x00, 0x00, 0xFF }, 0, 15);
                    }

                }
                else
                {
                    //return false;
                }
                pictureBox1.Image = new Bitmap(vcc.QueryFrame().ToBitmap(), new Size(960, 540)) ?? null;
                pictureBox1.Update();
                //     }
                /*
                  bool incenter = false;
                  int rp = 0;
                  while (!incenter)
                  {// move top and bottom
                      bm = vcc.QueryFrame().ToBitmap();

                      mugshotface = bm.ToMat();
                      using var model = InitializeFaceDetectionModel(new Size(mugshotface.Width, mugshotface.Height));
                      model.Detect(mugshotface, facefeatures);

                      var facedata = (float[,])facefeatures.GetData(jagged: true);

                      if (facedata != null)
                      {
                          face = new float[] { facedata[0, 0], facedata[0, 1], facedata[0, 2], facedata[0, 3] };
                          if ((mugshotface.Height / 2) < face[1] + (face[3] / 2))
                          {
                              if (rp != 0 && rp != 1) incenter = true;

                              sp.Write(new byte[] { 0x81, 0x01, 0x06, 0x03, 0x18, 0x18, 0x00, 0x00, 0x00, 0x00, 0x0F, 0x0F, 0x0B, 0x08, 0xFF }, 0, 15);

                              rp = 1;
                          }
                          else
                          {
                              if (rp != 0 && rp != 2) incenter = true;

                              sp.Write(new byte[] { 0x81, 0x01, 0x06, 0x03, 0x18, 0x18, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x04, 0x08, 0xFF }, 0, 15);

                              rp = 2;
                          }

                      }
                      else
                      {
                          //vcc false;
                      }
                  }

                  int zv = 1;
                  bool zoomright = false;
                  short zoomvalue = 1000;
                  while (!zoomright)
                  {// zoom in
                      bm = vcc.QueryFrame().ToBitmap();

                      mugshotface = bm.ToMat();
                      using var model = InitializeFaceDetectionModel(new Size(mugshotface.Width, mugshotface.Height));
                      model.Detect(mugshotface, facefeatures);

                      var facedata = (float[,])facefeatures.GetData(jagged: true);

                      if (facedata != null)
                      {
                          face = new float[] { facedata[0, 0], facedata[0, 1], facedata[0, 2], facedata[0, 3] };
                          if ((mugshotface.Height) > face[3] * 5)
                          {

                              sp.Write(getbfromi((short)(zoomvalue * zv)), 0, 9);
                          }
                          else
                          {
                              zoomright = true;
                              sp.Write(getbfromi((short)((float)zoomvalue * 1.1f * (zv - 1))), 0, 9);
                          }

                      }
                      else
                      {
                          //return false;
                      }
                      zv++;
                      zoomvalue = (short)((float)zoomvalue / 0.99f);
                      if (zv > 20) zoomright = true;
                  }
                  System.Diagnostics.Debug.WriteLine(zv.ToString());
                */
                sp.Close();
            }
            // return true;

        }
        public byte[] getbfromi(short zoomvalue)
        {// get called for zooming in function
            byte[] result = new byte[9];

            byte[] ba = BitConverter.GetBytes(zoomvalue);
            string bas = BitConverter.ToString(ba);
            result[0] = 0x81;
            result[1] = 0x01;
            result[2] = 0x04;
            result[3] = 0x47;
            result[4] = Byte.Parse(bas[3].ToString(), System.Globalization.NumberStyles.HexNumber);
            result[5] = Byte.Parse(bas[4].ToString(), System.Globalization.NumberStyles.HexNumber);
            result[6] = Byte.Parse(bas[0].ToString(), System.Globalization.NumberStyles.HexNumber);
            result[7] = Byte.Parse(bas[1].ToString(), System.Globalization.NumberStyles.HexNumber);
            result[8] = 0xFF;
            return result;
        }

        // test button
        private void button2_Click_1(object sender, EventArgs e)
        {
             using (var sp = new System.IO.Ports.SerialPort(Settings1.Default.controllername, 9600, Parity.None, 8, StopBits.One))
            {// zoom out when the menu is loaded
                sp.Open();
                sp.Write(new byte[] { 0x81, 0x01, 0x04, 0x39, 0x00, 0xFF }, 0, 6);
                Thread.Sleep(500);
                sp.Write(new byte[] { 0x81, 0x01, 0x04, 0x39, 0x0D, 0xFF }, 0, 6);
                sp.Close();
            }
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
