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
using System.Windows.Threading;

using Redefinable.Applications.EasyCapture.Models;


namespace Redefinable.Applications.EasyCapture.View
{
    /// <summary>
    /// WindowCapturePanel.xaml の相互作用ロジック
    /// </summary>
    public partial class WindowCapturePanel : UserControl
    {
        // 非公開フィールド
        ICollection<WindowInfo> windowInformations;
        DispatcherTimer dispatcherTimer;


        // 公開プロパティ




        // コンストラクタ

        /// <summary>
        /// 新しいWindowCapturePanelクラスのインスタンスを初期化子ます。
        /// </summary>
        public WindowCapturePanel()
        {
            InitializeComponent();

            this.ComboBoxRefreshButton.Click += (sender, e) => { this.UpdateWindowInformations(); };
            this.WindowsComboBox.SelectionChanged += WindowsComboBox_SelectionChanged;

            this.UpdateWindowInformations();
            this.WindowsComboBox.SelectedIndex = 1;

            this.dispatcherTimer = new DispatcherTimer();
            this.dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            this.dispatcherTimer.Tick += DispatcherTimer_Tick;
            this.dispatcherTimer.Start();
        }

        private void WindowsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //this.updatePreviewImage();
            //Console.WriteLine(this.WindowsComboBox.SelectedItem);
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (this.Visibility == Visibility.Visible)
                this.updatePreviewImage();
        }

        private void updatePreviewImage()
        {
            if (this.PreviewImage.Source != null)
            {
                //var currentImage = this.PreviewImage.Source;
                this.PreviewImage.Source = null;
            }


            Object selected = this.WindowsComboBox.SelectedItem;
            if (selected == null)
                this.PreviewImage.Source = null;
            else
            {
                WindowInfo info = ((WindowItemInfo)selected).WindowInfo;

                if (info.Width == 0 || info.Height == 0)
                    throw new Exception(info.Name + "は高さまたは幅が0です。");

                var screenShot = WinForm.ScreenShotUtility.GetCroppedScreenShot(info.PositionX, info.PositionY, info.Width, info.Height);
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


                this.PreviewImage.Source = bitmapImage;
            }
        }

        /// <summary>
        /// 現在のウィンドウ情報を取得し、コンボボックスを更新します。
        /// </summary>
        public void UpdateWindowInformations()
        {
            this.WindowsComboBox.Items.Clear();
            
            this.windowInformations = WindowInfo.GetAllActiveWindows();
            //Console.WriteLine(" ==  Informations == ");
            foreach (var item in this.windowInformations)
            {
                this.WindowsComboBox.Items.Add(new WindowItemInfo(item, false));
                //Console.WriteLine("\"{0}\"=x:{1},y:{2},w:{3},h:{4}", item.Name, item.PositionX, item.PositionY, item.Width, item.Height);
                foreach (var subItem in item.Children)
                {
                    this.WindowsComboBox.Items.Add(new WindowItemInfo(subItem, true));
                    //Console.WriteLine(">> \"{0}\"=x:{1},y:{2},w:{3},h:{4}", subItem.Name, subItem.PositionX, subItem.PositionY, subItem.Width, subItem.Height);
                }
            }
        }


        class WindowItemInfo
        {
            // 非公開フィールド
            private WindowInfo windowInfo;
            private bool isSubWindow;


            // 公開プロパティ
            
            public WindowInfo WindowInfo
            {
                get { return this.windowInfo; }
            }

            public bool IsSubWindow
            {
                get { return this.isSubWindow; }
            }


            // コンストラクタ

            public WindowItemInfo(WindowInfo windowInfo, bool isSubWindow)
            {
                this.windowInfo = windowInfo;
                this.isSubWindow = isSubWindow;
            }


            // 公開メソッド

            public override string ToString()
            {
                string name = this.windowInfo.Name;
                int max = 40;

                if (name.Length >= max)
                    name = name.Substring(0, max) + " ...";

                if (this.isSubWindow)
                    name = "# " + name;

                return name;
            }
        }
    }
}
