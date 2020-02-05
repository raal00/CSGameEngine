using SharpDX;
using System;

using SharpDX.Mathematics.Interop;
using SharpDX.DirectWrite;
using SharpDX.Direct2D1;

namespace GameLib.Models
{
    public class UI
    {
        private readonly Camera2D camera2;
        // UI Components
        public RawRectangleF fpsTextBox;
        public RawRectangleF MessageTextBox;

        #region debugUI
        public RawRectangleF BuildRect;
        public RawRectangleF BuildTextureRect;
        public RawRectangleF BuildParamsRect;
        #endregion

        public string GameMes   = "";
        public string HeroesMes = "";


        // Colors
        public RawColor4 mainFormBackgroundColor;
        
        // Brushes
        public SolidColorBrush blackBrush;
        public SolidColorBrush whiteBrush;
        public SolidColorBrush redBrush;
        // Text formats
        public TextFormat textFormat;

        public UI(WindowRenderTarget RenderTarget, Camera2D cam) 
        {
            camera2 = cam;
            InitColors();
            InitBrushes(RenderTarget);
            InitTFormats();
            InitUI();
        }

        public void InitColors() 
        {
            mainFormBackgroundColor = new SharpDX.Mathematics.Interop.RawColor4(255, 255, 255, 1);
        }
        public void InitBrushes(WindowRenderTarget RenderTarget)
        {
            blackBrush = new SolidColorBrush(RenderTarget, SharpDX.Color.Black);
            whiteBrush = new SolidColorBrush(RenderTarget, SharpDX.Color.White);
            redBrush = new SolidColorBrush(RenderTarget, SharpDX.Color.DarkRed);
        }
        public void InitUI()
        {
            fpsTextBox = new RawRectangleF(camera2.camPos.X, 32, camera2.camPos.X + 200, 4);
            MessageTextBox = new RawRectangleF(camera2.camPos.X + 400, 100, camera2.camPos.X + 700, 4);
            BuildRect = new RawRectangleF(0,0,0,0);
            BuildTextureRect = new RawRectangleF(0, 32, 0, 64);
            BuildParamsRect = new RawRectangleF(0, 32, 0, 64);
        }
        public void InitTFormats()
        {
            textFormat = new TextFormat(new SharpDX.DirectWrite.Factory(), "Calibri", FontWeight.Bold, FontStyle.Italic, FontStretch.Medium, 18) 
                        { TextAlignment = TextAlignment.Leading, ParagraphAlignment = ParagraphAlignment.Center};
            
        }
        public void SetUIPos() 
        {
            fpsTextBox.Top = camera2.camPos.Y + 5;
            fpsTextBox.Bottom = camera2.camPos.Y + 35;
            fpsTextBox.Left = camera2.camPos.X;
            fpsTextBox.Right = camera2.camPos.X + 200;

            if (GameMes != null)
            {
                MessageTextBox.Top = camera2.camPos.Y + 5;
                MessageTextBox.Bottom = camera2.camPos.Y + 105;
                MessageTextBox.Left = camera2.camPos.X + 650;
                MessageTextBox.Right = camera2.camPos.X + 850;
            }
        }
        public void SetUIPos_Debug() 
        {
            BuildTextureRect.Top = camera2.camPos.Y + 5;
            BuildTextureRect.Bottom = camera2.camPos.Y + 55;
            BuildTextureRect.Left = camera2.camPos.X + 300;
            BuildTextureRect.Right = camera2.camPos.X + 350;

            BuildParamsRect.Top = camera2.camPos.Y + 5;
            BuildParamsRect.Bottom = camera2.camPos.Y + 205;
            BuildParamsRect.Left = camera2.camPos.X  + 360;
            BuildParamsRect.Right = camera2.camPos.X + 640;
        }
        public void SetMessageText(string mes = null) 
        {
            GameMes = mes;
        }
    }
}
