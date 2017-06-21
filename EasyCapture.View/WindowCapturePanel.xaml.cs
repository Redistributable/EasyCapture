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


        // 公開プロパティ




        // コンストラクタ

        /// <summary>
        /// 新しいWindowCapturePanelクラスのインスタンスを初期化子ます。
        /// </summary>
        public WindowCapturePanel()
        {
            InitializeComponent();

            this.UpdateWindowInformations();
            this.WindowsComboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// 現在のウィンドウ情報を取得し、コンボボックスを更新します。
        /// </summary>
        public void UpdateWindowInformations()
        {
            this.WindowsComboBox.Items.Clear();
            
            this.windowInformations = WindowInfo.GetAllActiveWindows();
            foreach (var item in this.windowInformations)
                this.WindowsComboBox.Items.Add(new WindowItemInfo(item));
        }


        class WindowItemInfo
        {
            // 非公開フィールド
            private WindowInfo windowInfo;


            // 公開プロパティ
            
            public WindowInfo WindowInfo
            {
                get { return this.windowInfo; }
            }


            // コンストラクタ

            public WindowItemInfo(WindowInfo windowInfo)
            {
                this.windowInfo = windowInfo;
            }


            // 公開メソッド

            public override string ToString()
            {
                string name = this.windowInfo.Name;
                int max = 40;

                if (name.Length >= max)
                    name = name.Substring(0, max) + " ...";

                return name;
            }
        }
    }
}
