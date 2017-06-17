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

            // イベントの初期化
            this.RectCaptureButtonClick = delegate { };
            this.WindowCaptureButtonClick = delegate { };
            this.SettingButtonClick = delegate { };
            this.ExitButtonClick = delegate { };

            this.RectCaptureButton.Click += this.RectCaptureButtonClick;
            this.WindowCaptureButton.Click += this.WindowCaptureButtonClick;
            this.SettingButton.Click += this.SettingButtonClick;
            this.ExitButton.Click += this.ExitButtonClick;
        }


        /// <summary>
        /// 「領域を指定してキャプチャ」がクリックされたときに発生します。
        /// </summary>
        public event RoutedEventHandler RectCaptureButtonClick;

        /// <summary>
        /// 「ウィンドウを選択してキャプチャ」がクリックされたときに発生します。
        /// </summary>
        public event RoutedEventHandler WindowCaptureButtonClick;

        /// <summary>
        /// 「キャプチャ設定」がクリックされたときに発生します。
        /// </summary>
        public event RoutedEventHandler SettingButtonClick;

        /// <summary>
        /// 「終了」がクリックされたときに発生します。
        /// </summary>
        public event RoutedEventHandler ExitButtonClick;
    }
}
