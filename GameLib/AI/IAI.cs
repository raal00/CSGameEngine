using GameLib.Models;
using GameLib.Transform;
using System;
using System.Collections.Generic;

namespace GameLib.AI
{
    public interface IAI
    {
        Level lvl { get; }
        void MoveTo(Vector2 EndPoint);
        void ContactWithMainHero();

    }
}
