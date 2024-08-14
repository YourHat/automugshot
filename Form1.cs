using Emgu.CV;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace automugshot;

public partial class mainMenu : Form
{

    FrontMugshot[] FrontMugs;
    SideMugshot[] SideMugs;
    int selectedfront = 9;
    int selectedside = 9;
    List<PictureBox> pblist = new List<PictureBox>();
   // List<Label> lpblist = new List<Label>();
    List<Label> selist = new List<Label>();

    List<Label> test1list = new List<Label>();
    List<Label> test2list = new List<Label>();
    List<Label> test3list = new List<Label>();

    public VideoCapture capturedimage = new VideoCapture(Settings1.Default.cameraindex);

    public mainMenu()
    {
        InitializeComponent();
        FrontMugs = new FrontMugshot[4];
        SideMugs = new SideMugshot[4];
        pblist.Add(pictureBox1);
        pblist.Add(pictureBox2);
        pblist.Add(pictureBox3);
        pblist.Add(pictureBox4);
        pblist.Add(pictureBox5);
        pblist.Add(pictureBox6);
        pblist.Add(pictureBox7);
        pblist.Add(pictureBox8);

        test1list.Add(eye1);
        test1list.Add(eye2);
        test1list.Add(eye3);
        test1list.Add(eye4);
        test1list.Add(eye5);
        test1list.Add(eye6);
        test1list.Add(eye7);
        test1list.Add(eye8);

        test2list.Add(head1);
        test2list.Add(head2);
        test2list.Add(head3);
        test2list.Add(head4);
        test2list.Add(head5);
        test2list.Add(head6);
        test2list.Add(head7);
        test2list.Add(head8);

        test3list.Add(face1);
        test3list.Add(face2);
        test3list.Add(face3);
        test3list.Add(face4);
        test3list.Add(face5);
        test3list.Add(face6);
        test3list.Add(face7);
        test3list.Add(face8);

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
        for (int i = 0; i < pblist.Count; i++)
        {
            pblist[i].Image = null;
            test1list[i].Visible = false;
            test2list[i].Visible = false;
            test3list[i].Visible = false;
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
                pblist[selectedside].Image.Save(@"" + Settings1.Default.filepathforpic + "\\mugSide.jpeg", System.Drawing.Imaging.ImageFormat.Bmp);

            }
            else
            {
                pblist[selectedfront].Image.Save(@"" + Settings1.Default.filepathforpic + "\\mugfront" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpeg", System.Drawing.Imaging.ImageFormat.Bmp);
                pblist[selectedside].Image.Save(@"" + Settings1.Default.filepathforpic + "\\mugSide" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpeg", System.Drawing.Imaging.ImageFormat.Bmp);

            }
        }
        System.Diagnostics.Debug.WriteLine("Mugshot Saved");
    }

    private void settingsMenu_Click(object sender, EventArgs e)
    {
        //open setting window
        //set path
        //overwrite mugshots or save name as date and time to not overwrite mugshots
        var settingsform = new saveSettings(ref capturedimage);
        settingsform.ShowDialog(this);
       // capturedimage = new VideoCapture(Settings1.Default.cameraindex);
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
        // VideoCapture capturedimage = new VideoCapture(Settings1.Default.cameraindex);



        for (int i = 0; i < 4; i++)
        {
            SideMugs[i] = new SideMugshot(capturedimage.QueryFrame().ToBitmap());
            if (SideMugs[i].isGoodMugshot == false || SideMugs[i] == null)
            {

                pblist[i + 4].Image = Image.FromFile(@".\errorface.jpg");
                test1list[i + 4].Visible = false;
                test2list[i + 4].Visible = false;
                test3list[i + 4].Visible = false;
            }
            else{

                pblist[i + 4].Image = new Bitmap(SideMugs[i].croppedbm, new Size(150, 200));
                if (SideMugs[i].isEyeOpen)
                    test1list[i + 4].ForeColor = Color.Green;
                else
                    test1list[i + 4].ForeColor = Color.Yellow;

                if (SideMugs[i].isFacingSide)
                    test2list[i + 4].ForeColor = Color.Green;
                else
                    test2list[i + 4].ForeColor = Color.Red;

                if (SideMugs[i].isFacingLeftSide)
                    test3list[i + 4].ForeColor = Color.Green;
                else
                    test3list[i + 4].ForeColor = Color.Red;

                test1list[i + 4].Visible = true;
                test2list[i + 4].Visible = true;
                test3list[i + 4].Visible = true;
            }
            pblist[i+4].Update();
            Thread.Sleep(1000);

        }


    }

    private void takeFrontPic_Click(object sender, EventArgs e)
    {
        //take picture of the front
        //take four
        //if good, green line, not good then red line

        // VideoCapture capturedimage = new VideoCapture(Settings1.Default.cameraindex);



        for (int i = 0; i < 4; i++)
        {
            FrontMugs[i] = new FrontMugshot(capturedimage.QueryFrame().ToBitmap());
            if (FrontMugs[i].isGoodMugshot == false || FrontMugs[i] == null)
            {

                pblist[i + 4].Image = Image.FromFile(@".\errorface.jpg");
                test1list[i].Visible = false;
                test2list[i].Visible = false;
                test3list[i].Visible = false;
            }
            else
            {
                pblist[i].Image = new Bitmap(FrontMugs[i].croppedbm, new Size(150, 200));
                // pblist[i].Image = FrontMugs[i].croppedbm;
                if (FrontMugs[i].areEyesOpen)
                    test1list[i].ForeColor = Color.Green;
                else
                    test1list[i].ForeColor = Color.Red;

                if (FrontMugs[i].isHeadTilted)
                    test2list[i].ForeColor = Color.Green;
                else
                    test2list[i].ForeColor = Color.Red;

                if (FrontMugs[i].isFacingfront)
                    test3list[i].ForeColor = Color.Green;
                else
                    test3list[i].ForeColor = Color.Red;


                test1list[i].Visible = true;
                test2list[i].Visible = true;
                test3list[i].Visible = true;
            }
            pblist[i].Update();
            Thread.Sleep(1000);
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

    private void mainMenu_Load(object sender, EventArgs e)
    {
        this.WindowState = FormWindowState.Minimized;
        this.WindowState = FormWindowState.Normal;
        this.Focus(); this.Show();
    }
}
