using Emgu.CV.Dnn;
using Emgu.CV.Structure;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Bitmap croppedbm { get => cropmugshot(); }

        public SideMugshot(Bitmap orignialbm)
        {

                originalbm = orignialbm;
                Mat mugshotface = originalbm.ToMat();
                var facefeatures = new Mat();
                using var model = InitializeFaceDetectionModel(new Size(mugshotface.Width, mugshotface.Height));
                model.Detect(mugshotface, facefeatures);
                var facedata = (float[,])facefeatures.GetData(jagged: true);
            if(facedata != null)
            {
                face = new float[] { facedata[0, 0], facedata[0, 1], facedata[0, 2], facedata[0, 3] };
                lefteye = new float[] { facedata[0, 4], facedata[0, 5] };
                righteye = new float[] { facedata[0, 6], facedata[0, 7] };
                isGoodMugshot = true;
            }
            else { 

                isGoodMugshot = false;
            }
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
            {
                try
                {
                    x = facerec.X - (facerec.Width / 3);
                    xw = (int)(facerec.Width + ((facerec.Width * 5f) / 5f));
                    xydiff = (((xw * 4) / 3) - facerec.Height);
                    y = facerec.Y - (xydiff / 3);
                    yw = facerec.Height + xydiff;
                    rect = new Rectangle(x, y, xw, yw);
                    newbit = originalbm.Clone(rect, originalbm.PixelFormat);
                    return newbit;
                }
                catch
                {
                    newbit = originalbm;
                    isGoodMugshot = false;
                }

            }
            return newbit;
        }
    }
}
