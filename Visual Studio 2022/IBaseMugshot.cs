using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace automugshot.Visual_Studio_2022
{
    internal interface IBaseMugshot
    {
        public VideoCapture capturedimage { get; set; }
        public Bitmap originalbm { get; set; }
        public float[] face { get; set; }
        public float[] lefteye { get;  }
        public float[] righteye { get;  }
        public bool isGoodMugshot { get; set; }
        //public bool isHeadTited { get; set; }
        // public bool isFacingfront { get; set; }
        //public bool areEyesOpen { get; set; }
        public Bitmap croppedbm { get; }



    }
}
