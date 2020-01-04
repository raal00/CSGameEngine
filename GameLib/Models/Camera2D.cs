using SharpDX;
using System;

namespace GameLib.Models
{
    public class Camera2D
    {
        float Rotation = 0;
        public Vector2 camPos = new Vector2(0,0);

        public float Zoom = 1;

        public int ViewportWidth;
        public int ViewportHeight;

        public Vector2 ViewportCenter { get { return new Vector2(ViewportWidth * 0.5f, ViewportHeight * 0.5f); } }

        public Matrix3x2 GetTransform3x2()
        {
            return Translation;
        }

        private Matrix3x2 Translation
        {
            get
            {
                return Matrix3x2.Translation(-camPos.X, -camPos.Y) *
                       Matrix3x2.Rotation(Rotation) *
                       Matrix3x2.Scaling(Zoom) *
                       Matrix3x2.Translation(ViewportCenter); ;
            }
        }

        public void MoveCamera(Vector2 position)
        {
            Vector2 newPosition = camPos + position;
            camPos = newPosition;
        }

        public Camera2D(int viewportWidth, int viewportHeight)
        {
            ViewportWidth = viewportWidth;
            ViewportHeight = viewportHeight;
        }
    }
}
