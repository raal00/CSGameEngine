using System;

namespace GameLib
{
    public interface IGameApi
    {
        void Run(string formName);
        void Pause();
        void Exit();

    }
}
