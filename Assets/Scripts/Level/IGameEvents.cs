using System;

namespace Infrastructure.Level
{
    public interface IGameEvents
    {
        event Action LevelStart;
        event Action FinishGame;
        event Action PlayGame;
        event Action StopGame;
    }
}