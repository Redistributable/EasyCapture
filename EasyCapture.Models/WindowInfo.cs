using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Redefinable.Applications.EasyCapture.Models
{
    public class WindowInfo
    {
        // 非公開フィールド
        private string name;
        private int handle;

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

        public int Handle
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
    }
}
