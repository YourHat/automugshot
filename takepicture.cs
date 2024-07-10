using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace automugshot
{
    internal class Takepicture
    {
        public List<Bitbool> takefrontpic()
        {
            var fourcams = new List<Bitbool>();
            Bitmap result = new Bitmap(115, 150);
            using (Graphics graph = Graphics.FromImage(result))
            {
                Rectangle ImageSize = new Rectangle(0, 0, 115, 150);
                graph.FillRectangle(Brushes.Blue, ImageSize);
            }
           

            fourcams.Add(new Bitbool(true, result));
            fourcams.Add(new Bitbool(false, result));
            fourcams.Add(new Bitbool(true, result));
            fourcams.Add(new Bitbool(true, result));

            return fourcams;
        }

        public List<Bitbool> takesidepic()
        {
            var fourcams = new List<Bitbool>();
            Bitmap result = new Bitmap(115, 150);
            using (Graphics graph = Graphics.FromImage(result))
            {
                Rectangle ImageSize = new Rectangle(0, 0, 115, 150);
                graph.FillRectangle(Brushes.GreenYellow, ImageSize);
            }


            fourcams.Add(new Bitbool(false, result));
            fourcams.Add(new Bitbool(false, result));
            fourcams.Add(new Bitbool(false, result));
            fourcams.Add(new Bitbool(true, result));

            return fourcams;
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