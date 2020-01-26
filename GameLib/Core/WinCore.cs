using System;
using System.Collections.Generic;
using System.Diagnostics;

using GameLib.Models;

using SharpDX;
using SharpDX.DXGI;
using SharpDX.Direct2D1;
using SharpDX.DirectWrite;
using SharpDX.Windows;

namespace GameLib.Core
{
    public class WinCore : ICore
    {
        public RenderForm mainRenderForm;
        public WindowRenderTarget RenderTarget = null;  // 
        protected SharpDX.Direct2D1.Factory Factory = null; //
        protected RenderLoop.RenderCallback callback;
        protected Camera2D cam2d = new Camera2D(0, 0);

        protected double fps = 0;
        protected Stopwatch gameClock;
        protected double gameTime = 0;
        protected int gameFrameCount = 0;
        protected double ElapsedTime;
        protected bool pause = false;

        protected RenderTargetProperties rndTargetProperties;
        protected HwndRenderTargetProperties hwndTargetProperties;

        public Level Level;
        public UI userinterface;
        public bool Debug = true;
        public bool EndGame;
        public int MapSizeK = 4;
        public bool OnRealMatrix = false;
        public string MatrixPath;
        public byte[,] mapMatrix;
        public int MatrWidth;
        public int MatrHeight;

        public WinCore() 
        {
        }


        public void ShowForm() 
        {
            callback = new RenderLoop.RenderCallback(Render);
            RenderLoop.Run(mainRenderForm, callback);
        }

        public virtual void initForm(string formtitle)
        {
            mainRenderForm = new RenderForm(formtitle);

            Factory = new SharpDX.Direct2D1.Factory(SharpDX.Direct2D1.FactoryType.SingleThreaded);
            mainRenderForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            mainRenderForm.Width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size.Width;
            mainRenderForm.Height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size.Height;
            MatrWidth = mainRenderForm.Width * MapSizeK;
            MatrHeight = mainRenderForm.Height * MapSizeK;

            mapMatrix = new byte[MatrHeight, MatrWidth];
            if (OnRealMatrix == true) MatrixPath.LoadMTRX(ref mapMatrix, MatrWidth, MatrHeight);
            mainRenderForm.ShowIcon = false;
            gameClock = Stopwatch.StartNew();

            //
            rndTargetProperties = new RenderTargetProperties(new PixelFormat(Format.B8G8R8A8_UNorm, SharpDX.Direct2D1.AlphaMode.Premultiplied));
            hwndTargetProperties = new HwndRenderTargetProperties();

            //
            if (Level == null) Level = new Level();
            hwndTargetProperties.Hwnd = mainRenderForm.Handle;
            hwndTargetProperties.PixelSize = new Size2(mainRenderForm.Width, mainRenderForm.Height);
            hwndTargetProperties.PresentOptions = PresentOptions.None;

            RenderTarget = new WindowRenderTarget(Factory, rndTargetProperties, hwndTargetProperties);
            mainRenderForm.Show();
        }

        public virtual void InitObjects() { }
        public virtual void InitBinds() { }
        public virtual void GameLogic() { }

        private void Render()
        {
            if (pause) return;

            RenderTarget.BeginDraw();
            RenderTarget.Clear(userinterface.mainFormBackgroundColor);

            GameLogic();
            RenderTarget.EndDraw();
        }


        public void Pause() 
        {
            pause = !pause;
        }
    }
}
