using SharpDX.Direct2D1;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace GameLib.Models
{
    public class AnimationController
    {
        private readonly GameObject target;

        private Animation[] anims;
        private int animsCount;

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
        }
        public async void AnimRun_Run() 
        {
            Animation a = anims.Where(x => x.AnimName == "Run").FirstOrDefault();
            if (a != null) await a.PlayAsync(200);
        }
        public async void AnimIdle_Run() 
        {
            Animation a = anims.Where(x => x.AnimName == "Idle").FirstOrDefault();
            if (a != null) await a.PlayAsync(200);
        }
    }
}
