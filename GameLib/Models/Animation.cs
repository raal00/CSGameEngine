using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using GameLib.Controller;
using SharpDX.Direct2D1;
using SharpDX.WIC;
using System.Drawing.Imaging;

namespace GameLib.Models
{
    public class Animation
    {
        public SharpDX.Direct2D1.Bitmap[] AnimTextures;
        private readonly GameObject target;

        public event EventHandler OnAnimationEnd = delegate { };

        private int animCount;
        private object locker;

        public string AnimName;
        public bool Stopped;
        public bool Loop;

        public Animation(string animPath, string animName,GameObject gameObject, WindowRenderTarget renderTarget)
        {
            Loop = true;
            target = gameObject;
            AnimName = animName;
            Stopped = false;
            locker = new object();

            DirectoryInfo dic = new DirectoryInfo(animPath);
            
            FileInfo[] textureFiles = dic.GetFiles();
            animCount = textureFiles.Length;
            AnimTextures = new SharpDX.Direct2D1.Bitmap[animCount];
            for (int i = 0; i < animCount; i++) 
            {
                AnimTextures[i] = loadTexture(renderTarget, textureFiles[i].FullName);
            }
            //OnAnimationEnd = new EventHandler(null, null);
        }
        public async Task PlayAsync(int time) => await Task.Run(()=> {
            do
            {
                for (int i = 0; i < animCount; i++)
                {
                    if (Stopped) 
                    {
                        Loop = false;
                        break;
                    }
                    lock (locker) target.Texture = AnimTextures[i];
                    Task.Delay(time).Wait();
                }
            }
            while (Loop);
            if (!Stopped)
            {
                OnAnimationEnd.Invoke(null, null);
            }
        });

        private SharpDX.Direct2D1.Bitmap loadTexture(WindowRenderTarget target, string texturePath)
        {
            TextureHandler bmpHandler = new TextureHandler();
            ImagingFactory factory = new ImagingFactory();

            var bmps = bmpHandler.LoadBMPSFromFile(texturePath, factory, ImageFormat.Bmp);
            if (bmps == null)
            {
                Console.WriteLine("Error");
                return null;
            }
            FormatConverter formatConverter = new FormatConverter(factory);
            formatConverter.Initialize(bmps, SharpDX.WIC.PixelFormat.Format32bppPBGRA, BitmapDitherType.Spiral8x8, null, 0f, BitmapPaletteType.MedianCut);
            return SharpDX.Direct2D1.Bitmap.FromWicBitmap(target, formatConverter);
        }
    }
}
