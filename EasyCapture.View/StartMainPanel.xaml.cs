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
    /// StartMainPanel.xaml の相互作用ロジック
    /// </summary>
    public partial class StartMainPanel : UserControl
    {
        public StartMainPanel()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 「領域を指定してキャプチャ」がクリックされたときに発生します。
        /// </summary>
        public event RoutedEventHandler RectCaptureButtonClick
        {
            add { this.RectCaptureButton.Click += value; }
            remove { this.RectCaptureButton.Click -= value; }
        }

        /// <summary>
        /// 「ウィンドウを選択してキャプチャ」がクリックされたときに発生します。
        /// </summary>
        public event RoutedEventHandler WindowCaptureButtonClick
        {
            add { this.WindowCaptureButton.Click += value; }
            remove { this.WindowCaptureButton.Click -= value; }
        }

        /// <summary>
        /// 「キャプチャ設定」がクリックされたときに発生します。
        /// </summary>
        public event RoutedEventHandler SettingButtonClick
        {
            add { this.SettingButton.Click += value; }
            remove { this.SettingButton.Click -= value; }
        }

        /// <summary>
        /// 「終了」がクリックされたときに発生します。
        /// </summary>
        public event RoutedEventHandler ExitButtonClick
        {
            add { this.ExitButton.Click += value; }
            remove { this.ExitButton.Click -= value; }
        }
    }
}
