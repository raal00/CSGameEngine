using GameLib.Enums;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;
using System;

namespace GameLib.Models
{
    public interface IGameObject
    {
        void SetTexture(WindowRenderTarget target);
    }
}
