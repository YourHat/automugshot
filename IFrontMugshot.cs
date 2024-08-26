using automugshot.Visual_Studio_2022;
using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace automugshot
{
    internal interface IFrontMugshot : IBaseMugshot
    {
        public VideoCapture capturedimage { get; set; }
        public Bitmap originalbm { get; set; }
        public float[] face { get; set; }
        public float[] lefteye { get; set; }
        public float[] righteye { get; set; }

        public bool isGoodMugshot { get; set; }
        
        public bool isHeadTilted { get;  }
        
        public bool isFacingfront { get; }
        
        public bool areEyesOpen { get; }
        public Bitmap croppedbm { get; set; }

        public Bitmap cropmugshot();


    }
}


