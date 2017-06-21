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
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowPage currentPage;
        private int currentPageCount;
        private bool isFinished;


        /// <summary>
        /// 終了ボタンにより閉じられたかどうかを示す値を取得します。
        /// </summary>
        public bool IsFinished
        {
            get { return this.isFinished; }
        }


        public MainWindow()
        {
            InitializeComponent();

            this.ResizeMode = ResizeMode.CanMinimize | ResizeMode.CanResize;
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                var icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Reflection.Assembly.GetEntryAssembly().Location);
                icon.Save(ms);
                ms.Seek(0, System.IO.SeekOrigin.Begin);
                this.Icon = BitmapFrame.Create(ms);
            }

            this.Closing += MainWindow_Closing;
            this.WindowStartMainPanel.RectCaptureButtonClick += WindowStartMainPanel_RectCaptureButtonClick;
            this.WindowStartMainPanel.WindowCaptureButtonClick += WindowStartMainPanel_WindowCaptureButtonClick;
            this.WindowStartMainPanel.SettingButtonClick += WindowStartMainPanel_SettingButtonClick;
            this.WindowStartMainPanel.ExitButtonClick += (sender, e) => { this.finish(); };
            this.WindowSubpageBottomBox.PrevButtonClick += WindowSubpageBottomBox_PrevButtonClick;
            this.WindowSubpageBottomBox.ExitButtonClick += (sender, e) => { this.finish(); };
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!this.isFinished)
                this.finish();

            if (!this.isFinished)
                e.Cancel = true;
        }

        private void WindowStartMainPanel_RectCaptureButtonClick(object sender, RoutedEventArgs e)
        {
            this.WindowStartMainPanel.Visibility = Visibility.Hidden;
            this.WindowRectCaptureMainPanel.Visibility = Visibility.Visible;

            this.WindowSubpageBottomBox.Visibility = Visibility.Visible;

            this.WindowTopPanel.Text = "領域を指定してキャプチャ";

            this.currentPage = MainWindowPage.RectCapture;
            this.currentPageCount++;
        }

        private void WindowStartMainPanel_WindowCaptureButtonClick(object sender, RoutedEventArgs e)
        {
            this.WindowStartMainPanel.Visibility = Visibility.Hidden;
            this.WindowWindowCapturePanel.Visibility = Visibility.Visible;

            this.WindowSubpageBottomBox.Visibility = Visibility.Visible;

            this.WindowTopPanel.Text = "ウィンドウを指定してキャプチャ";

            this.currentPage = MainWindowPage.WindowCapture;
            this.currentPageCount++;



            this.WindowWindowCapturePanel.UpdateWindowInformations();

            /*
            ICollection<Models.WindowInfo> test = Models.WindowInfo.GetAllActiveWindows();
            foreach (var item in test)
                Console.WriteLine("{0}:({1:0000},{2:0000}) {3:0000}x{4:0000}", item.Name.PadRight(30), item.PositionX, item.PositionY, item.Width, item.Height);
            */
        }

        private void WindowStartMainPanel_SettingButtonClick(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine("イベントが発生しました。");

            this.WindowStartMainPanel.Visibility = Visibility.Hidden;
            this.WindowSettingMainPanel.Visibility = Visibility.Visible;

            this.WindowSubpageBottomBox.Visibility = Visibility.Visible;

            this.WindowTopPanel.Text = "キャプチャ設定";

            this.currentPage = MainWindowPage.Setting;
            this.currentPageCount++;
        }
        
        private void WindowSubpageBottomBox_PrevButtonClick(object sender, RoutedEventArgs e)
        {
            this.currentPageCount--;

            if (this.currentPageCount < 0)
                throw new Exception("UIエラーが発生しました。ページカウントが負の値を示しています。");
            else if (this.currentPageCount == 0)
            {
                // トップページに戻す
                this.WindowStartMainPanel.Visibility = Visibility.Visible;

                this.WindowRectCaptureMainPanel.Visibility = Visibility.Hidden;
                this.WindowWindowCapturePanel.Visibility = Visibility.Hidden;
                this.WindowSettingMainPanel.Visibility = Visibility.Hidden;

                this.WindowSubpageBottomBox.Visibility = Visibility.Collapsed;

                this.WindowTopPanel.Text = "";
                this.currentPage = MainWindowPage.Start;
            }
            else
            {
                // 各モード内で１つ前のページに戻す
                switch (this.currentPage)
                {
                    case MainWindowPage.RectCapture:
                        {
                            break;
                        }
                    case MainWindowPage.WindowCapture:
                        {
                            break;
                        }
                    case MainWindowPage.Setting:
                        {
                            break;
                        }
                    default:
                        throw new Exception("UIエラーが発生しました。ページモードの種類を示す値が正しくありません。");
                }
            }
        }

        private void finish()
        {
            if (MessageBox.Show(this, "終了してもよろしいですか", this.Title, MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                return;

            this.isFinished = true;
            try
            {
                this.Close();
            }
            catch (InvalidOperationException)
            {
                // dummy
            }
        }
    }

    enum MainWindowPage
    {
        Start,
        RectCapture,
        WindowCapture,
        Setting,
    }
}
