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

        public bool IsAnimationCompleating = false;
        public string CurrentAnimName = null;
        public Animation PlayingAnimation = null;
        public Animation DefaultAnimation = null;

        public AnimationController(string controllerPath, GameObject target, WindowRenderTarget renderTarget) 
        {
            DirectoryInfo dic = new DirectoryInfo(controllerPath);
            DirectoryInfo[] animDirs = dic.GetDirectories();
            animsCount = animDirs.Length;
            anims = new Animation[animsCount];
            for (int i = 0; i < animsCount; i++) 
            {
                anims[i] = new Animation(controllerPath + "/"+ animDirs[i].Name+"/", animDirs[i].Name, target, renderTarget);
            }
            DefaultAnimation = anims.Where(x => x.AnimName == "Idle").FirstOrDefault();
            playDefaultAnimation();
        }

        public async void AnimationRun(string name, bool loop, int changeTime) 
        {
            Animation a = anims.Where(x => x.AnimName == name).FirstOrDefault();
            if (a != null && IsAnimationCompleating == false) 
            {
                stopDefaultAnimation();
                IsAnimationCompleating = true;
                PlayingAnimation = a;
                PlayingAnimation.Stopped = false;
                CurrentAnimName = name;
                a.Loop = loop;
                if (!loop) a.OnAnimationEnd += OnAnimationEndAction;
                await a.PlayAsync(changeTime); 
            }
        }
        public async void AnimationRun(Animation animation, bool loop, int changeTime) 
        {
            if (animation != null && PlayingAnimation == DefaultAnimation)
            {
                PlayingAnimation = animation;
                PlayingAnimation.Stopped = false;
                CurrentAnimName = animation.AnimName;

                animation.Loop = loop;
                await animation.PlayAsync(changeTime);
            }
        }

        private void OnAnimationEndAction(object sender, EventArgs e)
        {
            IsAnimationCompleating = false;
            AnimationStop();
        }

        private async void playDefaultAnimation() 
        {
            PlayingAnimation = DefaultAnimation;
            PlayingAnimation.Stopped = false;
            CurrentAnimName = DefaultAnimation.AnimName;

            DefaultAnimation.Loop = true;
            await DefaultAnimation.PlayAsync(200);
        }
        private void stopDefaultAnimation()
        {
            if (PlayingAnimation == DefaultAnimation)
            {
                IsAnimationCompleating = false;
                PlayingAnimation.Stopped = true;
                PlayingAnimation = null;
                CurrentAnimName = null;
            }
        }

        public void AnimationStop() 
        {
            if (PlayingAnimation != null && PlayingAnimation != DefaultAnimation)
            {
                IsAnimationCompleating = false;
                PlayingAnimation.Stopped = true;
                playDefaultAnimation();
            }
        }
        
    }
}
