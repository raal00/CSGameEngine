using System;

namespace GameLib.Models
{
    public class Size
    {
        public int Width;
        public int Height;

        public Size() 
        {
            Width = 100;
            Height = 100;
        }

        public Size(int w, int h) 
        {
            Width = w;
            Height = h;
        }
    }
}
