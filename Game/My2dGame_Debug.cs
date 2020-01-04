using GameLib;
using GameLib.Core;
using GameLib.Enums;
using GameLib.Models;
using GameLib.Transform;
using System;

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
            float x = e.Location.X * 2;
            float y = e.Location.Y;

            IGameObject gameObject = null;
            if (objConstructorTag == ObjConstructorTag.GameObj)
            {
                gameObject = new GameObject();
                (gameObject as GameObject).Size = new Size(100,128);
                (gameObject as GameObject).texturePath = Strings.TexturePath + "texture1.png";
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
                    (gameObject as Barrier).texturePath = Strings.TexturePath + "wall.png";

                    (gameObject as Barrier).Collider.Left   = startPos.X;
                    (gameObject as Barrier).Collider.Right  = endPos.X;
                    (gameObject as Barrier).Collider.Top    = startPos.Y;
                    (gameObject as Barrier).Collider.Bottom = endPos.Y;
                    (gameObject as Barrier).SetTexture(RenderTarget);
                    GetStartPos = false;
                    float top = (gameObject as Barrier).Collider.Top;
                    float bot = (gameObject as Barrier).Collider.Bottom;
                    float left = (gameObject as Barrier).Collider.Left;
                    float right = (gameObject as Barrier).Collider.Right;
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

                    for (float i = top; i < bot; i++)
                    {
                        for (float j = left; j < right; j++)
                        {
                            mapMatrix[(int)i, (int)j] = 1;
                        }
                    }
                    (gameObject as Barrier).objectTag = Tag.ENVIRONMENT;
                }
            }
            Level.AddObject(gameObject, objConstructorTag);
        }
        private void MainRenderForm_KeyDown_Debug(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case System.Windows.Forms.Keys.Space:
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
            }
        }
    }
}
