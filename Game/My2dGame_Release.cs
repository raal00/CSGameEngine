using GameLib;
using GameLib.Core;
using GameLib.Enums;
using GameLib.Models;
using System;

namespace Game
{
    // release part
    public sealed partial class My2dGame : WinCore
    {
        private void MainRenderForm_MouseClick_Release(object sender, System.Windows.Forms.MouseEventArgs e)
        {

        }



        private void MainRenderForm_KeyDown_release(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            bool free = true;
            switch (e.KeyCode)
            {
                case System.Windows.Forms.Keys.Space:
                    if (controlledHero.OnGround == false) break;
                    if (controlledHero.Collider.Top - Jump.Y <= 0) break;
                    for (int i = (int)controlledHero.Collider.Left; i < (int)controlledHero.Collider.Right; i++)
                    {
                        if (mapMatrix[(int)(controlledHero.Collider.Top - controlledHero.JumpPower), i] == 1)
                        {
                            free = false;
                            break;
                        }
                    }
                    if (free)
                    {
                        Jump.X = 0;
                        Jump.Y = controlledHero.JumpPower;
                        controlledHero.Move(Jump);
                    }
                    break;
                case System.Windows.Forms.Keys.Right:
                    if (controlledHero.Collider.Right + controlledHero.Speed >= MatrWidth) break;
                    for (int i = (int)controlledHero.Collider.Top + 10; i < (int)controlledHero.Collider.Bottom; i++)
                    { 
                        if (mapMatrix[i, (int)(controlledHero.Collider.Right + controlledHero.Speed)] == 1) 
                        {
                            free = false;
                            break;
                        }
                    }
                    if (free) 
                    {
                        Right.X = controlledHero.Speed;
                        Right.Y = 0;
                        controlledHero.Move(Right); 
                    }
                    break;
                case System.Windows.Forms.Keys.Left:
                    if (controlledHero.Collider.Left - controlledHero.Speed <= 0) break;
                    for (int i = (int)controlledHero.Collider.Top + 10; i < (int)controlledHero.Collider.Bottom; i++)
                    {
                        if (mapMatrix[i, (int)(controlledHero.Collider.Left - controlledHero.Speed)] == 1)
                        {
                            free = false;
                            break;
                        }
                    }
                    if (free)
                    {
                        Left.X = -controlledHero.Speed;
                        Left.Y = 0;
                        controlledHero.Move(Left);
                    }
                    break;
            }
        }
    }
}
