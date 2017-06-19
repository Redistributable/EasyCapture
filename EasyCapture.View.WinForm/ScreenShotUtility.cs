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
        public static Image GetScreenShot()
        {
            return GetScreenShot(ScreenShotType.Managed);
        }

        public static Image GetScreenShot(ScreenShotType type)
        {
            Image resultImage = null;

            if (type == ScreenShotType.Managed)
            {
                Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                    Screen.PrimaryScreen.Bounds.Height);
                Graphics g = Graphics.FromImage(bmp);
                g.CopyFromScreen(new Point(0, 0), new Point(0, 0), bmp.Size);
                g.Dispose();

                resultImage = bmp;
            }
            else
            {
                throw new NotImplementedException();
            }

            return resultImage;
        }
    }

    public enum ScreenShotType
    {
        Win32API,
        Managed,
    }
}
