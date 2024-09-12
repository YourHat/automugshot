using Emgu.CV;
using Emgu.CV.Dnn;
using Emgu.CV.Reg;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Hompus.VideoInputDevices;
using System.Security.Cryptography;
using Xunit;
using System.IO.Ports;


namespace automugshot
{
    internal class FrontMugshot : IFrontMugshot
    {
        private readonly CascadeClassifier cas_eyes_deteection = new CascadeClassifier("haarcascade_eye.xml");
        //Emgu.CV files
        

        private FaceDetectorYN InitializeFaceDetectionModel(Size inputSize) => new FaceDetectorYN(
            model: "face_detection_yunet_2023mar.onnx",
            config: string.Empty,
            inputSize: inputSize,
            scoreThreshold: 0.8f,
            nmsThreshold: 0.3f,
            topK: 5000,
            backendId: Emgu.CV.Dnn.Backend.Default,
            targetId: Target.Cpu
        );

        public VideoCapture capturedimage { get; set; }
        public Bitmap originalbm { get; set; }

    
        public float[] face { get; set; }
        public float[] lefteye { get; set; }
        public float[] righteye { get; set; }

        public bool isGoodMugshot { get; set; }
        public bool isHeadTilted { get => ((Math.Acos(Math.Abs(righteye[0] - lefteye[0]) / (Math.Sqrt(Math.Pow(righteye[0] - lefteye[0], 2) + Math.Pow(Math.Abs(righteye[1] - lefteye[1]), 2)))) * (180f / Math.PI)) < 10);}
        public bool isFacingfront { get => (Math.Abs(Math.Abs(face[0] - lefteye[0]) - Math.Abs((face[0] + face[2]) - righteye[0])) < face[2] / 10); }
        public bool areEyesOpen { get => cas_eyes_deteection.DetectMultiScale(originalbm.ToImage<Gray, byte>(), 1.5, 8, Size.Empty).Length > 1; }
        public Bitmap croppedbm { get; set; }

        public FrontMugshot(Bitmap orignialbdm)
        {

             originalbm = orignialbdm;
                Mat mugshotface = originalbm.ToMat();
                var facefeatures = new Mat();
                using var model = InitializeFaceDetectionModel(new Size(mugshotface.Width, mugshotface.Height));
                model.Detect(mugshotface, facefeatures);

                var facedata = (float[,])facefeatures.GetData(jagged: true);
            int faceint;
                if (facedata != null)
                {
                faceint = getinmateface(facedata, mugshotface.Width / 2);
                face = new float[] { facedata[faceint, 0], facedata[faceint, 1], facedata[faceint, 2], facedata[faceint, 3] };
                lefteye = new float[] { facedata[faceint, 4], facedata[faceint, 5] };
                righteye = new float[] { facedata[faceint, 6], facedata[faceint, 7] };
                isGoodMugshot = true;
                }
                else
                {

                    isGoodMugshot = false;
                }
            if (isGoodMugshot) croppedbm = cropmugshot();
            }

        public Bitmap cropmugshot()
        {
            
            Rectangle facerec = new Rectangle((int)face[0], (int)face[1], (int)face[2], (int)face[3]);
            int x = (int)(facerec.X - (facerec.Width / (5f/2f)));
            int xw = (int)(facerec.Width + ((facerec.Width / (5f/2f)) * 2));
            int xydiff = (((xw * 4) / 3) - facerec.Height);
            int y = facerec.Y - (xydiff / 2);
            int yw = facerec.Height + xydiff;
            Rectangle rect = new Rectangle(x, y, xw, yw);
            Bitmap newbit;
            try
            {
                newbit = originalbm.Clone(rect, originalbm.PixelFormat);
                return newbit;
            }
            catch (Exception e)
            {// zoom out if its bad
                if (Settings1.Default.controllerwork) { 
                    using (var sp = new System.IO.Ports.SerialPort(Settings1.Default.controllername, 9600, Parity.None, 8, StopBits.One))
                    {
                        sp.Open();
                        sp.Write(new byte[] { 0x81, 0x01, 0x04, 0x07, 0x35, 0xFF }, 0, 6);
                        Thread.Sleep(500);
                        sp.Write(new byte[] { 0x81, 0x01, 0x04, 0x07, 0x00, 0xFF }, 0, 6);
                        sp.Close();
                    }
                Thread.Sleep(100);
                 }
                newbit = originalbm;
                isGoodMugshot = false;


            }
            return newbit;

        }
        // get inmate face (face in the middle)
        public int getinmateface(float[,] facelist, int piccenter)
        {
            if (facelist.GetLength(0) == 1) return 0;
            int closest = 0;
            float difffromcenter;
            float diff = piccenter * 2;
            for (int i = 0; i < facelist.GetLength(0); i++)
            {
                difffromcenter = Math.Abs(piccenter - (facelist[i, 0] + (facelist[i, 2] / 2)));
                if (diff > difffromcenter)
                {
                    closest = i;
                    diff = difffromcenter;
                }
            }

            return closest;
        }

    }
}
