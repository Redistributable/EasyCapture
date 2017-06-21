using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using win32 = Redefinable.Applications.EasyCapture.Models.WindowInfoInteropMethods;


namespace Redefinable.Applications.EasyCapture.Models
{
    public class WindowInfo
    {
        // 非公開フィールド
        private string name;
        private IntPtr handle;

        private int positionX;
        private int positionY;
        private int width;
        private int height;

        private List<WindowInfo> children;


        // 公開プロパティ

        public string Name
        {
            get { return this.name; }
        }

        public IntPtr Handle
        {
            get { return this.handle; }
        }

        public int PositionX
        {
            get { return this.positionX; }
        }

        public int PositionY
        {
            get { return this.positionY; }
        }

        public int Width
        {
            get { return this.width; }
        }

        public int Height
        {
            get { return this.height; }
        }


        public ICollection<WindowInfo> Children
        {
            get { return this.children; }
        }


        // コンストラクタ
        
        public WindowInfo(string name, IntPtr handle, int positionX, int positionY, int width, int height, ICollection<WindowInfo> children)
        {
            this.name = name;
            this.handle = handle;
            this.positionX = positionX;
            this.positionY = positionY;
            this.width = width;
            this.height = height;

            this.children = new List<WindowInfo>();
            this.children.AddRange(children);
        }
        

        // 静的メソッド

        public static ICollection<WindowInfo> GetAllActiveWindows()
        {
            return GetAllWindows().Where(item => (item.width * item.height >= 2) && (item.positionX >= 0) && (item.positionY >= 0)).ToArray();
        }

        public static ICollection<WindowInfo> GetAllWindows()
        {
            List<WindowInfo> windows = new List<WindowInfo>();
            win32.EnumWindows(new win32.EnumWindowsDelegate((hWnd, lparam) =>
            {
                int titleLength = win32.GetWindowTextLength(hWnd);
                if (0 < titleLength)
                {
                    // タイトル
                    StringBuilder sb_title = new StringBuilder(titleLength + 1);
                    win32.GetWindowText(hWnd, sb_title, sb_title.Capacity);

                    // クラス名
                    StringBuilder sb_class = new StringBuilder(1024);
                    win32.GetClassName(hWnd, sb_class, sb_class.Capacity);

                    // 位置と大きさ
                    win32.RECT rect = new win32.RECT();
                    win32.GetWindowRect(hWnd, ref rect);


                    windows.Add(new WindowInfo(
                        sb_title.ToString(),
                        hWnd,
                        rect.left,
                        rect.top,
                        rect.right - rect.left,
                        rect.bottom - rect.top,
                        new WindowInfo[0]));
                }

                return true;
            }), IntPtr.Zero);

            return windows;
        }
    }

    
}
