using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Redefinable.Applications.EasyCapture.View
{
    /// <summary>
    /// RectCaptureSelectToolWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class RectCaptureSelectToolWindow : Window
    {
        private double startX;
        private double startY;
        private double currentWidth;
        private double currentHeight;

        

        public RectCaptureSelectToolWindow()
        {
            InitializeComponent();

            this.startX = 0;
            this.startY = 0;
            this.currentWidth = 0;
            this.currentHeight = 0;

            this.MainGrid.MouseLeftButtonDown += MainGrid_MouseLeftButtonDown;
            this.MainGrid.MouseMove += MainGrid_MouseMove;
            this.MainGrid.MouseLeftButtonUp += MainGrid_MouseLeftButtonUp;
        }

        private void MainGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var point = e.GetPosition(this);
            this.startX = point.X;
            this.startY = point.Y;

            this.SelectRect.Margin = new Thickness(point.X, point.Y, 0, 0);
            this.SelectRect.Width = 0;
            this.SelectRect.Height = 0;

            this.SelectRect.Visibility = Visibility.Visible;
        }

        private void MainGrid_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.SelectRect.Visibility != Visibility.Visible || e.LeftButton != MouseButtonState.Pressed)
                return;

            var point = e.GetPosition(this.SelectRect);
            this.currentWidth = point.X;
            this.currentHeight = point.Y;

            this.SelectRect.Width = point.X;
            this.SelectRect.Height = point.Y;
        }

        private void MainGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        

        public RectCaptureSelectToolWindowResult ShowSelectWindow()
        {
            System.Drawing.Image screenShot = WinForm.ScreenShotUtility.GetScreenShot();
            BitmapImage bitmapImage = new BitmapImage();

            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                screenShot.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                ms.Seek(0, System.IO.SeekOrigin.Begin);

                bitmapImage.BeginInit();
                bitmapImage.StreamSource = ms;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
            }


            this.BackImage.Source = bitmapImage;
            //this.Owner = owner;
            this.ShowDialog();

            Rect result = new Rect(this.startX, this.startY, this.currentWidth, this.currentHeight);
            return new RectCaptureSelectToolWindowResult(result, false);
        }
    }

    public struct RectCaptureSelectToolWindowResult
    {
        private Rect rectangle;
        private bool cancel;


        public Rect Rectangle
        {
            get { return this.rectangle; }
            set { this.rectangle = value; }
        }

        public bool Cancel
        {
            get { return this.cancel; }
            set { this.cancel = value; }
        }


        public RectCaptureSelectToolWindowResult(Rect rectangle, bool cancel)
        {
            this.rectangle = rectangle;
            this.cancel = cancel;
        }
    }
}
