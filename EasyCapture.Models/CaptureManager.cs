using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using EmergenceGuardian.FFmpeg;


namespace Redefinable.Applications.EasyCapture.Models
{
    public class CaptureManager
    {
        // 非公開フィールド
        private CaptureManagerRectangle currentRectangle;
        private CaptureManagerOutputMode outputMode;

        private CaptureManagerState state;

        private Thread captureThread;
        private bool captureExitFlag;


        // 公開プロパティ
        
        public CaptureManagerRectangle CurrentRectangle
        {
            get { return this.currentRectangle; }
        }

        public CaptureManagerOutputMode OutputMode
        {
            get { return this.outputMode; }
        }

        public CaptureManagerState State
        {
            get { return this.state; }
        }

        public int PositionX
        {
            get { return this.currentRectangle.X; }
            set { this.currentRectangle.X = value; }
        }

        public int PositionY
        {
            get { return this.currentRectangle.Y; }
            set { this.currentRectangle.Y = value; }
        }


        // コンストラクタ

        public CaptureManager(CaptureManagerRectangle rectangle, CaptureManagerOutputMode outputMode)
        {
            this.currentRectangle = rectangle;
            this.outputMode = outputMode;
            this.state = CaptureManagerState.Ready;
            this.captureThread = new Thread(new ParameterizedThreadStart(this.captureProc));
            this.captureExitFlag = false;

            this.currentRectangle.ValueChanged += CurrentRectangle_ValueChanged;
        }


        // 非公開メソッド
        
        private void CurrentRectangle_ValueChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void captureProc(Object param)
        {
            if (param is CaptureProcParameter == false)
                throw new InvalidOperationException("captureProcのパラメータには、CaptureProcParameter型のインスタンスを指定してください。");
        }
        


        // 公開メソッド

        public void StartCapture()
        {
            if (this.captureThread.IsAlive)
                throw new InvalidOperationException("キャプチャは現在実行中です。");

            this.captureExitFlag = false;
            this.captureThread.Start();
        }

        public Task<ConvertResult> ExitCapture()
        {
            if (!this.captureThread.IsAlive)
                throw new InvalidOperationException("キャプチャが開始していません。");

            this.captureExitFlag = true;
            while (this.captureThread.IsAlive)
                Thread.Sleep(1);


        }


        // 内部クラス

        class CaptureProcParameter
        {

        }
    }

    public struct CaptureManagerRectangle
    {
        // 非公開フィールド
        private int x;
        private int y;
        private int width;
        private int height;


        // 非公開イベント
        
        private event EventHandler valueChanged;


        // 公開イベント

        /// <summary>
        /// 値が変更されたときに発生します。
        /// </summary>
        public event EventHandler ValueChanged
        {
            add { this.valueChanged += value; }
            remove { this.valueChanged -= value; }
        }


        // 公開プロパティ

        public int X
        {
            get { return this.x; }
            set { this.x = value; }
        }

        public int Y
        {
            get { return this.y; }
            set { this.y = value; }
        }

        public int Width
        {
            get { return this.width; }
            set { this.width = value; }
        }

        public int Height
        {
            get { return this.height; }
            set { this.height = value; }
        }


        // コンストラクタ

        /// <summary>
        /// 新しいCaptureManagerRectangle構造体を初期化します。
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public CaptureManagerRectangle(int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.valueChanged = delegate { };
        }
    }

    public enum CaptureManagerOutputMode
    {
        /// <summary>
        /// 動画
        /// </summary>
        Movie,

        /// <summary>
        /// 動画 (音声なし)
        /// </summary>
        MovieWithOutSound,

        /// <summary>
        /// 連続静止画
        /// </summary>
        Picture,
        
        /// <summary>
        /// 音声のみ
        /// </summary>
        SoundOnly
    }

    public enum CaptureManagerState
    {
        /// <summary>
        /// キャプチャは実行可能です。
        /// </summary>
        Ready,

        /// <summary>
        /// 実行中です。
        /// </summary>
        Alive,

        /// <summary>
        /// その他の状態です。
        /// </summary>
        Other
    }

    public class CaptureManagerConvertResult
    {

    }
}
