using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;
using SharpDX.WIC;

using System;
using System.Drawing.Imaging;
using System.Xml.Serialization;

using GameLib.Enums;
using GameLib.Controller;
using GameLib.Transform;

namespace GameLib.Models
{
    [Serializable]
    public class GameObject : IGameObject
    {
        // outside params
        public bool visible;
        public int deep;
        public float Opacity = 1;
        public Vector2 position;
        public Size Size;

        // dinamic params
        public bool OnGround;
        public int Speed;
        public int JumpPower;
        public int TicsInTheAir;

        // inside params
        public string name;
        public Tag objectTag;
        public string texturePath;
        
        public RawRectangleF Collider;

        [XmlIgnore]
        [NonSerialized]
        public SharpDX.Direct2D1.Bitmap Texture;
        [XmlIgnore]
        [NonSerialized]
        public Phisics phisics;
        [XmlIgnore]
        [NonSerialized]
        public AnimationController animationController;


        public GameObject() 
        {
            initParams();
        }

        public GameObject(string name)
        {
            initParams(name:name);
        }
        public GameObject(string name, Size s, Vector2 pos)
        {
            initParams(pos, s, name);
        }

        public void Move(Vector2 a) 
        {
                position ^= a;
                Collider.Left += a.X;
                Collider.Right += a.X;
                Collider.Top -= a.Y;
                Collider.Bottom -= a.Y;
        }

        public void SetTexture(WindowRenderTarget target)
        {
            TextureHandler bmpHandler = new TextureHandler();
            ImagingFactory factory = new ImagingFactory();

            var bmps = bmpHandler.LoadBMPSFromFile(texturePath, factory, ImageFormat.Bmp);
            if (bmps == null) 
            {
                Console.WriteLine("Error");
                return;
            }
            FormatConverter formatConverter = new FormatConverter(factory);
            formatConverter.Initialize(bmps, SharpDX.WIC.PixelFormat.Format32bppPBGRA, BitmapDitherType.Spiral8x8, null, 0f, BitmapPaletteType.MedianCut);
            Texture = SharpDX.Direct2D1.Bitmap.FromWicBitmap(target, formatConverter);
        }

        private void initParams(Vector2 pos = null, Size s = null, string name = "gameObj") 
        {
            if (s == null) s = new Size();
            if (pos == null)
                this.position = new Vector2(0, 0);
            else
                this.position = pos;
            this.name = name;
            Collider.Left = 0;
            Collider.Right = s.Width;
            Collider.Top = 0;
            Collider.Bottom = s.Height;
            TicsInTheAir = 0;
            Speed = 5;
            JumpPower = 126;
            OnGround = false;
            visible = true;
            phisics = new Phisics();
        }
     
    }
}
