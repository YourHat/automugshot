using Emgu.CV;
using Emgu.CV.Dnn;
using Emgu.CV.Face;
using Emgu.CV.Flann;
using Emgu.CV.Structure;
using Hompus.VideoInputDevices;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO.Ports;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace automugshot;

public partial class mainMenu : Form
{

    FrontMugshot[] FrontMugs; // list of front mugshot
    SideMugshot[] SideMugs; // list of side mugshot
    int selectedfront = 9;
    int selectedside = 9;
    List<PictureBox> pblist; //list of picture box for mugshots
    List<Label> selist; // list of lables for "selected"
    List<Label> test1list; // list of labels for eyes
    List<Label> test2list;// list of labels for head tiltle / facing side
    List<Label> test3list; // list of labels for facing front and left side
    List<System.Windows.Forms.Button> bl;

    public VideoCapture capturedimage;

    public mainMenu()
    {
        InitializeComponent();
        FrontMugs = new FrontMugshot[4];
        SideMugs = new SideMugshot[4];
        pblist = new List<PictureBox>() { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6, pictureBox7, pictureBox8 };
        test1list = new List<Label>() {eye1,eye2,eye3,eye4,eye5,eye6,eye7,eye8 };
        selist = new List<Label>() { sp1, sp2, sp3, sp4, sp5, sp6, sp7, sp8};
        test2list = new List<Label>() { head1, head2, head3, head4, head5, head6, head7, head8 };
        test3list = new List<Label>() { face1, face2, face3, face4, face5, face6, face7, face8 };


        capturedimage = new VideoCapture(Settings1.Default.cameraindex); // initiate videocapture
        if (!capturedimage.IsOpened)
        {
            Settings1.Default.cameraindex = 0;
            Settings1.Default.Save();
            capturedimage = new VideoCapture(Settings1.Default.cameraindex);
        }
            try // check and see if controller for camera is connected and selected
            {
            using (var sp = new System.IO.Ports.SerialPort(Settings1.Default.controllername, 9600, Parity.None, 8, StopBits.One))
            {
                sp.Open();
                sp.Write(new byte[] { 0x81, 0x01, 0x04, 0x39, 0x00, 0xFF }, 0, 6); // change to CAM_AE Full Auto
                Thread.Sleep(200);
                sp.Write(new byte[] { 0x81, 0x01, 0x04, 0x39, 0x0D, 0xFF }, 0, 6); // change to CAM_AE Bright
                sp.Close();
            }
            
        }
        catch (Exception ex)
        {
          
        }

        if (Settings1.Default.overridepic == false && Settings1.Default.filename == 0) // check and see if override setting is true or false
        {
            inmateinfotextbox.Enabled = true;
            inmateinfortext.Enabled = true;
        }
        else
        {
            inmateinfotextbox.Enabled = false;
            inmateinfortext.Enabled = false;
        }
        switch (Settings1.Default.cameraspeed) // movement speed of the PTZ camera
        {
            case 0:
                SlowButton.ForeColor = Color.Red;
                break;
            case 1:
                MediumButton.ForeColor = Color.Red;
                break;
            case 2:
                FastButton.ForeColor = Color.Red;
                break;
        }
        camerastatlabel.ForeColor = Color.Green;
        camerastatlabel.Text = "Ready!";
     //   controllerbuttonchange(Settings1.Default.controllerwork);

        // UI of the buttons

        bl = new List<System.Windows.Forms.Button>() { newPic, savePics, CalibrateButton,takeFrontPic,takeSidePic,settingsMenu,helpMenu,DecBrightnessButton,IncBrightnessButton,MoveDownButton,MoveUpButton,MoveLeftButton, MoveRightButton,ZoominButton,ZoomOutButton,StopMoveButton,StopZoomButton,SlowButton,FastButton,MediumButton,resetcamerabutton};

        foreach (var bu in bl)
        {
            bu.FlatAppearance.BorderColor = Color.Gray;
            bu.FlatAppearance.BorderSize = 1;
        }

        newPic.FlatAppearance.BorderColor = Color.Green;
        newPic.FlatAppearance.BorderSize = 8;

    }

    // Garbage Collector is not disposing Bitmap correctory..... idk why
    // forcing GC to dispose to save memory
    int GCtime = 0;
    Size targetSize = new Size(386, 216);
    void timer1_Tick(object sender, EventArgs e) {

        Image<Bgr, byte> image = null;
        using (Mat frame = capturedimage.QueryFrame())
        {
            if (frame != null)
            {
                // Resize the frame to the desired size
                Mat resizedFrame = new Mat();
                CvInvoke.Resize(frame, resizedFrame, targetSize);
                image?.Dispose();
                // Convert the resized frame to a Bitmap
                image = resizedFrame.ToImage<Bgr, byte>();

                if (livepicbox.Image != null) { livepicbox.Image.Dispose(); }


                // Display the image in the PictureBox
                livepicbox.Image?.Dispose(); // Dispose of the previous image if it exists
                livepicbox.Image = image.ToBitmap();
            }
            GCtime++;
            if (GCtime > 600)
            {
                GC.Collect(); // dont really want to do this. but i had too......
                GCtime = 0;
            }
        }

    }

    // start new mugshot process button
    private async void newPic_Click(object sender, EventArgs e)
    {

         camerastatlabel.ForeColor = Color.Red;
        camerastatlabel.Text = "Getting Ready...";
         camerastatlabel.Update();
        foreach (var b in bl)
        {
            b.Enabled = false;
        }

        // reset pictures and inmate identifier text box
        for (int i = 0; i < pblist.Count; i++)
        {
           
            pblist[i].Image = null;
            test1list[i].Visible = false;
            test2list[i].Visible = false;
            test3list[i].Visible = false;
            selist[i].Visible = false;
            selectedfront = 9;
            selectedside = 9;
        }
        for (int i = 0;i<4;i++)
        {
            FrontMugs[i]?.originalbm?.Dispose();
            FrontMugs[i]?.croppedbm?.Dispose();
            SideMugs[i]?.originalbm?.Dispose();
            SideMugs[i]?.croppedbm?.Dispose();
            FrontMugs[i] = null;
            SideMugs[i] = null;
        }

        GC.Collect(); // dont really want to do this. but i had too.....

        inmateinfotextbox.Text = String.Empty;
        if (Settings1.Default.controllerwork)
        {


            var b = await iszoomedin();
            if(b) {
                camerastatlabel.ForeColor = Color.Green;
                camerastatlabel.Text = "Ready!";
                buttoncolortogray();                                                
                CalibrateButton.FlatAppearance.BorderColor = Color.Green;
                CalibrateButton.FlatAppearance.BorderSize = 8;
            }

        }
        else
        {
            camerastatlabel.ForeColor = Color.Green;
            camerastatlabel.Text = "Ready!";
            buttoncolortogray();
            takeFrontPic.FlatAppearance.BorderColor = Color.Green;
            takeFrontPic.FlatAppearance.BorderSize = 8;
        }

        foreach (var b in bl)
        {
            b.Enabled = true;
        }
    }

    private async Task<bool> iszoomedin()
    {
        using (var sp = new System.IO.Ports.SerialPort(Settings1.Default.controllername, 9600, Parity.None, 8, StopBits.One))
        {
            sp.Open();
            sp.Write(new byte[] { 0x81, 0x01, 0x06, 0x04, 0xFF }, 0, 5);// reset the camera to the original position
            await Task.Delay(1000).ConfigureAwait(false);
            sp.Write(new byte[] { 0x81, 0x01, 0x04, 0x47, 0x00, 0x00, 0x00, 0x00, 0xFF }, 0, 9); // zoom out
            
            bool a = false;
            byte[] buffer = new byte[7];
            while (!a)
            {
              sp.Write(new byte[] { 0x81, 0x09, 0x04, 0x47, 0xFF }, 0, 5);
              sp.Read(buffer, 0, 7);
                if (buffer.Count(x=> x == 0x00) >= 4)
                {
                    a = true;
                }
                await Task.Delay(100).ConfigureAwait(false);
                Debug.WriteLine(buffer[0].ToString() + " - " + buffer[1].ToString() + " - " + buffer[2].ToString() + " - " + buffer[3].ToString() + " - " + buffer[4].ToString() + " - " + buffer[5].ToString() + " - " + buffer[6].ToString() );
            }
            sp.Close();
        }

        return true;

    }


    // reset all the button's UI
    private void buttoncolortogray()
    {
        newPic.FlatAppearance.BorderColor = Color.Gray;
        newPic.FlatAppearance.BorderSize = 1;
        savePics.FlatAppearance.BorderColor = Color.Gray;
        savePics.FlatAppearance.BorderSize = 1;
        CalibrateButton.FlatAppearance.BorderColor = Color.Gray;
        CalibrateButton.FlatAppearance.BorderSize = 1;
        takeFrontPic.FlatAppearance.BorderColor = Color.Gray;
        takeFrontPic.FlatAppearance.BorderSize = 1;
        takeSidePic.FlatAppearance.BorderColor = Color.Gray;
        takeSidePic.FlatAppearance.BorderSize = 1;
        DecBrightnessButton.FlatAppearance.BorderColor = Color.Gray;
        DecBrightnessButton.FlatAppearance.BorderSize = 1;
        IncBrightnessButton.FlatAppearance.BorderColor = Color.Gray;
        IncBrightnessButton.FlatAppearance.BorderSize = 1;
    }

    // combining mugshots -> ran if it is checked in the settings menu
    private Bitmap combinemugshots (Bitmap front, Bitmap side)
    {

        var combinedmugsize = new Size(Settings1.Default.combinedmugwidth, Settings1.Default.combinedmugheight);
        Bitmap result = new Bitmap(combinedmugsize.Width, combinedmugsize.Height);
        Bitmap newfront;
        Bitmap newside;

        if(front.Width/(combinedmugsize.Width/2f) > front.Height/(combinedmugsize.Height))
        {
            var sameaspectwidth = front.Width/(front.Height/((float)combinedmugsize.Height));
            var widthdiff = Math.Abs(sameaspectwidth - (combinedmugsize.Width / 2f));
            Rectangle newaspect = new Rectangle((int)(widthdiff / 2f), 0, (int)(combinedmugsize.Width / 2f), combinedmugsize.Height);

            front = new Bitmap(front, new Size((int)sameaspectwidth,combinedmugsize.Height));
             newfront = front.Clone(newaspect, front.PixelFormat);
                
            side = new Bitmap(side, new Size((int)sameaspectwidth, combinedmugsize.Height));
             newside = side.Clone(newaspect, side.PixelFormat);
        }
        else
        {

            var sameaspectheight = front.Height / (front.Width / (combinedmugsize.Width / 2f));
            var heightdiff = Math.Abs(sameaspectheight - (combinedmugsize.Height / 2f));
            Rectangle newaspect = new Rectangle(0, (int)(heightdiff / 2f), combinedmugsize.Width, combinedmugsize.Height);

            front = new Bitmap(front, new Size(combinedmugsize.Width, (int)sameaspectheight));
             newfront = front.Clone(newaspect, front.PixelFormat);

            side = new Bitmap(side, new Size(combinedmugsize.Width, (int)sameaspectheight));
             newside = side.Clone(newaspect, side.PixelFormat);
        }


        using(Graphics g = Graphics.FromImage(result))
        {
            g.DrawImage(newfront, Point.Empty);
            g.DrawImage(newside, combinedmugsize.Width/2, 0);
        }



        return result;
    }
    //save mugshots button
    private void savePics_Click(object sender, EventArgs e)
    {

        if (selectedfront != 9 && selectedside != 9) // if mugshots are selected
        {
            if (FrontMugs[selectedfront] is not null && SideMugs[selectedside-4] is not null)
            {
                if (Settings1.Default.overridepic == true) // save  ( override mugshots)
                {
                    FrontMugs[selectedfront].croppedbm.Save(@"" + Settings1.Default.filepathforpic + "\\Front_Mugshot.jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    SideMugs[selectedside - 4].croppedbm.Save(@"" + Settings1.Default.filepathforpic + "\\Side_Mugshot.jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    if(Settings1.Default.combinemugshots == true)
                    {
                        combinemugshots(FrontMugs[selectedfront].croppedbm, SideMugs[selectedside - 4].croppedbm).Save(@"" + Settings1.Default.filepathforpic + "\\xCombined_Mugshot.jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    }


                    if (Settings1.Default.openfolder == true)
                        Process.Start("explorer.exe", @"" + Settings1.Default.filepathforpic);
                    ErrorPrompt.ShowErrorMessage("Mugshots Saved!");
                }
                else // save (not override mugshots
                {
                    if (Settings1.Default.filename == 0)
                    {
                        string inmateinfo = inmateinfotextbox.Text.Trim().Replace(" ", "_");
                        if (inmateinfo == string.Empty || inmateinfo == "") // check and make sure that inmate identifier is typed
                        {
                            string promptValue = ErrorPrompt.ShowErrorMessage("Type Inmate's identifier such as inmate number or name before saving!");
                        }
                        else
                        {
                            FrontMugs[selectedfront].croppedbm.Save(@"" + Settings1.Default.filepathforpic + "\\" + DateTime.Now.ToString("yyyy_MMdd_HHmm_") + inmateinfo + "_Front.jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
                            SideMugs[selectedside - 4].croppedbm.Save(@"" + Settings1.Default.filepathforpic + "\\" + DateTime.Now.ToString("yyyy_MMdd_HHmm_") + inmateinfo + "_Side.jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
                            if (Settings1.Default.combinemugshots == true)
                            {
                                combinemugshots(FrontMugs[selectedfront].croppedbm, SideMugs[selectedside - 4].croppedbm).Save(@"" + Settings1.Default.filepathforpic + "\\" + DateTime.Now.ToString("yyyy_MMdd_HHmm_") + inmateinfo + "_xCombined.jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
                            }

                            if (Settings1.Default.openfolder == true)
                                Process.Start("explorer.exe", @"" + Settings1.Default.filepathforpic);
                            ErrorPrompt.ShowErrorMessage("Mugshots Saved!");
                        }
                    }
                    else
                    {
                        FrontMugs[selectedfront].croppedbm.Save(@"" + Settings1.Default.filepathforpic + "\\" + DateTime.Now.ToString("yyyy_MMdd_HHmm") + "_Front.jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
                        SideMugs[selectedside - 4].croppedbm.Save(@"" + Settings1.Default.filepathforpic + "\\" + DateTime.Now.ToString("yyyy_MMdd_HHmm") + "_Side.jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
                        if (Settings1.Default.combinemugshots == true)
                        {
                            combinemugshots(FrontMugs[selectedfront].croppedbm, SideMugs[selectedside - 4].croppedbm).Save(@"" + Settings1.Default.filepathforpic  +"\\" + DateTime.Now.ToString("yyyy_MMdd_HHmm") + "_xCombined.jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
                        }

                        if (Settings1.Default.openfolder == true)
                            Process.Start("explorer.exe", @"" + Settings1.Default.filepathforpic);
                        ErrorPrompt.ShowErrorMessage("Mugshots Saved!");

                    }

                }
                buttoncolortogray();
                newPic.FlatAppearance.BorderColor = Color.Green;
                newPic.FlatAppearance.BorderSize = 8;
            }
            else
            {
                string promptValue = ErrorPrompt.ShowErrorMessage("Take mugshots before saving!");
            }


        }
        else
        {
            string promptValue = ErrorPrompt.ShowErrorMessage("Select mugshots by clicking the images before saving!");
        }

    }

    // settings button
    private void settingsMenu_Click(object sender, EventArgs e)
    {// openes settings manu

        int tempcam = Settings1.Default.cameraindex;
        /// show saving icon while saving all the settings
        var settingsform = new saveSettings();
        settingsform.ShowDialog(this);


        loadingscreen.ShowSplashScreen("saving...");
        if (tempcam != Settings1.Default.cameraindex)
            capturedimage = new VideoCapture(Settings1.Default.cameraindex);

        if (Settings1.Default.overridepic == false && Settings1.Default.filename == 0)
        {
            inmateinfotextbox.Enabled = true;
            inmateinfortext.Enabled = true;
        }
        else
        {
            inmateinfotextbox.Enabled = false;
            inmateinfortext.Enabled = false;
        }

        try
        {// if controller does not work,gray out the buttons for camera controll
            using (var sp = new System.IO.Ports.SerialPort(Settings1.Default.controllername, 9600, Parity.None, 8, StopBits.One))
            {
                sp.Open();
                sp.Write(new byte[] { 0x81, 0x01, 0x04, 0x47, 0x00, 0x00, 0x00, 0x00, 0xFF }, 0, 9); // zoom out
                sp.Write(new byte[] { 0x81, 0x01, 0x04, 0x39, 0x00, 0xFF }, 0, 6); // change CAM_AE to Full Auto
                Thread.Sleep(200);
                sp.Write(new byte[] { 0x81, 0x01, 0x04, 0x39, 0x0D, 0xFF }, 0, 6); // change CAM_AE to Bright
                sp.Close();
            }
            //  brightnessMenu.Enabled = true;
            Settings1.Default.controllerwork = true;
            Settings1.Default.Save();

        }
        catch (Exception ex)
        {
            Settings1.Default.controllerwork = false;
            Settings1.Default.Save();
        }
        controllerbuttonchange(Settings1.Default.controllerwork);
        Thread.Sleep(500);
        loadingscreen.CloseForm();

    }

    // help button
    private void helpMenu_Click(object sender, EventArgs e)
    {
        //open help window
        var helpform = new HelpMenu();
        helpform.ShowDialog(this);
    }

    // take side pictues button
    private void takeSidePic_Click(object sender, EventArgs e)
    {
        //take picture of the side
        //take four
        foreach (var b in bl)
        {
            b.Enabled = false;
        }
        bool istheregoodpicture = false;
        camerastatlabel.ForeColor = Color.Red;
        camerastatlabel.Text = "Taking mugshots...";
        camerastatlabel.Update();

        for (int i = 0; i < 4; i++)
        {
            SideMugs[i] = new SideMugshot(capturedimage.QueryFrame().ToBitmap()); //create sidemugshot class object
            if (SideMugs[i].isGoodMugshot == false || SideMugs[i] == null) // error handling
            {

                pblist[i + 4].Image = System.Drawing.Image.FromFile(@".\errorface.jpg");
                test1list[i + 4].Visible = false;
                test2list[i + 4].Visible = false;
                test3list[i + 4].Visible = false;
            }
            else
            {

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
                istheregoodpicture = true;
            }
            pblist[i + 4].Update();
            Thread.Sleep(200); // wait 500 millisecond for the next mugshot

        }
        //   }
        //  else { string promptValue = ErrorPrompt.ShowErrorMessage("Face not detected or subject moving around too much"); }
        camerastatlabel.ForeColor = Color.Green;
        camerastatlabel.Text = "Ready!";
        if (istheregoodpicture)
        {
            buttoncolortogray();
            savePics.FlatAppearance.BorderColor = Color.Green;
            savePics.FlatAppearance.BorderSize = 8;
        }
        foreach (var b in bl)
        {
            b.Enabled = true;
        }
    }

    //take front pictures button
    private void takeFrontPic_Click(object sender, EventArgs e)
    {
        foreach (var b in bl)
        {
            b.Enabled = false;
        }
        //take picture of the front
        //take four
        bool istheregoodpicture = false;

        camerastatlabel.ForeColor = Color.Red;
        camerastatlabel.Text = "Taking mugshots...";
        camerastatlabel.Update();
        for (int i = 0; i < 4; i++)
        {
            FrontMugs[i] = new FrontMugshot(capturedimage.QueryFrame().ToBitmap()); // create new front mugshot class object
            if (FrontMugs[i].isGoodMugshot == false || FrontMugs[i] == null) // error handling
            {

                pblist[i].Image = System.Drawing.Image.FromFile(@".\errorface.jpg");
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
                istheregoodpicture = true;
            }
            pblist[i].Update();
            Thread.Sleep(200);
        }

        camerastatlabel.ForeColor = Color.Green;
        camerastatlabel.Text = "Ready!";
        if (istheregoodpicture)
        {
            buttoncolortogray();
            takeSidePic.FlatAppearance.BorderColor = Color.Green;
            takeSidePic.FlatAppearance.BorderSize = 8;
        }
        foreach (var b in bl)
        {
            b.Enabled = true;
        }

    }

    // selecting mugshots from here
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
        if (FrontMugs[index] is not null && test1list[index].Visible == true)
        {
            if (selectedfront != 9)
            {
                selist[selectedfront].Visible = false;
            }
            selectedfront = index;
            selist[selectedfront].Visible = true;
        }


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
        if (SideMugs[index-4] is not null )
        {
            if (selectedside != 9)
            {
                selist[selectedside].Visible = false;
            }
            selectedside = index;
            selist[selectedside].Visible = true;
        }


    }

    // selecting mugshot until here


    // main menu loading method
    System.Windows.Forms.Timer timer1;
    private void mainMenu_Load(object sender, EventArgs e)
    {
        this.WindowState = FormWindowState.Minimized;
        this.WindowState = FormWindowState.Normal;
        this.Focus(); this.Show();
        // for the camera monitor on the screen
        timer1 = new System.Windows.Forms.Timer();
        timer1.Interval = 50;
       timer1.Tick += timer1_Tick;
        timer1.Start();
        newPic.Focus();
    }


    // get the center face image 
    public int getinmateface(float[,] facelist, int piccenter)
    {
        if (facelist.GetLength(0) == 1) return 0;
        int closest = 0;
        float difffromcenter;
        float diff = piccenter * 2;
        for(int i = 0;i< facelist.GetLength(0);i++)
        {
            difffromcenter = Math.Abs(piccenter - (facelist[i, 0] + (facelist[i, 2] / 2)));
            if (diff > difffromcenter )
            {
                closest = i;
                diff = difffromcenter;
            }
        }

        return closest;
    }

    // move, and zoom so that our inmate face is in the middle and zoomed in.
    public async Task CalibrateCamera()
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
        int faceint = 0;
        using (var sp = new System.IO.Ports.SerialPort(Settings1.Default.controllername, 9600, Parity.None, 8, StopBits.One))
        {
            sp.Open();
 
            //  while (!inmiddle)
            //  {// move left and right
            bm = capturedimage.QueryFrame().ToBitmap();

            mugshotface = bm.ToMat();
            using var model = InitializeFaceDetectionModel(new Size(mugshotface.Width, mugshotface.Height));
            model.Detect(mugshotface, facefeatures);

            var facedata = (float[,])facefeatures.GetData(jagged: true);

            if (facedata != null)
            {
                faceint = getinmateface(facedata,mugshotface.Width/2);
                face = new float[] { facedata[faceint, 0], facedata[faceint, 1], facedata[faceint, 2], facedata[faceint, 3] };
                if ((mugshotface.Width / 2) < face[0] + (face[2] / 2f))
                {
                    float tempfacemiddle = face[0] + (face[2] / 2f);
                    // if (rpp != 0 && rpp != 1) inmiddle = true;
                    sp.Write(new byte[] { 0x81, 0x01, 0x06, 0x03, 0x18, 0x18, 0x00, 0x00, 0x04, 0x08, 0x00, 0x00, 0x00, 0x00, 0xFF }, 0, 15);
                    await Task.Delay(500).ConfigureAwait(false);

                    bm = capturedimage.QueryFrame().ToBitmap();
                    mugshotface = bm.ToMat();
                    using var model2 = InitializeFaceDetectionModel(new Size(mugshotface.Width, mugshotface.Height));
                    model2.Detect(mugshotface, facefeatures);
                    facedata = (float[,])facefeatures.GetData(jagged: true);
                    faceint = getinmateface(facedata, mugshotface.Width / 2);
                    face = new float[] { facedata[faceint, 0], facedata[faceint, 1], facedata[faceint, 2], facedata[faceint, 3] };
                    float diffx = (((face[0] + (face[2] / 2f)) - (mugshotface.Width / 2f)) / ((tempfacemiddle - (face[0] + (face[2] / 2f))) / 5f) * 12.4f);
                    Debug.WriteLine(diffx.ToString());
                    var bytelist = getbfromi((short)diffx);
                    sp.Write(new byte[] { 0x81, 0x01, 0x06, 0x03, 0x18, 0x18, bytelist[4], bytelist[5], bytelist[6], bytelist[7], 0x00, 0x00, 0x00, 0x00, 0xFF }, 0, 15);
                }
                else
                {
                    float tempfacemiddle = face[0] + (face[2] / 2);

                    sp.Write(new byte[] { 0x81, 0x01, 0x06, 0x03, 0x18, 0x18, 0x0F, 0x0F, 0x0B, 0x08, 0x00, 0x00, 0x00, 0x00, 0xFF }, 0, 15);
                    await Task.Delay(500).ConfigureAwait(false);
                    bm = capturedimage.QueryFrame().ToBitmap();
                    mugshotface = bm.ToMat();
                    using var model2 = InitializeFaceDetectionModel(new Size(mugshotface.Width, mugshotface.Height));
                    model2.Detect(mugshotface, facefeatures);
                    facedata = (float[,])facefeatures.GetData(jagged: true);
                    faceint = getinmateface(facedata, mugshotface.Width / 2);
                    face = new float[] { facedata[faceint, 0], facedata[faceint, 1], facedata[faceint, 2], facedata[faceint, 3] };
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
            await Task.Delay(500).ConfigureAwait(false);



            // move up and down
            mugshotface = bm.ToMat();
            using var model3 = InitializeFaceDetectionModel(new Size(mugshotface.Width, mugshotface.Height));
            model3.Detect(mugshotface, facefeatures);

            facedata = (float[,])facefeatures.GetData(jagged: true);

            if (facedata != null)
            {
                faceint = getinmateface(facedata, mugshotface.Width / 2);
                face = new float[] { facedata[faceint, 0], facedata[faceint, 1], facedata[faceint, 2], facedata[faceint, 3] };
                if ((mugshotface.Height / 2f) > face[1] + ((face[3])*0.7f))
                {
                    float tempfacemiddle = face[1] + ((face[3]) * 0.7f);
                    // if (rpp != 0 && rpp != 1) inmiddle = true;
                    sp.Write(new byte[] { 0x81, 0x01, 0x06, 0x03, 0x14, 0x14, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x04, 0x08, 0xFF }, 0, 15);
                    await Task.Delay(500).ConfigureAwait(false);
                    bm = capturedimage.QueryFrame().ToBitmap();
                    mugshotface = bm.ToMat();
                    using var model4 = InitializeFaceDetectionModel(new Size(mugshotface.Width, mugshotface.Height));
                    model4.Detect(mugshotface, facefeatures);
                    facedata = (float[,])facefeatures.GetData(jagged: true);
                    faceint = getinmateface(facedata, mugshotface.Width / 2);
                    face = new float[] { facedata[faceint, 0], facedata[faceint, 1], facedata[faceint, 2], facedata[faceint, 3] };
                    float diffx = ((((mugshotface.Height / 2f) - (face[1] + ((face[3]) * 0.7f))) / (((face[1] + ((face[3]) * 0.7f)) - tempfacemiddle) / 5f)) * 12.4f);
                    Debug.WriteLine(diffx.ToString());
                    var bytelist = getbfromi((short)diffx);
                    sp.Write(new byte[] { 0x81, 0x01, 0x06, 0x03, 0x14, 0x14, 0x00, 0x00, 0x00, 0x00, bytelist[4], bytelist[5], bytelist[6], bytelist[7], 0xFF }, 0, 15);
                }
                else
                {
                    float tempfacemiddle = face[1] + ((face[3]) * 0.7f);

                    sp.Write(new byte[] { 0x81, 0x01, 0x06, 0x03, 0x14, 0x14, 0x00, 0x00, 0x00, 0x00, 0x0F, 0x0F, 0x0B, 0x08, 0xFF }, 0, 15);
                    await Task.Delay(500).ConfigureAwait(false);
                    bm = capturedimage.QueryFrame().ToBitmap();
                    mugshotface = bm.ToMat();
                    using var model4 = InitializeFaceDetectionModel(new Size(mugshotface.Width, mugshotface.Height));
                    model4.Detect(mugshotface, facefeatures);
                    facedata = (float[,])facefeatures.GetData(jagged: true);
                    faceint = getinmateface(facedata, mugshotface.Width / 2);
                    face = new float[] { facedata[faceint, 0], facedata[faceint, 1], facedata[faceint, 2], facedata[faceint, 3] };
                    float diffx = ((face[1] + ((face[3]) * 0.7f)) - (mugshotface.Height / 2f)) / ((tempfacemiddle - (face[1] + ((face[3]) * 0.7f))) / 5f);
                    Debug.WriteLine(diffx.ToString());
                    var bytelist = getbfromi((short)(65535 - (diffx * 12.4f)));
                    sp.Write(new byte[] { 0x81, 0x01, 0x06, 0x03, 0x14, 0x14, 0x00, 0x00, 0x00, 0x00, bytelist[4], bytelist[5], bytelist[6], bytelist[7], 0xFF }, 0, 15);
                }

            }
            else
            {
                //return false;
            }
            await Task.Delay(500).ConfigureAwait(false);


            // zoom in and out
            int zv = 1;
            bool isfirst = true;
            bool zoomright = false;
            short zoomvalue = 2000;
            while (!zoomright)
            {// zoom in
                bm = capturedimage.QueryFrame().ToBitmap();

                mugshotface = bm.ToMat();
                using var model5 = InitializeFaceDetectionModel(new Size(mugshotface.Width, mugshotface.Height));
                model.Detect(mugshotface, facefeatures);

                facedata = (float[,])facefeatures.GetData(jagged: true);

                if (facedata != null)
                {
                    faceint = getinmateface(facedata, mugshotface.Width / 2);
                    face = new float[] { facedata[faceint, 0], facedata[faceint, 1], facedata[faceint, 2], facedata[faceint, 3] };
                    if ((mugshotface.Height) > face[3] * 2)
                    {
                        sp.Write(getbfromi((short)(zoomvalue * zv)), 0, 9);
                        isfirst = false;
                    }
                    else
                    {
                        
                        zoomright = true;
                        if(!isfirst)
                            sp.Write(getbfromi((short)((float)zoomvalue * 0.8f * (zv-2))), 0, 9);
                    }
                }
                else
                {
                    zoomright = true;
                    if (!isfirst)
                        sp.Write(getbfromi((short)((float)zoomvalue * 0.8f * (zv - 2))), 0, 9);
                }
                zv++;
                zoomvalue = (short)((float)zoomvalue / 0.99f);
                await Task.Delay(300).ConfigureAwait(false);
 
                if (zv > 9) zoomright = true;
            }
            
            sp.Close();
        }
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

    // to show message 
    public static class ErrorPrompt
    {// error or message prompt
        public static string ShowErrorMessage(string text)
        {
            Form prompt = new Form()
            {
                
                Width = (text.Length * 15) ,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "",
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label() { Left = 50, Top = 20, Width = text.Length * 10, Text = text };
            textLabel.Font = new System.Drawing.Font("Stencil", 12);
            System.Windows.Forms.Button confirmation = new System.Windows.Forms.Button() { Text = "Ok", Left = 50, Width = 100, Top = 50, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;
            return prompt.ShowDialog() == DialogResult.OK ? "" : "";

        }
    }

    //calibrate camera button
    async private void CalibrateButton_Click(object sender, EventArgs e)
    {
        foreach (var b in bl)
        {
            b.Enabled = false;
        }
        camerastatlabel.ForeColor = Color.Red;
        camerastatlabel.Text = "Calibrating...";
        await CalibrateCamera();
        await CalibrateCamera();
        camerastatlabel.ForeColor = Color.Green;
        camerastatlabel.Text = "Ready!";
        buttoncolortogray();
        DecBrightnessButton.FlatAppearance.BorderColor = Color.Green;
        DecBrightnessButton.FlatAppearance.BorderSize = 8;
        IncBrightnessButton.FlatAppearance.BorderColor = Color.Green;
        IncBrightnessButton.FlatAppearance.BorderSize = 8;
        foreach (var b in bl)
        {
            b.Enabled = true;
        }
    }
    // reset camera button
    private void resetcamerabutton_Click(object sender, EventArgs e)
    {
        camerastatlabel.ForeColor = Color.Red;
        camerastatlabel.Text = "Resetting Camera...";
        using (var sp = new System.IO.Ports.SerialPort(Settings1.Default.controllername, 9600, Parity.None, 8, StopBits.One))
        {
            sp.Open();
            sp.Write(new byte[] { 0x81, 0x01, 0x04, 0x39, 0x00, 0xFF }, 0, 6);
            Thread.Sleep(300);
            sp.Write(new byte[] { 0x81, 0x01, 0x04, 0x39, 0x0D, 0xFF }, 0, 6);
            sp.Write(new byte[] { 0x81, 0x01, 0x04, 0x47, 0x00, 0x00, 0x00, 0x00, 0xFF }, 0, 9);
            livepicbox.Update();
            Thread.Sleep(1000);
            sp.Write(new byte[] { 0x81, 0x01, 0x06, 0x04, 0xFF }, 0, 5);
            livepicbox.Update();
            sp.Close();
        }
        camerastatlabel.ForeColor = Color.Green;
        camerastatlabel.Text = "Ready!";
    }

    //increase brightness button
    private void IncBrightnessButton_Click(object sender, EventArgs e)
    {
        using (var sp = new System.IO.Ports.SerialPort(Settings1.Default.controllername, 9600, Parity.None, 8, StopBits.One))
        {
            sp.Open();
            sp.Write(new byte[] { 0x81, 0x01, 0x04, 0x0D, 0x02, 0xFF }, 0, 6);
            sp.Close();
        }
        if (!(DecBrightnessButton.FlatAppearance.BorderColor == Color.Gray))
        {
            buttoncolortogray();
            takeFrontPic.FlatAppearance.BorderColor = Color.Green;
            takeFrontPic.FlatAppearance.BorderSize = 8;
        }

    }

    // decrease brightness button
    private void DecBrightnessButton_Click(object sender, EventArgs e)
    {
        using (var sp = new System.IO.Ports.SerialPort(Settings1.Default.controllername, 9600, Parity.None, 8, StopBits.One))
        {
            sp.Open();
            sp.Write(new byte[] { 0x81, 0x01, 0x04, 0x0D, 0x03, 0xFF }, 0, 6);
            sp.Close();
        }
        if (!(DecBrightnessButton.FlatAppearance.BorderColor == Color.Gray))
        {
            buttoncolortogray();
            takeFrontPic.FlatAppearance.BorderColor = Color.Green;
            takeFrontPic.FlatAppearance.BorderSize = 8;
        }
    }


    // canera controll buttons from here
    private void ZoominButton_Click(object sender, EventArgs e)
    {
        camerastatlabel.ForeColor = Color.Red;
        camerastatlabel.Text = "Zooming In...";
        using (var sp = new System.IO.Ports.SerialPort(Settings1.Default.controllername, 9600, Parity.None, 8, StopBits.One))
        {
            sp.Open();
            switch (Settings1.Default.cameraspeed)
            {
                case 0:
                    sp.Write(new byte[] { 0x81, 0x01, 0x04, 0x07, 0x21, 0xFF }, 0, 6);
                    break;
                case 1:
                    sp.Write(new byte[] { 0x81, 0x01, 0x04, 0x07, 0x23, 0xFF }, 0, 6);
                    break;
                case 2:
                    sp.Write(new byte[] { 0x81, 0x01, 0x04, 0x07, 0x25, 0xFF }, 0, 6);
                    break;
            }
            sp.Close();
        }
    }

    private void ZoomOutButton_Click(object sender, EventArgs e)
    {
        camerastatlabel.ForeColor = Color.Red;
        camerastatlabel.Text = "Zooming Out...";
        using (var sp = new System.IO.Ports.SerialPort(Settings1.Default.controllername, 9600, Parity.None, 8, StopBits.One))
        {
            sp.Open();
            switch (Settings1.Default.cameraspeed)
            {
                case 0:
                    sp.Write(new byte[] { 0x81, 0x01, 0x04, 0x07, 0x31, 0xFF }, 0, 6);
                    break;
                case 1:
                    sp.Write(new byte[] { 0x81, 0x01, 0x04, 0x07, 0x33, 0xFF }, 0, 6);
                    break;
                case 2:
                    sp.Write(new byte[] { 0x81, 0x01, 0x04, 0x07, 0x35, 0xFF }, 0, 6);
                    break;
            }
            sp.Close();
        }
    }

    private void MoveLeftButton_Click(object sender, EventArgs e)
    {
        camerastatlabel.ForeColor = Color.Red;
        camerastatlabel.Text = "Camera Moving...";
        using (var sp = new System.IO.Ports.SerialPort(Settings1.Default.controllername, 9600, Parity.None, 8, StopBits.One))
        {
            sp.Open();
            switch (Settings1.Default.cameraspeed)
            {
                case 0:
                    sp.Write(new byte[] { 0x81, 0x01, 0x06, 0x01, 0x01, 0x01, 0x01, 0x03, 0xFF }, 0, 9);
                    break;
                case 1:
                    sp.Write(new byte[] { 0x81, 0x01, 0x06, 0x01, 0x07, 0x07, 0x01, 0x03, 0xFF }, 0, 9);
                    break;
                case 2:
                    sp.Write(new byte[] { 0x81, 0x01, 0x06, 0x01, 0x09, 0x09, 0x01, 0x03, 0xFF }, 0, 9);
                    break;
            }
            sp.Close();
        }
    }

    private void MoveUpButton_Click(object sender, EventArgs e)
    {
        camerastatlabel.ForeColor = Color.Red;
        camerastatlabel.Text = "Camera Moving...";
        using (var sp = new System.IO.Ports.SerialPort(Settings1.Default.controllername, 9600, Parity.None, 8, StopBits.One))
        {
            sp.Open();
            switch (Settings1.Default.cameraspeed)
            {
                case 0:
                    sp.Write(new byte[] { 0x81, 0x01, 0x06, 0x01, 0x01, 0x01, 0x03, 0x01, 0xFF }, 0, 9);
                    break;
                case 1:
                    sp.Write(new byte[] { 0x81, 0x01, 0x06, 0x01, 0x07, 0x07, 0x03, 0x01, 0xFF }, 0, 9);
                    break;
                case 2:
                    sp.Write(new byte[] { 0x81, 0x01, 0x06, 0x01, 0x09, 0x09, 0x03, 0x01, 0xFF }, 0, 9);
                    break;
            }
            sp.Close();
        }
    }

    private void MoveRightButton_Click(object sender, EventArgs e)
    {
        camerastatlabel.ForeColor = Color.Red;
        camerastatlabel.Text = "Camera Moving...";
        using (var sp = new System.IO.Ports.SerialPort(Settings1.Default.controllername, 9600, Parity.None, 8, StopBits.One))
        {
            sp.Open();
            switch (Settings1.Default.cameraspeed)
            {
                case 0:
                    sp.Write(new byte[] { 0x81, 0x01, 0x06, 0x01, 0x01, 0x01, 0x02, 0x03, 0xFF }, 0, 9);
                    break;
                case 1:
                    sp.Write(new byte[] { 0x81, 0x01, 0x06, 0x01, 0x07, 0x07, 0x02, 0x03, 0xFF }, 0, 9);
                    break;
                case 2:
                    sp.Write(new byte[] { 0x81, 0x01, 0x06, 0x01, 0x09, 0x09, 0x02, 0x03, 0xFF }, 0, 9);
                    break;
            }
            sp.Close();
        }
    }

    private void MoveDownButton_Click(object sender, EventArgs e)
    {
        camerastatlabel.ForeColor = Color.Red;
        camerastatlabel.Text = "Camera Moving...";
        using (var sp = new System.IO.Ports.SerialPort(Settings1.Default.controllername, 9600, Parity.None, 8, StopBits.One))
        {
            sp.Open();
            switch (Settings1.Default.cameraspeed)
            {
                case 0:
                    sp.Write(new byte[] { 0x81, 0x01, 0x06, 0x01, 0x01, 0x01, 0x03, 0x02, 0xFF }, 0, 9);
                    break;
                case 1:
                    sp.Write(new byte[] { 0x81, 0x01, 0x06, 0x01, 0x07, 0x07, 0x03, 0x02, 0xFF }, 0, 9);
                    break;
                case 2:
                    sp.Write(new byte[] { 0x81, 0x01, 0x06, 0x01, 0x09, 0x09, 0x03, 0x02, 0xFF }, 0, 9);
                    break;
            }
            sp.Close();
        }
    }

    private void StopZoomButton_Click(object sender, EventArgs e)
    {
        using (var sp = new System.IO.Ports.SerialPort(Settings1.Default.controllername, 9600, Parity.None, 8, StopBits.One))
        {
            sp.Open();
            sp.Write(new byte[] { 0x81, 0x01, 0x04, 0x07, 0x00, 0xFF }, 0, 6);
            sp.Close();
        }
        camerastatlabel.ForeColor = Color.Green;
        camerastatlabel.Text = "Ready!";
    }

    private void StopMoveButton_Click(object sender, EventArgs e)
    {
        using (var sp = new System.IO.Ports.SerialPort(Settings1.Default.controllername, 9600, Parity.None, 8, StopBits.One))
        {
            sp.Open();
            sp.Write(new byte[] { 0x81, 0x01, 0x06, 0x01, 0x014, 0x14, 0x03, 0x03, 0xFF }, 0, 9);
            sp.Close();
        }
        camerastatlabel.ForeColor = Color.Green;
        camerastatlabel.Text = "Ready!";
    }

    private void SlowButton_Click(object sender, EventArgs e)
    {
        Settings1.Default.cameraspeed = 0;
        Settings1.Default.Save();
        SlowButton.ForeColor = Color.Red;
        MediumButton.ForeColor = Color.Black;
        FastButton.ForeColor = Color.Black;
 
    }

    private void MediumButton_Click(object sender, EventArgs e)
    {
        Settings1.Default.cameraspeed = 1;
        Settings1.Default.Save();
        SlowButton.ForeColor = Color.Black;
        MediumButton.ForeColor = Color.Red;
        FastButton.ForeColor = Color.Black;
    }

    private void FastButton_Click(object sender, EventArgs e)
    {
        Settings1.Default.cameraspeed = 2;
        Settings1.Default.Save();
        SlowButton.ForeColor = Color.Black;
        MediumButton.ForeColor = Color.Black;
        FastButton.ForeColor = Color.Red;
    }
    // canera controll buttons until here

    private void controllerbuttonchange(bool boo)
    {
        CalibrateButton.Enabled = boo;
        DecBrightnessButton.Enabled = boo;
        IncBrightnessButton.Enabled = boo;
        ZoominButton.Enabled = boo;
        ZoomOutButton.Enabled = boo;
        MoveDownButton.Enabled = boo;
        MoveUpButton.Enabled = boo;
        MoveLeftButton.Enabled = boo;
        MoveRightButton.Enabled = boo;
        resetcamerabutton.Enabled = boo;
        StopMoveButton.Enabled = boo;
        StopZoomButton.Enabled = boo;
        SlowButton.Enabled = boo;
        MediumButton.Enabled = boo;
        FastButton.Enabled = boo;
    }
}
