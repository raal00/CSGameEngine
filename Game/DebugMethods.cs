using GameLib.Controller;
using SharpDX.Direct2D1;
using SharpDX.WIC;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public static class DebugMethods
    {
        /// <summary>
        /// load bitmap from image file
        /// </summary>
        /// <param name="target">render target</param>
        /// <param name="texturePath">file path</param>
        /// <returns>bitmap</returns>
        public static SharpDX.Direct2D1.Bitmap loadWallTexture(WindowRenderTarget target, string texturePath)
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
