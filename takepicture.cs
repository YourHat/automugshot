using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Hompus.VideoInputDevices;
using Emgu.CV.Structure;
using System.Security.Cryptography;
using Emgu.CV.Dnn;
using Xunit;



namespace automugshot
{
    internal class Takepicture
    {


        static readonly CascadeClassifier cas_eyes_deteection = new CascadeClassifier("haarcascade_eye.xml");
        //Emgu.CV files

        public List<Bitbool> takefrontpic1()
        {
            var fourPicturesList = new List<Bitbool>();
            // create list to return 
            VideoCapture capturedimage = new VideoCapture(Settings1.Default.cameraindex);
            // get camera to use
            Bitmap bmp;
            Checkface result;

            // take 4 pictures
            for (int i = 0; i < 4; i++)
            {
                // bmp = capturedimage.QueryFrame().ToBitmap();
                //get image from the camera
                //result = checkmugshotfrontnew(bmp);
                //check the picture
                //fourPicturesList.Add(new Bitbool(false, croppic(bmp, result.facearea ?? new Rectangle[] { new Rectangle(20, 20, 20, 20) })));
                // add it to the list
                //System.Threading.Thread.Sleep(1000);
                //wait for a second 
            }

            return fourPicturesList;
            //return the 4 pictures taken
        }

        public Detectedface checkmugshotfrontnew(Mat mat)
        {
            var face = getFaceFront(mat);
            face.croppedbm = cropfrontface(face);
            face.isHeadTited = isHeadTiltedFront(face);
            face.isFacingfront = isFacingfrontFront(face);
            face.areEyesOpen = areEyesOpenFront(face);
            return face;
        }

        public Bitmap cropfrontface(Detectedface df)
        {
            Rectangle facerec = new Rectangle((int)df.face[0], (int)df.face[1], (int)df.face[2], (int)df.face[3]);
            int x = facerec.X - (facerec.Width / 3);
            int xw = (int)(facerec.Width + ((facerec.Width / 3) * 2));
            int xydiff = (((xw * 4) / 3) - facerec.Height);
            int y = facerec.Y - (xydiff / 2);
            int yw = facerec.Height + xydiff;
            Rectangle rect = new Rectangle(x, y, xw, yw);
            Bitmap newbit;
            try
            {
                newbit = df.originalbm.Clone(rect, df.originalbm.PixelFormat);
                return newbit;


            }
            catch (Exception e)
            {
                newbit = df.originalbm;
            }
            return newbit;
        }
        public Detectedface getFaceFront(Mat mat)
        {
            var faces = new Mat();
            using var model = InitializeFaceDetectionModel(new Size(mat.Width, mat.Height));
            model.Detect(mat, faces);
            var facedata = (float[,])faces.GetData(jagged: true);
            var df = new Detectedface(mat.ToBitmap(), new float[] { facedata[0, 0], facedata[0, 1], facedata[0, 2], facedata[0, 3] }, new float[] { facedata[0, 4], facedata[0, 5] }, new float[] { facedata[0, 6], facedata[0, 7] }, true);
            return df;
        }
        public bool areEyesOpenFront(Detectedface df)
        {
            var eyecou = cas_eyes_deteection.DetectMultiScale(df.originalbm.ToImage<Gray, byte>(),1.01,10,Size.Empty);
            if (eyecou.Length == 0) return false;
            return true;
        }

        public bool isHeadTiltedFront(Detectedface df)
        {
            if (Math.Abs(df.lefteye[1] - df.righteye[1]) < df.face[3] / 20) return true;
            return false;
        }
        public bool isFacingfrontFront(Detectedface df)
        {
            if (Math.Abs(Math.Abs(df.face[0] - df.lefteye[0]) - Math.Abs((df.face[0] + df.face[2]) - df.righteye[0])) < df.face[2] / 10) return true;
            return false;
        }
        FaceDetectorYN InitializeFaceDetectionModel(Size inputSize) => new FaceDetectorYN(
            model: "face_detection_yunet_2023mar.onnx",
            config: string.Empty,
            inputSize: inputSize,
            scoreThreshold: 0.8f,
            nmsThreshold: 0.3f,
            topK: 5000,
            backendId: Emgu.CV.Dnn.Backend.Default,
            targetId: Target.Cpu);


        public List<Bitbool> takeSidepic2()
        {
            var fourPicturesList = new List<Bitbool>();
            // create list to return 
            VideoCapture capturedimage = new VideoCapture(Settings1.Default.cameraindex);
            // get camera to use
            Bitmap bmp;
            Checkface result;

            // take 4 pictures
            for (int i = 0; i < 4; i++)
            {
                // bmp = capturedimage.QueryFrame().ToBitmap();
                //get image from the camera
                //result = checkmugshotfrontnew(bmp);
                //check the picture
                //fourPicturesList.Add(new Bitbool(false, croppic(bmp, result.facearea ?? new Rectangle[] { new Rectangle(20, 20, 20, 20) })));
                // add it to the list
                //System.Threading.Thread.Sleep(1000);
                //wait for a second 
            }

            return fourPicturesList;
            //return the 4 pictures taken
        }

        public Detectedface checkmugshotSidenew(Mat mat)
        {
            var face = getFaceSide(mat);
            face.croppedbm = cropsideface(face);
            face.isFacingfront = isFacingfrontSide(face);
            face.areEyesOpen = areEyesOpenSide(face);
           
            return face;
        }

        public Bitmap cropsideface(Detectedface df)
        {
            Rectangle facerec = new Rectangle((int)df.face[0], (int)df.face[1], (int)df.face[2], (int)df.face[3]);
            int x = facerec.X - (facerec.Width / 3);
            int xw = (int)(facerec.Width + ((facerec.Width * 9f) / 5f));
            int xydiff = (((xw * 4) / 3) - facerec.Height);
            int y = facerec.Y - (xydiff / 3);
            int yw = facerec.Height + xydiff;
            Rectangle rect = new Rectangle(x,y,xw,yw);
            Bitmap newbit;
            try
            {
                newbit = df.originalbm.Clone(rect, df.originalbm.PixelFormat);
                return newbit;
  

            }
            catch (Exception e)
            {
                newbit = df.originalbm;
            }
            return newbit;

        }
        public Detectedface getFaceSide(Mat mat)
        {
            var faces = new Mat();
            using var model = InitializeFaceDetectionModel(new Size(mat.Width, mat.Height));
            model.Detect(mat, faces);
            var facedata = (float[,])faces.GetData(jagged: true);
            var df = new Detectedface(mat.ToBitmap(), new float[] { facedata[0, 0], facedata[0, 1], facedata[0, 2], facedata[0, 3] }, new float[] { facedata[0, 4], facedata[0, 5] }, new float[] { facedata[0, 6], facedata[0, 7] }, true);
            return df;
        }
        public bool areEyesOpenSide(Detectedface df)
        {
            var eyecou = cas_eyes_deteection.DetectMultiScale(df.originalbm.ToImage<Gray, byte>(), 1.01, 10, Size.Empty);
            if (eyecou.Length == 0) return false;
            return true;
        }

        public bool isFacingfrontSide(Detectedface df)
        {
            if (Math.Abs(df.lefteye[0] - df.righteye[0]) < df.face[2] / 5) return true;
            return false;
        }
































        /*
                public List<Bitbool> takefrontpic()
                {
                    var fourPicturesList = new List<Bitbool>();
                    // create list to return 
                    VideoCapture capturedimage = new VideoCapture(Settings1.Default.cameraindex);
                    // get camera to use
                    Bitmap bmp;
                    Checkface result;

                    // take 4 pictures
                    for(int i = 0; i<4; i++)
                    {
                        bmp = capturedimage.QueryFrame().ToBitmap();
                        //get image from the camera
                        result = checkmugshotfront(bmp);
                        //check the picture
                        fourPicturesList.Add(new Bitbool(false, croppic(bmp, result.facearea??new Rectangle[] { new Rectangle(20,20,20,20) })));
                        // add it to the list
                        System.Threading.Thread.Sleep(1000);
                        //wait for a second 
                    }

                    return fourPicturesList;
                    //return the 4 pictures taken
                }

                public Checkface checkmugshotfront(Bitmap bm)
                {
                    Image<Bgr, byte> bmpic = bm.ToImage<Bgr, byte>();

                    var faceArray = detectface(bmpic);
                    if(faceArray is Array)
                    {
                        var eyeArray = detecteyes(bmpic);
                        if (eyeArray.Length >= 2)
                        {
                            if(isHeadTilted(eyeArray))
                            {
                                if (isFaceSideways(eyeArray, faceArray)){
                                    return new Checkface(true, faceArray);
                                }
                            }
                        }
                    }
                    return new Checkface(false, faceArray);
                }
                public System.Drawing.Rectangle[] detectface(Image<Bgr, byte> bmpics)
                {
                    var facecou = cas_face_deteection.DetectMultiScale(bmpics,1.4 );
                    if (facecou.Length == 0) return null;
                    System.Diagnostics.Debug.WriteLine(String.Join(", ", facecou));
                    System.Diagnostics.Debug.WriteLine("face detected");
                    //return [facecou[0].X,facecou[0].Width];
                    return facecou;
                }

                public System.Drawing.Rectangle[] detecteyes(Image<Bgr, byte> bmpics)
                {
                    var eyecou = cas_eyes_deteection.DetectMultiScale(bmpics);
                    // System.Diagnostics.Debug.WriteLine("eyes detected");
                    // System.Diagnostics.Debug.WriteLine(String.Join(", ", eyecou));
                    return eyecou;

                }

                public bool isHeadTilted(System.Drawing.Rectangle[] rt)
                {
                    if (Math.Abs(rt[0].Y - rt[1].Y) < Math.Abs((int)(((float)rt[0].X - (float)rt[1].X) / 5f))) return true;
                    return false;
                }
                public bool isFaceSideways(System.Drawing.Rectangle[] rt, System.Drawing.Rectangle[] faceint)
                {
                    var difference = rt[0].X < rt[1].X ? ((faceint[0].X + faceint[0].Width) - (rt[1].X + rt[1].Width)) - (rt[0].X - faceint[0].X) : ((faceint[0].X + faceint[0].Width) - (rt[0].X + rt[0].Width)) - (rt[1].X - faceint[0].X);
                    if (difference < (int)(faceint[0].Width/5f)) return true;
                    return false;
                }
                public Bitmap changebrightness(Bitmap bm)
                {

                    return bm;
                }
                public Bitmap croppic(Bitmap bm, System.Drawing.Rectangle[] rec)
                {
                    System.Drawing.Rectangle facerec = rec[0];
                    System.Drawing.Rectangle rect = new System.Drawing.Rectangle(facerec.X - (facerec.Width/3), facerec.Y - facerec.Height, facerec.Width + (facerec.Width / 3)*2, facerec.Height +facerec.Height*2 );
                    Bitmap cropped;
                    try
                    {
                        System.Diagnostics.Debug.WriteLine(String.Format("WOW - {0} - {1} -{2} -{3}", facerec.X - (facerec.Width / 6), facerec.Y - (facerec.Height / 2), facerec.Width + (facerec.Width / 3), facerec.Height + facerec.Height));

                        Bitmap newbit = bm.Clone(rect, bm.PixelFormat);

                        int diffsize = (int)((newbit.Height - ((newbit.Width*3f)/4f))/2f);
                        System.Diagnostics.Debug.WriteLine(String.Format("WOW - {0} - {1} -{2}", diffsize, newbit.Height, newbit.Height - diffsize));

                        System.Drawing.Rectangle rectnew = new System.Drawing.Rectangle(0, diffsize, newbit.Width, newbit.Height-diffsize);
                        cropped = newbit.Clone(rectnew, newbit.PixelFormat);


                    }
                    catch (Exception e)
                    {
                        cropped = bm;
                    }

                    return cropped;
                }

                public List<Bitbool> takesidepic()
                {
                    var fourPicturesList = new List<Bitbool>();
                    // create list to return 
                    VideoCapture capturedimage = new VideoCapture(Settings1.Default.cameraindex);
                    // get camera to use
                    Bitmap bmp;
                    Checkface result;

                    // take 4 pictures
                    for (int i = 0; i < 4; i++)
                    {
                        bmp = capturedimage.QueryFrame().ToBitmap();
                        //get image from the camera
                        result = checkmugshotside(bmp);
                        //check the picture
                        fourPicturesList.Add(new Bitbool(false, croppic(bmp, result.facearea ?? new System.Drawing.Rectangle[] { new System.Drawing.Rectangle(20, 20, 20, 20) })));
                        // add it to the list
                        System.Threading.Thread.Sleep(1000);
                        //wait for a second 
                    }

                    return fourPicturesList;
                    //return the 4 pictures taken
                }

                public Checkface checkmugshotside(Bitmap bm)
                {
                    Image<Bgr, byte> bmpic = bm.ToImage<Bgr, byte>();

                    var faceArray = detectsideface(bmpic);
                    if (faceArray is Array)
                    {
                      //  var eyeArray = detecteyes(bmpic);
                      //  if (eyeArray.Length >= 2)
                       // {
                       //     if (isHeadTilted(eyeArray))
                       //     {
                       //         if (isFaceSideways(eyeArray, faceArray))
                        //        {
                                    return new Checkface(true, faceArray);
                        //        }
                        //    }
                     //   }
                    }
                    return new Checkface(false, faceArray);
                }

                public System.Drawing.Rectangle[] detectsideface(Image<Bgr, byte> bmpics)
                {
                    var facecou = cas_side_deteection.DetectMultiScale(bmpics, 1.4);
                    if (facecou.Length == 0) return null;
                    System.Diagnostics.Debug.WriteLine(String.Join(", ", facecou));
                    System.Diagnostics.Debug.WriteLine("face detected");
                    //return [facecou[0].X,facecou[0].Width];
                    return facecou;
                }
            }
        */
    }

    public struct Checkface
            {
                public bool isgoodface;
                public System.Drawing.Rectangle[] facearea;
                public Checkface(bool isgoodface, System.Drawing.Rectangle[] facearea)
                {
                    this.isgoodface = isgoodface;
                    this.facearea = facearea;
                }
            }

   
    public struct Bitbool
    {
        public bool isgoodpic;
        public Bitmap picimg;

        public Bitbool(bool isgoodpic, Bitmap picimag)
        {
            this.isgoodpic = isgoodpic;
            this.picimg = picimag;
        }
    }
    public struct Detectedface
    {
        public readonly Bitmap originalbm;
        public readonly float[] face;
        public readonly float[] lefteye;
        public readonly float[] righteye;
        public readonly bool isfront;
        public bool isGoodMugshot { get; set; }
        public bool isHeadTited { get; set; }
        public bool isFacingfront { get; set; }
        public bool areEyesOpen { get; set; }
        public Bitmap croppedbm {  get; set; }
        public Detectedface(Bitmap originalbm, float[] face, float[] righteye, float[] lefteye, bool isfront)
        {
            this.originalbm = originalbm;
            this.face = face;
            this.lefteye = lefteye;
            this.righteye = righteye;
            this.isfront = isfront;
        }
    }
}