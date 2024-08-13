using automugshot.Visual_Studio_2022;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace automugshot
{
    internal interface ISideMugshot : IBaseMugshot
    {
        public VideoCapture capturedimage { get; set; }
        public Bitmap originalbm { get; set; }
        public float[] face { get; set; }
        public float[] lefteye { get; set; }
        public float[] righteye { get; set; }

        public bool isGoodMugshot { get; }

        public bool isFacingSide { get; }

        public bool isEyeOpen { get; }
        public Bitmap croppedbm { get; }


        public Bitmap cropmugshot();


    }
}
