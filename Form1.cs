using System.Drawing.Imaging;
using System.Windows.Forms;

namespace automugshot;

public partial class mainMenu : Form
{

    Takepicture takepics;
    int selectedfront = 9;
    int selectedside = 9;
    List<PictureBox> pblist = new List<PictureBox>();
    List<Label> lpblist = new List<Label>();
    List<Label> selist = new List<Label>();

    public mainMenu()
    {
        InitializeComponent();
        takepics = new Takepicture();
        pblist.Add(pictureBox1);
        pblist.Add(pictureBox2);
        pblist.Add(pictureBox3);
        pblist.Add(pictureBox4);
        pblist.Add(pictureBox5);
        pblist.Add(pictureBox6);
        pblist.Add(pictureBox7);
        pblist.Add(pictureBox8);

        lpblist.Add(lpb1);
        lpblist.Add(lpb2);
        lpblist.Add(lpb3);
        lpblist.Add(lpb4);
        lpblist.Add(lpb5);
        lpblist.Add(lpb6);
        lpblist.Add(lpb7);
        lpblist.Add(lpb8);


        selist.Add(sp1);
        selist.Add(sp2);
        selist.Add(sp3);
        selist.Add(sp4);
        selist.Add(sp5);
        selist.Add(sp6);
        selist.Add(sp7);
        selist.Add(sp8);
    }

    private void newPic_Click(object sender, EventArgs e)
    {
        // reset pictures
        for(int i = 0; i < lpblist.Count; i++)
        {
            pblist[i].Image = null;
            lpblist[i].Visible = false;
            selist[i].Visible = false;
        }
    }

    private void savePics_Click(object sender, EventArgs e)
    {
        //save selected pictures to the specified path
        if (selectedfront != 9 && selectedside != 9)
        {
            if (Settings1.Default.overridepic == true)
            {
                pblist[selectedfront].Image.Save(@"" + Settings1.Default.filepathforpic + "\\mugfront.jpeg", System.Drawing.Imaging.ImageFormat.Bmp);
                pblist[selectedfront].Image.Save(@"" + Settings1.Default.filepathforpic + "\\mugSide.jpeg", System.Drawing.Imaging.ImageFormat.Bmp);

            }
            else
            {
                pblist[selectedfront].Image.Save(@"" + Settings1.Default.filepathforpic + "\\mugfront"+ DateTime.Now.ToString("yyyyMMddHHmmss")+ ".jpeg", System.Drawing.Imaging.ImageFormat.Bmp);
                pblist[selectedfront].Image.Save(@"" + Settings1.Default.filepathforpic + "\\mugSide" +DateTime.Now.ToString("yyyyMMddHHmmss") +".jpeg", System.Drawing.Imaging.ImageFormat.Bmp);

            }
        }
    }

    private void settingsMenu_Click(object sender, EventArgs e)
    {
        //open setting window
        //set path
        //overwrite mugshots or save name as date and time to not overwrite mugshots
        var settingsform = new saveSettings();
        settingsform.ShowDialog(this);
    }

    private void helpMenu_Click(object sender, EventArgs e)
    {
        //open help window
        //explains how to use it
        //put my name, sgt stryd, and kcso name at the bottom

        var helpform = new HelpMenu();
        helpform.ShowDialog(this);
    }

    private void takeSidePic_Click(object sender, EventArgs e)
    {
        //take picture of the side
        //take four
        //if good, green line, not good then red line
        var sidepictures = takepics.takesidepic();
        for (int i = 0; i < sidepictures.Count; i++)
        {
            pblist[i + 4].Image = sidepictures[i].picimg;
            if (sidepictures[i].isgoodpic)
            {
                lpblist[i + 4].Text = "Good";
                lpblist[i + 4].ForeColor = Color.Green;

            }
            else
            {
                lpblist[i + 4].Text = "Bad";
                lpblist[i + 4].ForeColor = Color.Red;
            }
            lpblist[i + 4].Visible = true;
        }
    }

    private void takeFrontPic_Click(object sender, EventArgs e)
    {
        //take picture of the front
        //take four
        //if good, green line, not good then red line



        var frontpictures = takepics.takefrontpic();
        for (int i = 0; i < frontpictures.Count; i++)
        {
            pblist[i].Image = frontpictures[i].picimg;
            if (frontpictures[i].isgoodpic)
            {
                lpblist[i].Text = "Good";
                lpblist[i].ForeColor = Color.Green;

            }
            else
            {
                lpblist[i].Text = "Bad";
                lpblist[i].ForeColor = Color.Red;
            }
            lpblist[i].Visible = true;
        }
    }

    private void pictureBox1_Click(object sender, EventArgs e)
    {
        changeselectedfront(0);
    }

    private void pictureBox2_Click(object sender, EventArgs e)
    {
        changeselectedfront(1);
    }

    private void pictureBox3_Click(object sender, EventArgs e)
    {
        changeselectedfront(2);
    }

    private void pictureBox4_Click(object sender, EventArgs e)
    {
        changeselectedfront(3);
    }

    private void changeselectedfront(int index)
    {
        if (selectedfront != 9)
        {
            selist[selectedfront].Visible = false;
        }
        selectedfront = index;
        selist[selectedfront].Visible = true;

    }

    private void pictureBox5_Click(object sender, EventArgs e)
    {
        changeselectedside(4);
    }

    private void pictureBox6_Click(object sender, EventArgs e)
    {
        changeselectedside(5);
    }

    private void pictureBox7_Click(object sender, EventArgs e)
    {
        changeselectedside(6);
    }

    private void pictureBox8_Click(object sender, EventArgs e)
    {
        changeselectedside(7);
    }

    private void changeselectedside(int index)
    {
        if (selectedside != 9)
        {
            selist[selectedside].Visible = false;
        }
        selectedside = index;
        selist[selectedside].Visible = true;

    }
}
