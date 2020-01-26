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
        public bool Breakable = false;
        public bool HideOnApproach = false;
        public int  deep;
        public float Opacity = 1;

        public Barrier() 
        {
            objectTag = Tag.ENVIRONMENT;
        }
    }
}
