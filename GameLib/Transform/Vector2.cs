using System;

namespace GameLib.Transform
{
    public class Vector2
    {
        public float X;
        public float Y;

        public Vector2() { }
        public Vector2(float x, float y) 
        {
            X = x;
            Y = y;
        }
        public static Vector2 operator ^(Vector2 pos, Vector2 move)
        {
            pos.X += move.X;
            pos.Y -= move.Y;
            return pos;
        }
    }
}
