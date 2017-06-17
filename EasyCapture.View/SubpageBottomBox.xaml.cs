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
    /// SubpageBottomBox.xaml の相互作用ロジック
    /// </summary>
    public partial class SubpageBottomBox : UserControl
    {
        public SubpageBottomBox()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 「戻る」ボタンがクリックされたときに発生します。
        /// </summary>
        public event RoutedEventHandler PrevButtonClick
        {
            add { this.PrevButton.Click += value; }
            remove { this.PrevButton.Click -= value; }
        }

        /// <summary>
        /// 「終了」ボタンがクリックされたときに発生します。
        /// </summary>
        public event RoutedEventHandler ExitButtonClick
        {
            add { this.ExitButton.Click += value; }
            remove { this.ExitButton.Click -= value; }
        }
    }
}
