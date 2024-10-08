﻿using Emgu.CV.Dnn;
using Emgu.CV.Structure;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.ComponentModel.Design;

namespace automugshot
{
    internal class SideMugshot : ISideMugshot
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
        public bool isFacingSide { get => (lefteye[0] < face[0] + (face[2] / 2)) && (righteye[0] < face[0] + (face[2] / 2)); }
        public bool isEyeOpen { get => cas_eyes_deteection.DetectMultiScale(originalbm.ToImage<Gray, byte>(), 1.5, 4, Size.Empty).Length > 0; }

        public bool isFacingLeftSide { get => (righteye[0]- face[0]) < face[2]/2; }
        public Bitmap croppedbm { get; set; }

        public SideMugshot(Bitmap orignialbm)
        {

                originalbm = orignialbm;
                Mat mugshotface = originalbm.ToMat();
                var facefeatures = new Mat();
                using var model = InitializeFaceDetectionModel(new Size(mugshotface.Width, mugshotface.Height));
                model.Detect(mugshotface, facefeatures);
                var facedata = (float[,])facefeatures.GetData(jagged: true);
                int faceint;
            if(facedata != null)
            {
                faceint = getinmateface(facedata, mugshotface.Width / 2);
                face = new float[] { facedata[faceint, 0], facedata[faceint, 1], facedata[faceint, 2], facedata[faceint, 3] };
                lefteye = new float[] { facedata[faceint, 4], facedata[faceint, 5] };
                righteye = new float[] { facedata[faceint, 6], facedata[faceint, 7] };
                isGoodMugshot = true;
            }
            else { 

                isGoodMugshot = false;
            }
            if(isGoodMugshot) { croppedbm = cropmugshot(); }
        }

        public Bitmap cropmugshot()
        {
            Rectangle facerec = new Rectangle((int)face[0], (int)face[1], (int)face[2], (int)face[3]);
            int x = facerec.X - (facerec.Width / 3);
            int xw = (int)(facerec.Width + ((facerec.Width * 11f) / 5f));
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
            {// zoom out if its too close
                if (Settings1.Default.controllerwork)
                {
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

        // get inmate face ( middle face )
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
