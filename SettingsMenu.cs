using Emgu.CV;
using Hompus.VideoInputDevices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace automugshot
{
    public partial class saveSettings : Form
    {
        string folderPath;
        VideoCapture vcc;
        public saveSettings(ref VideoCapture vc)
        {
            InitializeComponent();
            vcc = vc;
            folderPath = Settings1.Default.filepathforpic;
            label2.Text = folderPath;
            label4.Visible = false;
            if (Settings1.Default.overridepic == true)
            {
                radioButton1.Checked = true;
            }
            else
            {
                radioButton2.Checked = true;
            }
            using (var sde = new SystemDeviceEnumerator())
            {

                var devices = sde.ListVideoInputDevice();
                foreach (var device in devices)
                {
                    cameralistbox.Items.Add(String.Format("{1}", device.Key, device.Value));
                }

            }
        }

        private void changePathButton_Click(object sender, EventArgs e)
        {
            //change the folder path to save pictures
            folderBrowserDialog1.ShowDialog();
            folderPath = folderBrowserDialog1.SelectedPath;
            label2.Text = folderPath;
        }

        private void saveSettingsButton_Click(object sender, EventArgs e)
        {
            //change the override setting for pictures
            Settings1.Default.filepathforpic = folderPath;
            if (radioButton1.Checked == true) { Settings1.Default.overridepic = true; } else { Settings1.Default.overridepic = false; };
            if(cameralistbox.SelectedIndex != -1) { Settings1.Default.cameraindex = cameralistbox.SelectedIndex; }

            Settings1.Default.Save();
            label2.Text = folderPath;

            loadingscreen.ShowSplashScreen("saving...");
            vcc = new VideoCapture(Settings1.Default.cameraindex);
            loadingscreen.CloseForm();
            label4.Visible = true;


        }

        private void saveSettings_Load(object sender, EventArgs e)
        {
 //           VideoCapture capturedimage = new VideoCapture(1);
    
            //cameralistbox.SetSelected(Settings1.Default.cameraindex, true);
        }
    }
}
