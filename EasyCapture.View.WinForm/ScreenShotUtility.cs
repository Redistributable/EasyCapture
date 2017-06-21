using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Redefinable.Applications.EasyCapture.View.WinForm
{
    public static class ScreenShotUtility
    {
        public static ScreenShotType DefaultScreenShotType
        {
            get;
            set;
        }


        static ScreenShotUtility()
        {
            DefaultScreenShotType = ScreenShotType.Managed;
        }



        public static Image GetScreenShot()
        {
            return GetScreenShot(DefaultScreenShotType);
        }

        public static Image GetScreenShot(ScreenShotType type)
        {
            Size displaySize = new Size(
                Screen.PrimaryScreen.Bounds.Width, 
                Screen.PrimaryScreen.Bounds.Height );

            return GetScreenShot(type, new Point(0, 0), displaySize);
        }

        public static Image GetScreenShot(ScreenShotType type, Point location, Size size)
        {
            Image resultImage = null;

            if (type == ScreenShotType.Managed)
            {
                Bitmap bmp = new Bitmap(size.Width, size.Height);
                Graphics g = Graphics.FromImage(bmp);
                g.CopyFromScreen(location, new Point(0, 0), size);
                g.Dispose();

                resultImage = bmp;
            }
            else
            {
                throw new NotImplementedException();
            }

            return resultImage;
        }

        public static Image GetCroppedScreenShot(int x, int y, int width, int height)
        {
            return GetScreenShot(DefaultScreenShotType, new Point(x, y), new Size(width, height));
        }
    }

    public enum ScreenShotType
    {
        Win32API,
        Managed,
    }
}
