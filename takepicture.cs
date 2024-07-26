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

namespace automugshot
{
    internal class Takepicture
    {
        public List<Bitbool> takefrontpic()
        {
            var fourcams = new List<Bitbool>();
            Bitmap notgoodimage = new Bitmap(115, 150);
            using (Graphics graph = Graphics.FromImage(notgoodimage))
            {
                Rectangle ImageSize = new Rectangle(0, 0, 115, 150);
                graph.FillRectangle(Brushes.Red, ImageSize);
            }
            VideoCapture capturedimage = new VideoCapture(Settings1.Default.cameraindex);

            Bitmap bmp = capturedimage.QueryFrame().ToBitmap();
            var result = checkmugshot(bmp);
            if (result.isgoodface)
                fourcams.Add(new Bitbool(true, croppic(bmp, result.facearea)));
            else
                fourcams.Add(new Bitbool(false, notgoodimage));
            System.Threading.Thread.Sleep(1000);
            result = checkmugshot(bmp);
            if (result.isgoodface)
                fourcams.Add(new Bitbool(true, croppic(bmp, result.facearea)));
            else
                fourcams.Add(new Bitbool(false, notgoodimage));
            System.Threading.Thread.Sleep(1000);
            if (result.isgoodface)
                fourcams.Add(new Bitbool(true, croppic(bmp, result.facearea)));
            else
                fourcams.Add(new Bitbool(false, notgoodimage));
            System.Threading.Thread.Sleep(1000);
            if (result.isgoodface)
                fourcams.Add(new Bitbool(true, croppic(bmp, result.facearea)));
            else
                fourcams.Add(new Bitbool(false, notgoodimage));
            System.Threading.Thread.Sleep(1000);


            return fourcams;
        }

        public List<Bitbool> takesidepic()
        {
            var fourcams = new List<Bitbool>();
            //Bitmap result = new Bitmap(115, 150);
            VideoCapture capturedimage = new VideoCapture(Settings1.Default.cameraindex);

            fourcams.Add(new Bitbool(true, capturedimage.QueryFrame().ToBitmap()));
            System.Threading.Thread.Sleep(500);
            fourcams.Add(new Bitbool(false, capturedimage.QueryFrame().ToBitmap()));
            System.Threading.Thread.Sleep(500);
            fourcams.Add(new Bitbool(true, capturedimage.QueryFrame().ToBitmap()));
            System.Threading.Thread.Sleep(500);
            fourcams.Add(new Bitbool(true, capturedimage.QueryFrame().ToBitmap()));

            return fourcams;
        }

        static readonly CascadeClassifier cas_eyes_deteection = new CascadeClassifier("haarcascade_eye.xml");
        static readonly CascadeClassifier cas_face_deteection = new CascadeClassifier("haarcascade_frontalface_alt_tree.xml");
        public Checkface checkmugshot(Bitmap bm)
        {
            Image<Bgr, byte> bmpic = bm.ToImage<Bgr, byte>();
            var faceint = detectface(bmpic);
            if(faceint is Array)
            {
                if (eyescheck(bmpic, faceint))
                {
                    return new Checkface(true, faceint);
                }
            }
            return new Checkface(false, faceint);
        }
        public Rectangle[] detectface(Image<Bgr, byte> bmpics)
        {
            var facecou = cas_face_deteection.DetectMultiScale(bmpics,1.4 );
            if (facecou.Length == 0) return null;
            System.Diagnostics.Debug.WriteLine(String.Join(", ", facecou));
            System.Diagnostics.Debug.WriteLine("face detected");
            //return [facecou[0].X,facecou[0].Width];
            return facecou;
        }
        public bool eyescheck(Image<Bgr, byte> bmpics, Rectangle[] faceint)
        {
            var eyecou = cas_eyes_deteection.DetectMultiScale(bmpics);
            if (eyecou.Length < 2) return false;
            System.Diagnostics.Debug.WriteLine("eyes detected");
            System.Diagnostics.Debug.WriteLine(String.Join(", ", eyecou));
            if (!headtilt(eyecou)) return false;
            System.Diagnostics.Debug.WriteLine("Head not tilted");
            if (!sidetilt(eyecou, faceint)) return false;
            System.Diagnostics.Debug.WriteLine("Head not side tilted");
            return true;

        }
        public bool headtilt(Rectangle[] rt)
        {
            if (Math.Abs(rt[0].Y - rt[1].Y) < Math.Abs((int)(((float)rt[0].X - (float)rt[1].X) / 5f))) return true;
            return false;
        }
        public bool sidetilt(Rectangle[] rt, Rectangle[] faceint)
        {
            var difference = rt[0].X < rt[1].X ? ((faceint[0].X + faceint[0].Width) - (rt[1].X + rt[1].Width)) - (rt[0].X - faceint[0].X) : ((faceint[0].X + faceint[0].Width) - (rt[0].X + rt[0].Width)) - (rt[1].X - faceint[0].X);
            if (difference < (int)(faceint[0].Width/5f)) return true;
            return false;
        }
        public Bitmap changebrightness(Bitmap bm)
        {

            return bm;
        }
        public Bitmap croppic(Bitmap bm, Rectangle[] rec)
        {
            Rectangle facerec = rec[0];
            Rectangle rect = new Rectangle(facerec.X - (facerec.Width/3), facerec.Y - facerec.Height, facerec.Width + (facerec.Width / 3)*2, facerec.Height +facerec.Height*2 );
            Bitmap cropped;
            try
            {
                System.Diagnostics.Debug.WriteLine(String.Format("WOW - {0} - {1} -{2} -{3}", facerec.X - (facerec.Width / 6), facerec.Y - (facerec.Height / 2), facerec.Width + (facerec.Width / 3), facerec.Height + facerec.Height));

                Bitmap newbit = bm.Clone(rect, bm.PixelFormat);
                
                int diffsize = (int)((newbit.Height - ((newbit.Width*3f)/4f))/2f);
                System.Diagnostics.Debug.WriteLine(String.Format("WOW - {0} - {1} -{2}", diffsize, newbit.Height, newbit.Height - diffsize));

                Rectangle rectnew = new Rectangle(0, diffsize, newbit.Width, newbit.Height-diffsize);
                cropped = newbit.Clone(rectnew, newbit.PixelFormat);


            }
            catch (Exception e)
            {
                cropped = bm;
            }
           
            return cropped;
        }
    }

    public struct Checkface
    {
        public bool isgoodface;
        public Rectangle[] facearea;
        public Checkface(bool isgoodface, Rectangle[] facearea)
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
    
}