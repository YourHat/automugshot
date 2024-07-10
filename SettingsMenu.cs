using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
        }

        private void changePathButton_Click(object sender, EventArgs e)
        {
            //change the folder path to save pictures
            folderBrowserDialog1.ShowDialog();
            folderPath = folderBrowserDialog1.SelectedPath;
            label2.Text=folderPath;
        }

        private void saveSettingsButton_Click(object sender, EventArgs e)
        {
            //change the override setting for pictures
            Settings1.Default.filepathforpic = folderPath;
            if(radioButton1.Checked == true) { Settings1.Default.overridepic = true; } else { Settings1.Default.overridepic = false; };
            Settings1.Default.Save();
            label2.Text = folderPath;
            label4.Visible = true;
        }
    }
}
