using GameLib.Models;
using System;
namespace GameLib.Params
{
    public static class MapValues
    {
        // map matrix
        public static byte[,] mapMatrix;
        public static int MatrWidth;
        public static int MatrHeight;

        // camera
        public static Camera2D cam2d;
        public static SharpDX.Vector2 camMove;
    }
}
