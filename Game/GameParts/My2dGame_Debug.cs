using GameLib;
using GameLib.Core;
using GameLib.Enums;
using GameLib.Models;
using GameLib.Params;
using GameLib.Transform;
using System;
using System.Linq;

namespace Game
{
    // debug part
    public sealed partial class My2dGame : WinCore
    {
        private bool GetStartPos = false;
        private Vector2 startPos = new Vector2();
        private Vector2 endPos = new Vector2();


        private void MainRenderForm_MouseClick_Debug(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            float x = e.Location.X + MapValues.cam2d.camPos.X;
            float y = e.Location.Y + MapValues.cam2d.camPos.Y;
            IGameObject gameObject = null;

            if (e.Button == System.Windows.Forms.MouseButtons.Middle) 
            {
                gameObject = (from obj in Level.WallsOnScene where
                              obj.Collider.Left  < x &&
                              obj.Collider.Right > x &&
                              obj.Collider.Top    < y &&
                              obj.Collider.Bottom > y
                              select obj).FirstOrDefault();
                if (gameObject != null)
                {
                    Level.ForDeleting = gameObject;
                    Level.DeleteStatus = true;
                }
                return;
            }

            if (objConstructorTag == ObjConstructorTag.GameObj)
            {
                gameObject = new GameObject("aa",new Size(100, 100));
                (gameObject as GameObject).texturePath = Strings.TexturePath + "Idle1.png";
                
                (gameObject as GameObject).SetTexture(RenderTarget);
                (gameObject as GameObject).Move(new GameLib.Transform.Vector2(x - 32, -y));
                (gameObject as GameObject).objectTag = Tag.HERO;
            }
            else if (objConstructorTag == ObjConstructorTag.Barrier)
            {
                if (GetStartPos == false)
                {
                    GetStartPos = true;
                    startPos.X = x;
                    startPos.Y = y;
                    userinterface.BuildRect.Left = x;
                    userinterface.BuildRect.Top = y;
                    return;
                }
                else 
                {
                    endPos.X = x;
                    endPos.Y = y;
                    gameObject = new Barrier();
                    //(gameObject as Barrier).texturePath = Strings.TexturePath + "wall.png";

                    gameObject.Collider.Left   = startPos.X;
                    gameObject.Collider.Right  = endPos.X;
                    gameObject.Collider.Top    = startPos.Y;
                    gameObject.Collider.Bottom = endPos.Y;
                    //(gameObject as Barrier).SetTexture(RenderTarget);
                    gameObject.Texture = buildTextures[numberOfWallTexture];
                    gameObject.texturePath = buildTextures_Names[numberOfWallTexture];
                    GetStartPos = false;
                    float top = gameObject.Collider.Top;
                    float bot = gameObject.Collider.Bottom;
                    float left  = gameObject.Collider.Left;
                    float right = gameObject.Collider.Right;
                    if (top > bot) 
                    {
                        float temp = top;
                        top = bot;
                        bot = temp;
                    }
                    if (left > right)
                    {
                        float temp = left;
                        left = right;
                        right = temp;
                    }
                    if (hasPhisCollider)
                    {
                        for (float i = top; i < bot; i++)
                        {
                            for (float j = left; j < right; j++)
                            {
                                MapValues.mapMatrix[(int)i, (int)j] = 1;
                            }
                        }
                    }
                    (gameObject as Barrier).position = new Vector2((right-left)/2+left,(bot-top)/2+top);
                    (gameObject as Barrier).HideOnApproach = hideOnApproach;
                    (gameObject as Barrier).visible = Visible;
                    (gameObject as Barrier).Breakable = breakable;
                    (gameObject as Barrier).objectTag = Tag.ENVIRONMENT;
                }
            }
            Level.AddObject(gameObject, objConstructorTag);
        }
        private void MainRenderForm_KeyDown_Debug(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case System.Windows.Forms.Keys.Space: // change type of object
                    if (objConstructorTag == ObjConstructorTag.Barrier)
                    {
                        objConstructorTag = ObjConstructorTag.GameObj;
                        userinterface.SetMessageText("Добавление объектов");
                    }
                    else if (objConstructorTag == ObjConstructorTag.GameObj)
                    {
                        objConstructorTag = ObjConstructorTag.Barrier;
                        userinterface.SetMessageText("Добавление стен");
                    }
                    break;
                case System.Windows.Forms.Keys.E: // change texture
                    if (numberOfWallTexture < buildTextures.Length - 1) numberOfWallTexture++;
                    break;
                case System.Windows.Forms.Keys.Q: // change texture
                    if (numberOfWallTexture > 0) numberOfWallTexture--;
                    break;
                case System.Windows.Forms.Keys.Z: // change object breakable
                    breakable = !breakable;
                    break;
                case System.Windows.Forms.Keys.X:
                    hasPhisCollider = !hasPhisCollider;
                    break;
                case System.Windows.Forms.Keys.C:
                    hideOnApproach = !hideOnApproach;
                    break;
                case System.Windows.Forms.Keys.V:
                    Visible = !Visible;
                    break;
                case System.Windows.Forms.Keys.Left: // camera left
                    MapValues.camMove.Y = 0;
                    MapValues.camMove.X = -5;
                    MapValues.cam2d.MoveCamera(MapValues.camMove);
                    userinterface.SetUIPos();
                    if (Debug == true) userinterface.SetUIPos_Debug();
                    RenderTarget.Transform = MapValues.cam2d.GetTransform3x2();
                    break;
                case System.Windows.Forms.Keys.Right: // camera rigth
                    MapValues.camMove.Y = 0;
                    MapValues.camMove.X = 5;
                    MapValues.cam2d.MoveCamera(MapValues.camMove);
                    userinterface.SetUIPos();
                    if (Debug == true) userinterface.SetUIPos_Debug();
                    RenderTarget.Transform = MapValues.cam2d.GetTransform3x2();
                    break;
                case System.Windows.Forms.Keys.Up: // camera up
                    MapValues.camMove.Y = -5;
                    MapValues.camMove.X = 0;
                    MapValues.cam2d.MoveCamera(MapValues.camMove);
                    userinterface.SetUIPos();
                    if (Debug == true) userinterface.SetUIPos_Debug();
                    RenderTarget.Transform = MapValues.cam2d.GetTransform3x2();
                    break;
                case System.Windows.Forms.Keys.Down: // camera down
                    MapValues.camMove.Y = 5;
                    MapValues.camMove.X = 0;
                    MapValues.cam2d.MoveCamera(MapValues.camMove);
                    userinterface.SetUIPos();
                    if (Debug == true) userinterface.SetUIPos_Debug();
                    RenderTarget.Transform = MapValues.cam2d.GetTransform3x2();
                    break;
                default: break;
            }

        }
        private void MainRenderForm_MouseWheel_Debug(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if ((e.Delta > 0 && MapValues.cam2d.Zoom < 4f) || (e.Delta < 0 && MapValues.cam2d.Zoom > 0.7f)) 
            {
                MapValues.cam2d.Zoom += e.Delta / 120;
                userinterface.SetUIPos();
                if (Debug == true) userinterface.SetUIPos_Debug();
                RenderTarget.Transform = MapValues.cam2d.GetTransform3x2();
            }
        }
    }
}
