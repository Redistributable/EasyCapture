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
    /// TopPanel.xaml の相互作用ロジック
    /// </summary>
    public partial class TopPanel : UserControl
    {
        public TopPanel()
        {
            InitializeComponent();
        }


        public string Text
        {
            get { return (string) this.PageTitle.Content; }
            set { this.PageTitle.Content = value; }
        }
    }
}
