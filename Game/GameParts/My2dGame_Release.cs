using GameLib;
using GameLib.Core;
using GameLib.Enums;
using GameLib.Models;
using GameLib.Params;
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
            bool move = false;
            if (!keys.ContainsKey(e.KeyCode))
            {
                keys.Add(e.KeyCode, true);
            }
            else
            {
                keys[e.KeyCode] = true;
            }


            if (keys.ContainsKey(System.Windows.Forms.Keys.Up) && keys[System.Windows.Forms.Keys.Up] &&
                keys.ContainsKey(System.Windows.Forms.Keys.Right) && keys[System.Windows.Forms.Keys.Right])
            {
                // Вперед + право
                controlledHero.MoveVec.Y = controlledHero.Speed;
                controlledHero.MoveVec.X = controlledHero.Speed;
                move = true;
            }
            else if (keys.ContainsKey(System.Windows.Forms.Keys.Up) && keys[System.Windows.Forms.Keys.Up] &&
                     keys.ContainsKey(System.Windows.Forms.Keys.Left) && keys[System.Windows.Forms.Keys.Left])
            {
                // Вперед + лево
                controlledHero.MoveVec.Y = controlledHero.Speed;
                controlledHero.MoveVec.X = -controlledHero.Speed;
                move = true;
            }
            else if (keys.ContainsKey(System.Windows.Forms.Keys.Down) && keys[System.Windows.Forms.Keys.Down] &&
                     keys.ContainsKey(System.Windows.Forms.Keys.Right) && keys[System.Windows.Forms.Keys.Right])
            {
                // Назад + право  
                controlledHero.MoveVec.Y = -controlledHero.Speed;
                controlledHero.MoveVec.X = controlledHero.Speed;
                move = true;
            }
            else if (keys.ContainsKey(System.Windows.Forms.Keys.Down) && keys[System.Windows.Forms.Keys.Down] &&
                     keys.ContainsKey(System.Windows.Forms.Keys.Left) && keys[System.Windows.Forms.Keys.Left])
            {
                // Назад + лево 
                controlledHero.MoveVec.Y = -controlledHero.Speed;
                controlledHero.MoveVec.X = -controlledHero.Speed;
                move = true;
            }
            else if (keys.ContainsKey(System.Windows.Forms.Keys.Up) && keys[System.Windows.Forms.Keys.Up])
            {
                // Вперед
                controlledHero.MoveVec.Y = controlledHero.Speed;
                controlledHero.MoveVec.X = 0;
                move = true;
            }
            else if (keys.ContainsKey(System.Windows.Forms.Keys.Down) && keys[System.Windows.Forms.Keys.Down])
            {
                // Назад
                controlledHero.MoveVec.Y = -controlledHero.Speed;
                controlledHero.MoveVec.X = 0;
                move = true;
            }
            else if (keys.ContainsKey(System.Windows.Forms.Keys.Left) && keys[System.Windows.Forms.Keys.Left])
            {
                // Влево
                controlledHero.MoveVec.X = -controlledHero.Speed;
                controlledHero.MoveVec.Y = 0;
                move = true;
            }
            else if (keys.ContainsKey(System.Windows.Forms.Keys.Right) && keys[System.Windows.Forms.Keys.Right])
            {
                // Вправо
                controlledHero.MoveVec.X = controlledHero.Speed;
                controlledHero.MoveVec.Y = 0;
                move = true;
            }
            else
            {
                switch (e.KeyCode)
                {
                    case System.Windows.Forms.Keys.E:
                        controlledHero.animationController.AnimationRun("Kick_R", false, 200);
                        IGameObject gobject = (from obj in Level.WallsOnScene
                                               where
                                    obj.Breakable == true &&
                                    Math.Abs(obj.position.X - controlledHero.position.X) < 100 &&
                                    Math.Abs(obj.position.Y - controlledHero.position.Y) < 300
                                               select obj).FirstOrDefault();
                        if (gobject != null) breakWall((Barrier)gobject);
                        break;
                }
                //
            }
            if (move)
            {
                controlledHero.Move(controlledHero.MoveVec);
                MapValues.cam2d.camPos.X = controlledHero.position.X - mainRenderForm.Width / 3;
                MapValues.cam2d.camPos.Y = controlledHero.position.Y - mainRenderForm.Height / 3;
                userinterface.SetUIPos();
                if (Debug == true) userinterface.SetUIPos_Debug();
                RenderTarget.Transform = MapValues.cam2d.GetTransform3x2();
            }
        }
        private void MainRenderForm_KeyUp_release(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (keys.ContainsKey(e.KeyCode)) 
            {
                keys[e.KeyCode] = false;
            }
            //if (controlledHero.animationController.PlayingAnimation.Loop == true) 
            
            {
                controlledHero.animationController.AnimationStop();
            }
        }
    }
}
