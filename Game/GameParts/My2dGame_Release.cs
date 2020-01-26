using GameLib;
using GameLib.Core;
using GameLib.Enums;
using GameLib.Models;
using System;
using System.Linq;

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
                case System.Windows.Forms.Keys.Right:
                    if (controlledHero.Collider.Right + controlledHero.Speed >= MatrWidth) break;
                    for (int i = (int)controlledHero.Collider.Top + 10; i < (int)controlledHero.Collider.Bottom - 10; i++)
                    { 
                        if (mapMatrix[i, (int)(controlledHero.Collider.Right + controlledHero.Speed)] == 1) 
                        {
                            free = false;
                            break;
                        }
                    }
                    if (free)
                    {
                        Move.X = controlledHero.Speed;
                        Move.Y = 0;
                        
                        if (!controlledHero.animationController.CurrentAnimName.Contains("Run"))
                        {
                            controlledHero.animationController.AnimationRun("Run_R", true, 200);
                        }
                        // move object
                        controlledHero.Move(Move);
                        // move camera
                        if (true)
                        {
                            camMove.Y = 0;
                            camMove.X = controlledHero.Speed;
                            cam2d.MoveCamera(camMove);
                            userinterface.SetUIPos();
                            if (Debug == true) userinterface.SetUIPos_Debug();
                            RenderTarget.Transform = cam2d.GetTransform3x2();
                        }
                    }
                    break;
                case System.Windows.Forms.Keys.Left:
                    if (controlledHero.Collider.Left - controlledHero.Speed <= 0) break;
                    for (int i = (int)controlledHero.Collider.Top + 10; i < (int)controlledHero.Collider.Bottom - 10; i++)
                    {
                        if (mapMatrix[i, (int)(controlledHero.Collider.Left - controlledHero.Speed)] == 1)
                        {
                            free = false;
                            break;
                        }
                    }
                    if (free)
                    {
                        Move.X = -controlledHero.Speed;
                        Move.Y = 0;
                        if (!controlledHero.animationController.CurrentAnimName.Contains("Run"))
                        {
                            controlledHero.animationController.AnimationRun("Run_R", true, 200);
                        }
                        // move object
                        controlledHero.Move(Move);
                        // move camera
                        if (true)
                        {
                            camMove.Y = 0;
                            camMove.X = -controlledHero.Speed;
                            cam2d.MoveCamera(camMove);
                            userinterface.SetUIPos();
                            if (Debug == true) userinterface.SetUIPos_Debug();
                            RenderTarget.Transform = cam2d.GetTransform3x2();
                        }
                    }
                    break;
                case System.Windows.Forms.Keys.Up:
                    if (controlledHero.Collider.Top - controlledHero.Speed <= 0) break;
                    for (int i = (int)controlledHero.Collider.Left + 10; i < (int)controlledHero.Collider.Right - 10; i++)
                    {
                        if (mapMatrix[(int)(controlledHero.Collider.Top + controlledHero.Speed), i] == 1)
                        {
                            free = false;
                            break;
                        }
                    }
                    if (free)
                    {
                        Move.Y = controlledHero.Speed;
                        Move.X = 0;
                        if (!controlledHero.animationController.CurrentAnimName.Contains("Run"))
                        {
                            controlledHero.animationController.AnimationRun("Run_R", true, 200);
                        }
                        // move object
                        controlledHero.Move(Move);
                        // move camera
                        if (true)
                        {
                            camMove.Y = -controlledHero.Speed;
                            camMove.X = 0;
                            cam2d.MoveCamera(camMove);
                            userinterface.SetUIPos();
                            if (Debug == true) userinterface.SetUIPos_Debug();
                            RenderTarget.Transform = cam2d.GetTransform3x2();
                        }
                    }
                    break;
                case System.Windows.Forms.Keys.Down:
                    if (controlledHero.Collider.Top + controlledHero.Speed >= MatrHeight) break;
                    for (int i = (int)controlledHero.Collider.Left + 10; i < (int)controlledHero.Collider.Right - 10; i++)
                    {
                        if (mapMatrix[(int)(controlledHero.Collider.Bottom - controlledHero.Speed), i] == 1)
                        {
                            free = false;
                            break;
                        }
                    }
                    if (free)
                    {
                        Move.Y = -controlledHero.Speed;
                        Move.X = 0;
                        if (!controlledHero.animationController.CurrentAnimName.Contains("Run"))
                        {
                            controlledHero.animationController.AnimationRun("Run_R", true, 200);
                        }
                        // Move object
                        controlledHero.Move(Move);
                        // Move camera
                        if (true)
                        {
                            camMove.Y = controlledHero.Speed;
                            camMove.X = 0;
                            cam2d.MoveCamera(camMove);
                            userinterface.SetUIPos();
                            if (Debug == true) userinterface.SetUIPos_Debug();
                            RenderTarget.Transform = cam2d.GetTransform3x2();
                        }
                    }
                    break;

                case System.Windows.Forms.Keys.E:
                    
                        controlledHero.animationController.AnimationRun("Kick_R", false, 200);
                        IGameObject gobject = (from obj in Level.WallsOnScene where 
                                           obj.Breakable == true &&
                                             Math.Abs(obj.position.X - controlledHero.position.X) < 100 &&
                                             Math.Abs(obj.position.Y - controlledHero.position.Y) < 400
                                       select obj).FirstOrDefault();
                    if (gobject != null) breakWall((Barrier)gobject);
                    break;
            }
        }
        private void MainRenderForm_KeyUp_release(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (controlledHero.animationController.PlayingAnimation.Loop == true) 
            {
                controlledHero.animationController.AnimationStop();
            }
        }
    }
}
