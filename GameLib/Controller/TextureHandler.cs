using SharpDX.WIC;
using SharpDX;
using System.Drawing.Imaging;
using System;

namespace GameLib.Controller
{
    public class TextureHandler
    {
        public BitmapSource LoadBMPSFromFile(string path, ImagingFactory factory, ImageFormat format)
        {
            try
            {
                BitmapDecoder bitmapDecoder = new BitmapDecoder(
                        factory,
                        path,
                        SharpDX.WIC.DecodeOptions.CacheOnDemand);

                FormatConverter formatConverter = new FormatConverter(factory);
                formatConverter.Initialize(bitmapDecoder.GetFrame(0),
                        SharpDX.WIC.PixelFormat.Format32bppBGRA,
                        BitmapDitherType.DualSpiral8x8,
                        null,
                        0.5,
                        BitmapPaletteType.Custom);
                return formatConverter;
            }
            catch (Exception er)
            {
                Console.WriteLine($"[Error] {er.Message}");
            }
            return null;
        }

    }
}
