using GameLib;
using GameLib.Controller;
using GameLib.Core;
using GameLib.Enums;
using GameLib.Models;
using GameLib.Params;
using SharpDX.Direct2D1;
using SharpDX.WIC;
using System;
using System.Drawing.Imaging;
using System.IO;

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
            MapValues.cam2d = new Camera2D(0, 0);
            RenderTarget.Transform = MapValues.cam2d.GetTransform3x2();
            MapValues.camMove = new SharpDX.Vector2(200, 200);
            // set UI
            if (userinterface == null) userinterface = new UI(RenderTarget, MapValues.cam2d);
            //mainRenderForm.Menu = new System.Windows.Forms.MainMenu();
            //mainRenderForm.Menu.MenuItems.Add("Exit").Click += OnMenuExitClick;
            //mainRenderForm.Menu.MenuItems.Add("Pause").Click += OnMenuPauseClick;
            //mainRenderForm.Menu.MenuItems.Add("Save").Click += OnMenuSaveClick;
            if (Debug) // if level contructor
            {
                #region Debug init part
                userinterface.SetMessageText("Режим создания уровня");
                statusGame = StatusGame.Debug;
                objConstructorTag = ObjConstructorTag.Barrier;
                
                player1 = new GameObject("player 1");
                player1.objectTag = Tag.CONTROLLEDHERO;
                background = new Barrier();

                
                background.Collider = new SharpDX.Mathematics.Interop.RawRectangleF(0, 0, mainRenderForm.Width * MapSizeK, mainRenderForm.Height * MapSizeK);
                background.texturePath = Strings.TexturePath + "background.jpg";
                background.SetTexture(RenderTarget);
                background.objectTag = Tag.ENVIRONMENT;

                player1.texturePath = Strings.TexturePath + "Idle1.png";
                player1.Move(new GameLib.Transform.Vector2(mainRenderForm.Width / 2, -mainRenderForm.Height / 2));
                player1.SetTexture(RenderTarget);

                Level.BackGround = background;
                Level.ObjectsOnScene.Add(player1);
                MapValues.cam2d.MoveCamera(new SharpDX.Vector2(mainRenderForm.Width, mainRenderForm.Height));
                controlledHero = player1;

                DirectoryInfo dic = new DirectoryInfo(Strings.BuildWallsPath);
                FileInfo[] textureFiles = dic.GetFiles();
                int texturesCount = textureFiles.Length;
                buildTextures = new SharpDX.Direct2D1.Bitmap[texturesCount];
                buildTextures_Names = new string[texturesCount];
                for (int i = 0; i < texturesCount; i++)
                {
                    buildTextures_Names[i] = textureFiles[i].FullName;
                    buildTextures[i] = DebugMethods.loadWallTexture(RenderTarget, buildTextures_Names[i]);
                }
                #endregion
            }
            else
            {
                #region Release init part
                statusGame = StatusGame.Release;
                MapValues.cam2d.Zoom = 1.5f;
                foreach (var obj in Level.ObjectsOnScene)
                {
                    obj.SetTexture(RenderTarget);
                    if (obj.objectTag == Tag.CONTROLLEDHERO) controlledHero = obj;
                }
                if (controlledHero == null)
                {
                    controlledHero = new GameObject();
                    controlledHero.texturePath = Strings.TexturePath + "Idle1.png";
                    controlledHero.Move(new GameLib.Transform.Vector2(200, -200));
                    controlledHero.SetTexture(RenderTarget);
                }
                MapValues.cam2d.MoveCamera(MapValues.camMove);
                RenderTarget.Transform = MapValues.cam2d.GetTransform3x2();
                controlledHero.animationController = new AnimationController(Strings.AnimationControllerPath + "Hero/", controlledHero, RenderTarget);

                foreach (var obj in Level.WallsOnScene)
                {
                    obj.SetTexture(RenderTarget);
                }
                Level.BackGround.SetTexture(RenderTarget);
                #endregion
            }
        }
    }
}