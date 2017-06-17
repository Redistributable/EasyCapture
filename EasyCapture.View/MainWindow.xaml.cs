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
        }

        private void WindowStartMainPanel_SettingButtonClick(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("イベントが発生しました。");
            this.WindowStartMainPanel.Visibility = Visibility.Hidden;
            this.WindowSettingMainPanel.Visibility = Visibility.Visible;
        }
    }
}
