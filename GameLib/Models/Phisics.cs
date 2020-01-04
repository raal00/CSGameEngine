using GameLib.Transform;
using System;

namespace GameLib.Models
{
    public class Phisics
    {
        private int weight = 2;
        public Vector2 grav;

        public Phisics()
        {
            grav = new Vector2(0, -weight);
        }
    }
}
