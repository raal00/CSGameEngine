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
using System.Windows.Forms;
using GameLib.Params;

namespace Game
{
    public sealed partial class My2dGame : WinCore, IObserverable
    {
        private StatusGame statusGame; // debug/release
        private ObjConstructorTag objConstructorTag; // add character/ add object
        private List<IObserver> observers;
        private Dictionary<Keys, bool> keys; // dict of pressed keys

        #region DEBUG
        Size CharacterSize = new Size(100, 100); // размер добаляемых персонажей
        Bitmap[] buildTextures;         // текстуры для объектов
        string[] buildTextures_Names;   // пути к файлам текстур
        int numberOfWallTexture = 0;    // номер тек. текстуры
   
        GameObject player1;
        Barrier background;
        bool breakable = false;      // флаг разрушаемости объекта
        bool hideOnApproach = false; // флаг сокрытия объекта при сближении
        bool hasPhisCollider = true; // флаг существования физ. оболочки
        bool Visible = true;         // флаг видимости объекта
        string BuildParamString;     // Вывод состояний флагов
        #endregion

         // вектор движения героя
        GameObject controlledHero;


        public My2dGame()
        {
            EndGame = false;
            observers = new List<IObserver>();
            keys = new Dictionary<Keys, bool>();
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
            RenderTarget.Transform = MapValues.cam2d.GetTransform3x2();
        }

        private void MainRenderForm_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (GetStartPos == true) 
            {
                userinterface.BuildRect.Right = e.Location.X + MapValues.cam2d.camPos.X;
                userinterface.BuildRect.Bottom = e.Location.Y + MapValues.cam2d.camPos.Y;
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
                    MapValues.mapMatrix[i, j] = 0;
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
                if (obj.HideOnApproach == true)
                {
                    // NEED TO OPTIMIZING
                    if ((obj.Collider.Left < controlledHero.Collider.Left) &&
                        (obj.Collider.Right > controlledHero.Collider.Right) &&
                        (obj.Collider.Top < controlledHero.Collider.Top) &&
                        (obj.Collider.Bottom > controlledHero.Collider.Bottom))
                    {
                        obj.visible = false;
                    }
                    else 
                    {
                        obj.visible = true;
                    }
                }
                if (obj.visible == true)
                {   
                    RenderTarget.DrawBitmap(obj.Texture, obj.Collider, 1, SharpDX.Direct2D1.BitmapInterpolationMode.NearestNeighbor);
                }
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
                    // NEED TO OPTIMIZING
                    BuildParamString = $"Разбиваемый: {breakable}\n" +
                                       $"Скрыть при приближении: {hideOnApproach}\n" +
                                       $"С физ. оболочкой: {hasPhisCollider}\n" +
                                       $"Видимый: {Visible}";
                    RenderTarget.DrawText(BuildParamString, userinterface.textFormat, userinterface.BuildParamsRect, userinterface.redBrush);
                }
            }
            
            RenderTarget.DrawText(userinterface.GameMes, userinterface.textFormat, userinterface.MessageTextBox, userinterface.redBrush);
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
