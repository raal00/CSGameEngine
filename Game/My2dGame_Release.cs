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
                    for (int i = (int)controlledHero.Collider.Top; i < (int)controlledHero.Collider.Bottom; i++)
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
                        if (!controlledHero.animationController.CurrentAnimName.Contains("Run"))
                        {
                            controlledHero.animationController.AnimationRun("Run_R", true, 200);
                            Right.X = controlledHero.Speed * 5;
                        }
                        else 
                        {
                            Right.X = controlledHero.Speed;
                        }
                        controlledHero.Move(Right); 
                    }
                    break;
                case System.Windows.Forms.Keys.Left:
                    if (controlledHero.Collider.Left - controlledHero.Speed <= 0) break;
                    for (int i = (int)controlledHero.Collider.Top; i < (int)controlledHero.Collider.Bottom; i++)
                    {
                        if (mapMatrix[i, (int)(controlledHero.Collider.Left - controlledHero.Speed)] == 1)
                        {
                            free = false;
                            break;
                        }
                    }
                    if (free)
                    {
                        if (!controlledHero.animationController.CurrentAnimName.Contains("Run"))
                        {
                            controlledHero.animationController.AnimationRun("Run_L", true, 200);
                            Left.X = -controlledHero.Speed * 5;
                        }
                        else 
                        {
                            Left.X = -controlledHero.Speed;
                        }
                        controlledHero.Move(Left);
                    }
                    break;
            }
        }
        private void MainRenderForm_KeyUp_release(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (controlledHero.animationController.CurrentAnimName.Contains("Run")) 
            {
                controlledHero.animationController.AnimationRun("Idle", true, 200);
            }
        }
    }
}
