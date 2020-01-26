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
    public abstract class IGameObject
    {
        public RawRectangleF Collider;
        public string texturePath;
        public bool visible;
        public Vector2 position;
        public Size Size;
        public Tag objectTag;

        [XmlIgnore]
        [NonSerialized]
        public SharpDX.Direct2D1.Bitmap Texture;

        public virtual void SetTexture(WindowRenderTarget target) 
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
    }
}
