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
    /// RectCaptureMainPanel.xaml の相互作用ロジック
    /// </summary>
    public partial class RectCaptureMainPanel : UserControl
    {
        public RectCaptureMainPanel()
        {
            InitializeComponent();

            this.SelectToolButton.Click += SelectToolButton_Click;
        }

        private void SelectToolButton_Click(object sender, RoutedEventArgs e)
        {
            RectCaptureSelectToolWindow tWin = new RectCaptureSelectToolWindow();
            var result = tWin.ShowSelectWindow();

            this.PositionXTextBox.Text = result.Rectangle.X.ToString();
            this.PositionYTextBox.Text = result.Rectangle.Y.ToString();
            this.WidthTextBox.Text = result.Rectangle.Width.ToString();
            this.HeightTextBox.Text = result.Rectangle.Height.ToString();
        }
    }
}
