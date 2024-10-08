﻿using Emgu.CV;
using Hompus.VideoInputDevices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace automugshot
{
    public partial class saveSettings : Form
    {
        string folderPath;

        public saveSettings()
        {
            InitializeComponent();
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

            if (Settings1.Default.filename == 0)
            {
                inmatedateradiobutton.Checked = true;
            }
            else
            {
                dateradiobutton.Checked = true;
            }

            if (Settings1.Default.openfolder == true) { openfoldercheckbox.Checked = true; }

            // get camera connected with USB cables
            using (var sde = new SystemDeviceEnumerator())
            {

                var devices = sde.ListVideoInputDevice();
                foreach (var device in devices)
                {
                    cameralistbox.Items.Add(String.Format("{1}", device.Key, device.Value));
                }

            }

            // get serial controllers
            ManagementClass processClass = new ManagementClass("Win32_PnPEntity");
            ManagementObjectCollection Ports = processClass.GetInstances();
            foreach (ManagementObject property in Ports)
            {
                var name = property.GetPropertyValue("Name");
                if (name != null && name.ToString().Contains("USB") && name.ToString().Contains("COM"))
                {
                    ControllerBox.Items.Add(name);
                }
            }
            ControllerBox.Items.Add("No Controller");

            widthbox.Text = Settings1.Default.combinedmugwidth.ToString(); ;
            heightbox.Text = Settings1.Default.combinedmugheight.ToString();

            if (Settings1.Default.combinemugshots == true)
            {
                widthbox.Enabled = true;
                heightbox.Enabled = true;
                combinepiccheckbox.Checked = true;
            }
            else
            {
                widthbox.Enabled = false;
                heightbox.Enabled = false;
                combinepiccheckbox.Checked = false;
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
            if (inmatedateradiobutton.Checked == true) { Settings1.Default.filename = 0; } else { Settings1.Default.filename = 1; }
            if (cameralistbox.SelectedIndex > -1) { Settings1.Default.cameraindex = cameralistbox.SelectedIndex; }

            if (combinepiccheckbox.Checked == true)
            {
                Settings1.Default.combinedmugwidth = int.Parse(widthbox.Text);
                Settings1.Default.combinedmugheight = int.Parse(heightbox.Text);
                Settings1.Default.combinemugshots = true;
            }
            else
            {
                Settings1.Default.combinemugshots = false;
            }


            string comnumber = @"COM[0-9]"; //regex
            Regex rg = new Regex(comnumber);
            if (ControllerBox.SelectedIndex > -1)
            {
                MatchCollection mc = rg.Matches(ControllerBox.GetItemText(ControllerBox.SelectedItem));
                if (mc.Count > 0) { Settings1.Default.controllername = mc[0].Value; }
                else { Settings1.Default.controllername = "Null"; }
            }
            if (openfoldercheckbox.Checked)
            {
                Settings1.Default.openfolder = true;
            }
            else { Settings1.Default.openfolder = false; }

            //Prolific USB-to-Serial Comm Port (COM5)
            Settings1.Default.Save();
            label2.Text = folderPath;
            label4.Visible = true;


        }

        private void combinepiccheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (combinepiccheckbox.Checked == true)
            {
                widthbox.Enabled = true;
                heightbox.Enabled = true;
            }
            else
            {
                widthbox.Enabled = false;
                heightbox.Enabled = false;
            }
        }

        // only let numbers to be typed in
        private void combinekeypress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar); ;
        }
    }
}
