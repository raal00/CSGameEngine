using SharpDX.Direct2D1;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace GameLib.Models
{
    public class AnimationController
    {

        private Animation[] anims;
        private int animsCount;

        public string CurrentAnimName = null;

        public AnimationController(string controllerPath, GameObject target, WindowRenderTarget renderTarget) 
        {
            DirectoryInfo dic = new DirectoryInfo(controllerPath);
            DirectoryInfo[] animDirs = dic.GetDirectories();
            animsCount = animDirs.Length;
            anims = new Animation[animsCount];
            for (int i = 0; i < animsCount; i++) 
            {
                anims[i] = new Animation(controllerPath+"/"+ animDirs[i].Name+"/", animDirs[i].Name, target, renderTarget);
            }
            AnimationRun("Idle", true, 200);
        }

        public async void AnimationRun(string name, bool loop, int changeTime) 
        {
            AnimationStop();
            Animation a = anims.Where(x => x.AnimName == name).FirstOrDefault();
            if (a != null) 
            {
                CurrentAnimName = name;
                a.Loop = loop;
                await a.PlayAsync(changeTime); 
            }
        }
        public void AnimationStop() 
        {
            if (CurrentAnimName != null) anims.Where(x => x.AnimName == CurrentAnimName).FirstOrDefault().Loop = false;
        }
    }
}
