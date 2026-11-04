using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestStack.White.UIItems.WindowItems;

namespace Test.Common
{
    public class BaseTest
    {
        protected Window window = null;
        public static int DelayTime = 10; // 1 sec
        public const int TEST_TIMEOUT = 240000; //120 seconds / 2min

        public BaseTest()
        {

        }

        public BaseTest(Window window)
        {
            this.window = window;
        }

        public Window GetWindow()
        {
            return this.window;
        }

        public virtual BaseTest Delay()
        {
            Thread.Sleep(DelayTime);
            return this;
        }

        public bool ImagesAreSame(Bitmap image1, Bitmap image2)
        {
            if (image1.Width != image2.Width || image1.Height != image2.Height) return false;
            for (int x = 0; x < image1.Width; x++)
            {
                for (int y = 0; y < image1.Height; y++)
                {
                    if(image1.GetPixel(x,y).ToArgb() != image2.GetPixel(x,y).ToArgb()) return false;
                }                
            }

            return true;
        }

        public bool CheckImages(Bitmap image, string refImageFileName)
        {
            if(File.Exists(@".\TestData\"+refImageFileName+".bmp"))
            {
                var refImage = new Bitmap(@".\TestData\"+refImageFileName+".bmp");
                if(!ImagesAreSame(image, refImage))
                {
//                    image.Save(@".\TestFailures\"+refImageFileName+"_Failed.bmp"); //TODO find out why this doesn't work
                    return false;
                }
            }
            else
            {
                image.Save(@".\TestData\" + refImageFileName + ".bmp");
            }

            return true;
        }

        protected bool AreClose(double a, double b)
        {
            return Math.Abs(a - b) < 2;
        }
        protected void VerifyCentered(Window parent)
        {
            Assert.IsTrue(AreClose((float)window.Bounds.Left, (parent.Bounds.Left + parent.Bounds.Right) / 2 - (window.Bounds.Width) / 2), "Window is not centerd with respect to the parent");
            Assert.IsTrue(AreClose((float)window.Bounds.Top, (parent.Bounds.Top + parent.Bounds.Bottom) / 2 - (window.Bounds.Height) / 2), "Window is not centerd with respect to the parent");
        }
    }
}
