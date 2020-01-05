using System;

namespace GameLib.Interaction
{
    public interface IObserverable
    {
        void AddObserver(IObserver observer);
        void RemoveObserver(IObserver observer);
        void Notify(int status);
    }
}
