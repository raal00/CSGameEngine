using GameLib;
using GameLib.Core;
using GameLib.Enums;
using GameLib.Models;
using System;

namespace Game
{
    // init obj part
    public sealed partial class My2dGame : WinCore
    {
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

            if (Debug) // if level contructor
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
                floor.texturePath = Strings.TexturePath + "floor1.png";
                floor.SetTexture(RenderTarget);
                floor.objectTag = Tag.ENVIRONMENT;

                background.Collider = new SharpDX.Mathematics.Interop.RawRectangleF(0, 0, mainRenderForm.Width * 2, mainRenderForm.Height - 200);
                background.texturePath = Strings.TexturePath + "background1.jpg";
                background.SetTexture(RenderTarget);
                background.objectTag = Tag.ENVIRONMENT;

                player1.texturePath = Strings.TexturePath + "Idle1.png";
                player1.Move(new GameLib.Transform.Vector2(200, -200));
                player1.SetTexture(RenderTarget);

                Level.BackGround = background;
                Level.Floor = floor;
                Level.ObjectsOnScene.Add(player1);

                controlledHero = player1;
            }
            else
            {
                mainRenderForm.Menu = new System.Windows.Forms.MainMenu();
                mainRenderForm.Menu.MenuItems.Add("Exit").Click += OnMenuExitClick;
                mainRenderForm.Menu.MenuItems.Add("Pause").Click += OnMenuPauseClick;
                mainRenderForm.Menu.MenuItems.Add("Save").Click += OnMenuSaveClick;
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
                    controlledHero.texturePath = Strings.TexturePath + "Idle1.png";
                    controlledHero.Move(new GameLib.Transform.Vector2(200, -200));
                    controlledHero.SetTexture(RenderTarget);
                }
                controlledHero.animationController = new AnimationController(Strings.AnimationControllerPath + "Hero/", controlledHero, RenderTarget);

                foreach (var obj in Level.WallsOnScene)
                {
                    obj.SetTexture(RenderTarget);
                }
                Level.Floor.SetTexture(RenderTarget);
                Level.BackGround.SetTexture(RenderTarget);
            }
        }

    }
}