using System;

namespace GameLib.Interaction
{
    public interface IObserver
    {
        void Action(int status);
    }
}
