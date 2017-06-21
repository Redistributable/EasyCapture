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
            Func<WindowInfo, bool> check = item =>
                 item.width >= 2 &&
                 item.height >= 2 &&
                 item.positionX >= 0 &&
                 item.positionY >= 0;

            var result = GetAllWindows().Where(check).ToArray();
            foreach (var item in result)
                item.children = new List<WindowInfo>(item.children.Where(check).ToArray());

            return result;
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




                    // 子ウィンドウの情報取得
                    List<WindowInfo> childWindows = new List<WindowInfo>();
                    win32.EnumChildWindows(hWnd, new win32.EnumWindowsDelegate((childHWnd, childLParam) =>
                    {
                        int childTitleLength = win32.GetWindowTextLength(childHWnd);
                        StringBuilder child_sb_title = new StringBuilder("unknown");

                        if (0 < childTitleLength)
                        {
                            // タイトル
                            child_sb_title = new StringBuilder(childTitleLength + 1);
                            win32.GetWindowText(childHWnd, child_sb_title, child_sb_title.Capacity);
                        }

                        // クラス名
                        StringBuilder child_sb_class = new StringBuilder(1024);
                        win32.GetClassName(childHWnd, child_sb_class, child_sb_class.Capacity);
                        if (child_sb_title.ToString() == "unknown")
                            child_sb_title = new StringBuilder("unknown (" + child_sb_class.ToString() + ")");

                        // 位置と大きさ
                        win32.RECT childRect = new win32.RECT();
                        win32.GetWindowRect(childHWnd, ref childRect);


                        // 孫ウィンドウの情報取得
                        List<WindowInfo> gs1Windows = new List<WindowInfo>();
                        /*
                        win32.EnumChildWindows(childHWnd, new win32.EnumWindowsDelegate((gs1HWnd, gs1LParam) =>
                        {
                            int gs1TitleLength = win32.GetWindowTextLength(childHWnd);
                            StringBuilder gs1_sb_title = new StringBuilder("unknown");

                            if (0 < gs1TitleLength)
                            {
                                // タイトル
                                gs1_sb_title = new StringBuilder(gs1TitleLength + 1);
                                win32.GetWindowText(gs1HWnd, gs1_sb_title, gs1_sb_title.Capacity);
                            }

                            // クラス名
                            StringBuilder gs1_sb_class = new StringBuilder(1024);
                            win32.GetClassName(gs1HWnd, gs1_sb_class, gs1_sb_class.Capacity);

                            // 位置と大きさ
                            win32.RECT gs1Rect = new win32.RECT();
                            win32.GetWindowRect(gs1HWnd, ref gs1Rect);

                            gs1Windows.Add(new WindowInfo(
                                gs1_sb_title.ToString(),
                                gs1HWnd,
                                gs1Rect.left,
                                gs1Rect.top,
                                gs1Rect.right - gs1Rect.left,
                                gs1Rect.bottom - gs1Rect.top,
                                gs1Windows));

                            return true;
                        }), IntPtr.Zero);
                        */


                        childWindows.Add(new WindowInfo(
                            child_sb_title.ToString(),
                            childHWnd,
                            childRect.left,
                            childRect.top,
                            childRect.right - childRect.left,
                            childRect.bottom - childRect.top,
                            gs1Windows));
                        
                        return true;
                    }), IntPtr.Zero);


                    windows.Add(new WindowInfo(
                        sb_title.ToString(),
                        hWnd,
                        rect.left,
                        rect.top,
                        rect.right - rect.left,
                        rect.bottom - rect.top,
                        childWindows));
                }

                return true;
            }), IntPtr.Zero);

            return windows;
        }
    }

    
}
