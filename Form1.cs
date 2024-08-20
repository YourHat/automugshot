using Emgu.CV;
using Emgu.CV.Dnn;
using Emgu.CV.Face;
using Emgu.CV.Flann;
using Emgu.CV.Structure;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO.Ports;
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
    List<PictureBox> pblist = new List<PictureBox>(); //list of picture box for mugshots
    List<Label> selist = new List<Label>(); // list of lables for "selected"

    List<Label> test1list = new List<Label>(); // list of labels for eyes
    List<Label> test2list = new List<Label>(); // list of labels for head tiltle / facing side
    List<Label> test3list = new List<Label>(); // list of labels for facing front and left side

    public VideoCapture capturedimage;

    public mainMenu()
    {
        InitializeComponent();
        FrontMugs = new FrontMugshot[4];
        SideMugs = new SideMugshot[4];

        // add picture boxes and labels to the list created
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

        capturedimage = new VideoCapture(Settings1.Default.cameraindex); // initiate videocapture

        try // check and see if controller for camera is connected and selected
        {
            using (var sp = new System.IO.Ports.SerialPort(Settings1.Default.controllername, 9600, Parity.None, 8, StopBits.One))
            {
                sp.Open();
                sp.Close();
            }
            brightnessMenu.Enabled = true;
        }
        catch (Exception ex)
        {
            brightnessMenu.Enabled = false;
        }

        if (Settings1.Default.overridepic == true) // check and see if override setting is true or false
        {
            inmateinfotextbox.Enabled = false;
            inmateinfortext.Enabled = false;
        }


    }


    private void newPic_Click(object sender, EventArgs e)
    {
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
        inmateinfotextbox.Text = String.Empty;
    }

    private void savePics_Click(object sender, EventArgs e)
    {
        string inmateinfo = inmateinfotextbox.Text.Trim().Replace(" ","_");
        if (selectedfront != 9 && selectedside != 9) // if mugshots are selected
        {
            if (Settings1.Default.overridepic == true) // save  ( override mugshots)
            {
                FrontMugs[selectedfront].croppedbm.Save(@"" + Settings1.Default.filepathforpic + "\\mugshotfront.jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
                SideMugs[selectedside-4].croppedbm.Save(@"" + Settings1.Default.filepathforpic + "\\mugshotside.jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
                if (Settings1.Default.openfolder == true)
                    Process.Start("explorer.exe", @"" + Settings1.Default.filepathforpic);
                ErrorPrompt.ShowErrorMessage("Mugshots Saved!");
            }
            else // save (not override mugshots
            {
                if(inmateinfo == string.Empty || inmateinfo == "") // check and make sure that inmate identifier is typed
                {
                    string promptValue = ErrorPrompt.ShowErrorMessage("Type Inmate's information such as inmate number or name");
                }
                else
                {
                    FrontMugs[selectedfront].croppedbm.Save(@"" + Settings1.Default.filepathforpic + "\\" + inmateinfo + "_front_" + DateTime.Now.ToString("_MM_dd_yyyy") + ".jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    SideMugs[selectedside - 4].croppedbm.Save(@"" + Settings1.Default.filepathforpic + "\\" + inmateinfo + "_side_" + DateTime.Now.ToString("_MM_dd_yyyy") + ".jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    if (Settings1.Default.openfolder == true)
                        Process.Start("explorer.exe", @"" + Settings1.Default.filepathforpic);
                    ErrorPrompt.ShowErrorMessage("Mugshots Saved!");
                }

            }

        }
        else
        {
            string promptValue = ErrorPrompt.ShowErrorMessage("Take mugshots and select mugshots to save");
        }
        
    }

    private void settingsMenu_Click(object sender, EventArgs e)
    {
        
        int tempcam = Settings1.Default.cameraindex;
        var settingsform = new saveSettings();
        settingsform.ShowDialog(this);


        loadingscreen.ShowSplashScreen("saving...");
        if(tempcam != Settings1.Default.cameraindex) 
            capturedimage = new VideoCapture(Settings1.Default.cameraindex);

        if (Settings1.Default.overridepic == true)
        {
            inmateinfotextbox.Enabled = false;
            inmateinfortext.Enabled = false;
        }
        else
        {
            inmateinfotextbox.Enabled = true;
            inmateinfortext.Enabled = true;
        }

        try
        {
            using (var sp = new System.IO.Ports.SerialPort(Settings1.Default.controllername, 9600, Parity.None, 8, StopBits.One))
            {
                sp.Open();
                sp.Write(new byte[] { 0x81, 0x01, 0x04, 0x47, 0x00, 0x00, 0x00, 0x00, 0xFF }, 0, 9);
                sp.Close();
            }
            brightnessMenu.Enabled = true;

        }
        catch (Exception ex)
        {
            brightnessMenu.Enabled = false;
        }
        loadingscreen.CloseForm();

    }

    private void helpMenu_Click(object sender, EventArgs e)
    {
        //open help window
        var helpform = new HelpMenu();
        helpform.ShowDialog(this);
    }

    private void takeSidePic_Click(object sender, EventArgs e)
    {
        //take picture of the side
        //take four
         if (brightnessMenu.Enabled == true?CalibrationCamera():true)
        {
            for (int i = 0; i < 4; i++)
            {
                SideMugs[i] = new SideMugshot(capturedimage.QueryFrame().ToBitmap());
                if (SideMugs[i].isGoodMugshot == false || SideMugs[i] == null)
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
                }
                pblist[i + 4].Update();
                Thread.Sleep(500);

            }
        }
        else { string promptValue = ErrorPrompt.ShowErrorMessage("Face not detected or subject moving around too much"); }


    }

    private void takeFrontPic_Click(object sender, EventArgs e)
    {
        //take picture of the front
        //take four

        if (brightnessMenu.Enabled == true ? CalibrationCamera() : true)
        {
            for (int i = 0; i < 4; i++)
            {
                FrontMugs[i] = new FrontMugshot(capturedimage.QueryFrame().ToBitmap());
                if (FrontMugs[i].isGoodMugshot == false || FrontMugs[i] == null)
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
                }
                pblist[i].Update();
                Thread.Sleep(500);
            }
        }
        else { string promptValue = ErrorPrompt.ShowErrorMessage("Face not detected or subject moving around too much. please try again."); }


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

    private void brightnessMenu_Click(object sender, EventArgs e)
    {
        Bitmap bm = capturedimage.QueryFrame().ToBitmap();
        var brightnesssetting = new BrightnessSettings(capturedimage);
        brightnesssetting.ShowDialog(this);

    }

    public bool CalibrationCamera()
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
            while (!inmiddle)
            {
                bm = capturedimage.QueryFrame().ToBitmap();

                mugshotface = bm.ToMat();
                using var model = InitializeFaceDetectionModel(new Size(mugshotface.Width, mugshotface.Height));
                model.Detect(mugshotface, facefeatures);

                var facedata = (float[,])facefeatures.GetData(jagged: true);

                if (facedata != null)
                {
                    face = new float[] { facedata[0, 0], facedata[0, 1], facedata[0, 2], facedata[0, 3] };
                    if ((mugshotface.Width / 2) < face[0] + (face[2] / 2))
                    {
                        if (rpp != 0 && rpp != 1) inmiddle = true;
                        sp.Write(new byte[] { 0x81, 0x01, 0x06, 0x03, 0x18, 0x18, 0x00, 0x00, 0x04, 0x08, 0x00, 0x00, 0x00, 0x00, 0xFF }, 0, 15);

                    }
                    else
                    {
                        if (rpp != 0 && rpp != 2) inmiddle = true;

                        sp.Write(new byte[] { 0x81, 0x01, 0x06, 0x03, 0x18, 0x18, 0x0F, 0x0F, 0x0B, 0x08, 0x00, 0x00, 0x00, 0x00, 0xFF }, 0, 15);

                        rpp = 2;
                    }

                }
                else
                {
                    return false;
                }
            }
            bool incenter = false;
            int rp = 0;
            while (!incenter)
            {
                bm = capturedimage.QueryFrame().ToBitmap();

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
                    return false;
                }
            }

            int zv = 1;
            bool zoomright = false;
            short zoomvalue = 1000;
            while (!zoomright)
            {
                bm = capturedimage.QueryFrame().ToBitmap();

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
                        sp.Write(getbfromi((short)((float)zoomvalue * 1.1f * zv)), 0, 9);
                    }

                }
                else
                {
                    return false;
                }
                zv++;
                zoomvalue = (short)((float)zoomvalue / 1.1f);
            }
            System.Diagnostics.Debug.WriteLine(zv.ToString());
            sp.Close();
        }
        return true;

    }
    public byte[] getbfromi(short zoomvalue)
    {
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
    public static  class ErrorPrompt
    {
        public static string ShowErrorMessage(string text)
        {
            Form prompt = new Form()
            {
                Width = (text.Length*10)+50,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "",
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label() { Left = 50, Top = 20, Width = text.Length * 10, Text = text };
            System.Windows.Forms.Button confirmation = new System.Windows.Forms.Button() { Text = "Ok", Left = 50, Width = 100, Top = 50, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;
            return prompt.ShowDialog() == DialogResult.OK ? "" : "";

        }
    }
}
