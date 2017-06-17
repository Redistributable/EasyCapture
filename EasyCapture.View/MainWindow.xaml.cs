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


        public MainWindow()
        {
            InitializeComponent();

            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                var icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Reflection.Assembly.GetEntryAssembly().Location);
                icon.Save(ms);
                ms.Seek(0, System.IO.SeekOrigin.Begin);
                this.Icon = BitmapFrame.Create(ms);
            }

            this.WindowStartMainPanel.SettingButtonClick += WindowStartMainPanel_SettingButtonClick;
            this.WindowSubpageBottomBox.PrevButtonClick += WindowSubpageBottomBox_PrevButtonClick;
        }


        private void WindowStartMainPanel_SettingButtonClick(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine("イベントが発生しました。");

            this.WindowStartMainPanel.Visibility = Visibility.Hidden;
            this.WindowSettingMainPanel.Visibility = Visibility.Visible;

            this.WindowSubpageBottomBox.Visibility = Visibility.Visible;

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

                this.WindowSettingMainPanel.Visibility = Visibility.Hidden;

                this.WindowSubpageBottomBox.Visibility = Visibility.Collapsed;

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
    }

    enum MainWindowPage
    {
        Start,
        RectCapture,
        WindowCapture,
        Setting,
    }
}
