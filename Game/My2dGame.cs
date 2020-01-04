using GameLib;
using GameLib.Core;
using GameLib.Enums;
using GameLib.Models;
using GameLib.Transform;
using System;
using System.Diagnostics;
using System.IO;

namespace Game
{
    public sealed partial class My2dGame : WinCore 
    {
        /*
         0000000000000000000
         0000011110000000000
         1111111111111100011    
             */
        private int floorPosY;
        private StatusGame statusGame;
        private ObjConstructorTag objConstructorTag;

        #region DEBUG
        Size oSize = new Size(128, 128);
        GameObject player1;
        Barrier floor;
        Barrier background;
        #endregion

        Vector2 Jump = new Vector2(0, 50);
        Vector2 Right = new Vector2(20, 0);
        Vector2 Left = new Vector2(-20, 0);

        SharpDX.Vector2 camStartPos;
        SharpDX.Vector2 camNewPos;
        SharpDX.Vector2 camMove;
        GameObject controlledHero;


        public My2dGame()
        {
        }


        /// <summary>
        /// Добавление объектов на сцену
        /// </summary>
        public override void InitObjects()
        {
            // set camera
            RenderTarget.Transform = cam2d.GetTransform3x2();
            camStartPos = new SharpDX.Vector2(0, 0);
            camNewPos = new SharpDX.Vector2(0, 0);
            camMove = new SharpDX.Vector2(0, 0);

            // set UI
            if (userinterface == null) userinterface = new UI(RenderTarget, cam2d);

            if (Debug)
            {
                statusGame = StatusGame.Debug;
                objConstructorTag = ObjConstructorTag.Barrier;
                // set floor
                floorPosY = mainRenderForm.Height - 200;
                for (int i = floorPosY; i < MatrHeight; i++)
                {
                    for (int j = 0; j < MatrWidth; j++)
                    {
                        mapMatrix[i, j] = 1;
                    }
                }
                player1 = new GameObject("player 1");
                player1.objectTag = Tag.CONTROLLEDHERO;
                floor = new Barrier();
                background = new Barrier();

                floor.Collider = new SharpDX.Mathematics.Interop.RawRectangleF(0, mainRenderForm.Height - 200, mainRenderForm.Width * 2, mainRenderForm.Height);
                floor.texturePath = Strings.TexturePath + "floor.jpg";
                floor.SetTexture(RenderTarget);
                floor.objectTag = Tag.ENVIRONMENT;

                background.Collider = new SharpDX.Mathematics.Interop.RawRectangleF(0, 0, mainRenderForm.Width * 2, mainRenderForm.Height-200);
                background.texturePath = Strings.TexturePath + "back.jpg";
                background.SetTexture(RenderTarget);
                background.objectTag = Tag.ENVIRONMENT;

                player1.texturePath = Strings.TexturePath + "texture1.png";
                player1.Move(new GameLib.Transform.Vector2(200, -200));
                player1.SetTexture(RenderTarget);

                Level.BackGround = background;
                Level.Floor = floor;
                Level.ObjectsOnScene.Add(player1);

                controlledHero = player1;
            }
            else 
            {
                statusGame = StatusGame.Release;
                foreach (var obj in Level.ObjectsOnScene)
                {
                    obj.SetTexture(RenderTarget);
                    if (obj.objectTag == Tag.CONTROLLEDHERO) controlledHero = obj;
                   // Console.WriteLine(obj.Collider.Right);
                }
                if (controlledHero == null)
                {
                    controlledHero = new GameObject();
                    controlledHero.texturePath = Strings.TexturePath + "texture1.png";
                    controlledHero.Move(new GameLib.Transform.Vector2(200, -200));
                    controlledHero.SetTexture(RenderTarget);
                }
                controlledHero.animationController = new AnimationController(Strings.AnimationControllerPath + "Hero/", controlledHero, RenderTarget);
                controlledHero.animationController.AnimIdle_Run();

                foreach (var obj in Level.WallsOnScene) 
                {
                    obj.SetTexture(RenderTarget);
                }
                Level.Floor.SetTexture(RenderTarget);
                Level.BackGround.SetTexture(RenderTarget);
            }
        }

        public override void InitBinds()
        {
            if (statusGame == StatusGame.Debug)
            {
                mainRenderForm.MouseClick += MainRenderForm_MouseClick_Debug;
                mainRenderForm.KeyDown += MainRenderForm_KeyDown_Debug;
            }
            else if (statusGame == StatusGame.Release) 
            {
                mainRenderForm.KeyDown += MainRenderForm_KeyDown_release;
                mainRenderForm.MouseClick += MainRenderForm_MouseClick_Release; 
            }
            mainRenderForm.MouseMove += MainRenderForm_MouseMove;
        }

        private void MainRenderForm_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (GetStartPos == true) 
            {
                userinterface.BuildRect.Right = e.Location.X * 2;
                userinterface.BuildRect.Bottom = e.Location.Y;
            }
            camNewPos.X = e.Location.X;
            camMove.X = camNewPos.X - camStartPos.X;
            cam2d.MoveCamera(camMove);
            userinterface.SetUIPos();
            RenderTarget.Transform = cam2d.GetTransform3x2();
            camStartPos = camNewPos;
        }



        /// <summary>
        /// Основной алгоритм игры
        /// </summary>
        /// 
        public override void GameLogic()
        {
            // draw floor
            RenderTarget.DrawBitmap(Level.Floor.Texture, Level.Floor.Collider, 1, SharpDX.Direct2D1.BitmapInterpolationMode.NearestNeighbor);
            // draw background
            RenderTarget.DrawBitmap(Level.BackGround.Texture, Level.BackGround.Collider, 1, SharpDX.Direct2D1.BitmapInterpolationMode.NearestNeighbor);
            // draw objects
            foreach (var obj in Level.ObjectsOnScene)
            {
                if (obj.position.Y + 20 >= MatrHeight) obj.visible = false;
                else obj.visible = true;

                if (obj.visible &&
                        (mapMatrix[(int)(obj.Collider.Bottom - obj.phisics.grav.Y), (int)(obj.Collider.Left + 20)] == 0) &&
                        (mapMatrix[(int)(obj.Collider.Bottom - obj.phisics.grav.Y), (int)(obj.Collider.Right - 20)] == 0))
                {
                    obj.OnGround = false;
                    obj.TicsInTheAir++;
                    obj.phisics.grav.Y = -2 - obj.TicsInTheAir / 32;
                    obj.Move(obj.phisics.grav);
                }
                else 
                {
                    if (obj.TicsInTheAir > 64) Console.WriteLine("SMERT");
                    obj.TicsInTheAir = 0;
                    obj.OnGround = true;
                }
                if (obj.visible) RenderTarget.DrawBitmap(obj.Texture, obj.Collider, 1, SharpDX.Direct2D1.BitmapInterpolationMode.NearestNeighbor);
            }
            // draw walls
            foreach (var obj in Level.WallsOnScene)
            {
                RenderTarget.DrawBitmap(obj.Texture, obj.Collider, 1, SharpDX.Direct2D1.BitmapInterpolationMode.NearestNeighbor);
            }
            if (statusGame == StatusGame.Debug && GetStartPos == true)
            {
                RenderTarget.DrawRectangle(userinterface.BuildRect, userinterface.blackBrush);
            }

            #region fps
            RenderTarget.DrawText(userinterface.message, userinterface.textFormat, userinterface.MessageTextBox, userinterface.blackBrush);
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
    }
}
