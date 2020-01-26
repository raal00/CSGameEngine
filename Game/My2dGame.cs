using GameLib.Interaction;
using GameLib;
using GameLib.Core;
using GameLib.Enums;
using GameLib.Models;
using GameLib.Transform;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using SharpDX.Direct2D1;

namespace Game
{
    public sealed partial class My2dGame : WinCore, IObserverable
    {
        /*
         0000000000000000000
         0000011110000000000
         1111111111111100011    
             */
        private StatusGame statusGame;
        private ObjConstructorTag objConstructorTag;
        private List<IObserver> observers;

        #region DEBUG
        Size oSize = new Size(128, 128);
        Bitmap[] buildTextures;
        string[] buildTextures_Names;
        int numberOfWallTexture = 0;
   
        GameObject player1;
        Barrier background;
        bool breakable = false;
        bool hideOnApproach = false;
        bool hasPhisCollider = true;
        string BuildParamString;
        #endregion

        Vector2 Move = new Vector2(0,0);

        SharpDX.Vector2 camMove;
        GameObject controlledHero;


        public My2dGame()
        {
            EndGame = false;
            observers = new List<IObserver>();
        }

        private void OnMenuPauseClick(object sender, EventArgs e)
        {
            Pause();
        }

        private void OnMenuExitClick(object sender, EventArgs e)
        {
            EndGame = true;
            Notify(1);
            mainRenderForm.Close();
        }
        private void OnMenuSaveClick(object sender, EventArgs e)
        {
            Notify(2);
        }

        public override void InitBinds()
        {
            if (statusGame == StatusGame.Debug)
            {
                mainRenderForm.MouseClick += MainRenderForm_MouseClick_Debug;
                mainRenderForm.KeyDown += MainRenderForm_KeyDown_Debug;
                mainRenderForm.MouseWheel += MainRenderForm_MouseWheel_Debug;
            }
            else if (statusGame == StatusGame.Release) 
            {
                mainRenderForm.KeyDown += MainRenderForm_KeyDown_release;
                mainRenderForm.KeyUp += MainRenderForm_KeyUp_release;
                mainRenderForm.MouseClick += MainRenderForm_MouseClick_Release; 
            }
            mainRenderForm.MouseMove += MainRenderForm_MouseMove;

            userinterface.SetUIPos();
            if (Debug == true) userinterface.SetUIPos_Debug();
            RenderTarget.Transform = cam2d.GetTransform3x2();
        }

        private void MainRenderForm_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (GetStartPos == true) 
            {
                userinterface.BuildRect.Right = e.Location.X + cam2d.camPos.X;
                userinterface.BuildRect.Bottom = e.Location.Y + cam2d.camPos.Y;
            }
        }
        void breakWall(Barrier barrier) 
        {
            Level.DeleteStatus = true;
            Level.ForDeleting = barrier;
            for (int i = (int)barrier.Collider.Top; i < (int)barrier.Collider.Bottom; i++) 
            {
                for (int j = (int)barrier.Collider.Left; j < (int)barrier.Collider.Right; j++) 
                {
                    mapMatrix[i, j] = 0;
                }
            }
        }


        /// <summary>
        /// Основной алгоритм игры
        /// </summary>
        /// 
        public override void GameLogic()
        {
            // draw background
            RenderTarget.DrawBitmap(Level.BackGround.Texture, Level.BackGround.Collider, 1, SharpDX.Direct2D1.BitmapInterpolationMode.NearestNeighbor);
            // draw objects
            if (Level.DeleteStatus == true && Level.ForDeleting != null) 
            {
                switch (Level.ForDeleting.objectTag) 
                {
                    case Tag.ENVIRONMENT:
                        Level.WallsOnScene.Remove((Barrier)Level.ForDeleting);
                        break;
                    case Tag.HERO:
                        Level.ObjectsOnScene.Remove((GameObject)Level.ForDeleting);
                        break;
                    default: break;
                }
                Level.DeleteStatus = false;
            }
            foreach (var obj in Level.ObjectsOnScene)
            {
                if (obj.visible) RenderTarget.DrawBitmap(obj.Texture, obj.Collider, 1, SharpDX.Direct2D1.BitmapInterpolationMode.NearestNeighbor);
            }
            // draw walls
            foreach (var obj in Level.WallsOnScene)
            {
                if (obj.visible == true)
                RenderTarget.DrawBitmap(obj.Texture, obj.Collider, 1, SharpDX.Direct2D1.BitmapInterpolationMode.NearestNeighbor);
            }

            if (statusGame == StatusGame.Debug)
            {
                if (GetStartPos == true)
                {
                    RenderTarget.DrawRectangle(userinterface.BuildRect, userinterface.redBrush);
                }
                if (buildTextures.Length > 0)
                {
                    RenderTarget.DrawBitmap(buildTextures[numberOfWallTexture], userinterface.BuildTextureRect, 1, BitmapInterpolationMode.NearestNeighbor);
                    if (breakable) BuildParamString = "Разбиваемый";
                    else BuildParamString = "Не разбиваемый";
                    if (hideOnApproach) BuildParamString += "\nСкрыть при приближении";
                    else BuildParamString += "\nНе скрывать";
                    if (hasPhisCollider) BuildParamString += "\nС физ. оболочкой";
                    else BuildParamString += "\nБез физ. оболочки";
                    RenderTarget.DrawText(BuildParamString, userinterface.textFormat, userinterface.BuildParamsRect, userinterface.redBrush);
                }
            }
            
            RenderTarget.DrawText(userinterface.message, userinterface.textFormat, userinterface.MessageTextBox, userinterface.redBrush);
            #region fps
            RenderTarget.DrawText($"{(int)fps} fps", userinterface.textFormat, userinterface.fpsTextBox, userinterface.blackBrush);
            gameFrameCount++;
            ElapsedTime = (double)gameClock.ElapsedTicks / Stopwatch.Frequency;
            gameTime += ElapsedTime;
            if (gameTime >= 1f)
            {
                fps = gameFrameCount / gameTime;
                gameFrameCount = 0;
                gameTime = 0;
            }
            gameClock.Restart();
            #endregion
        }

        public void AddObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void Notify(int status)
        {
            foreach (var observer in observers) 
            {
                observer.Action(status);
            }
        }
    }
}
