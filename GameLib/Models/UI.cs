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
        #endregion

        public string message = "This is test message" +
            "Hello world";

        // Colors
        public RawColor4 mainFormBackgroundColor;
        
        // Brushes
        public SolidColorBrush blackBrush;
        
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
        }
        public void InitUI()
        {
            fpsTextBox = new RawRectangleF(camera2.camPos.X, 32, camera2.camPos.X + 200, 4);
            MessageTextBox = new RawRectangleF(camera2.camPos.X + 400, 100, camera2.camPos.X + 700, 4);
            BuildRect = new RawRectangleF(0,0,0,0);
        }
        public void InitTFormats()
        {
            textFormat = new TextFormat(new SharpDX.DirectWrite.Factory(), "Tahoma", 15) 
                        { TextAlignment = TextAlignment.Leading, ParagraphAlignment = ParagraphAlignment.Center };
        }
        public void SetUIPos() 
        {
            fpsTextBox.Left = camera2.camPos.X;
            fpsTextBox.Right = camera2.camPos.X + 200;
            if (message != null)
            {
                MessageTextBox.Left = camera2.camPos.X + 400;
                MessageTextBox.Right = camera2.camPos.X + 700;
            }
        }
        public void SetMessageText(string mes = null) 
        {
            message = mes;
        }
    }
}
