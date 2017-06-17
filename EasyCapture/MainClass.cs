using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Markup;

using Redefinable.Applications.EasyCapture.View;
using Redefinable.Applications.EasyCapture.View.XamlTest;


namespace Redefinable.Applications.EasyCapture
{
    public static class MainClass
    {
        [STAThread]
        public static void Main(string[] args)
        {
            System.Windows.Forms.Application.EnableVisualStyles();

            /*
            TestWindow window = new TestWindow();
            Application app = new Application();
            app.Run(window);
            */

            MainWindow mainWnd = new MainWindow();
            Application app = new Application();
            app.Run(mainWnd);
        }
    }
}
