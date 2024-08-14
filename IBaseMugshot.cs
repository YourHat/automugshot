using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace automugshot
{
    internal interface IBaseMugshot
    {
        public VideoCapture capturedimage { get; set; }
        public Bitmap originalbm { get; set; }
        public float[] face { get; set; }
        public float[] lefteye { get; set; }
        public float[] righteye { get; set; }
        public bool isGoodMugshot { get; set; }
        public Bitmap croppedbm { get; }

        public Bitmap cropmugshot();
    }
}
