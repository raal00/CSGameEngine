using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;
using SharpDX.WIC;

using System;
using System.Drawing.Imaging;

using GameLib.Enums;
using GameLib.Controller;
using System.Xml.Serialization;
using GameLib.Transform;

namespace GameLib.Models
{
    [Serializable]
    public class Barrier : IGameObject
    {
        public bool visible;
        public int deep;
        public Tag objectTag;
        public float Opacity = 1;
        public string texturePath;
        public RawRectangleF Collider;

        [XmlIgnore]
        [NonSerialized]
        public SharpDX.Direct2D1.Bitmap Texture;
     
        

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
    }
}
