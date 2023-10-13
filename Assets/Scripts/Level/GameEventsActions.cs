using System;

namespace Level
{
    public class GameEventsActions : IGameActions, IGameEvents
    {
        private bool _onPaused;
        private ILevelLoader _levelLoader;
      
        public event Action LevelStart;
        public event Action FinishGame;
        public event Action PlayGame;
        public event Action StopGame;

        public GameEventsActions(ILevelLoader levelLoader)
        {
            _levelLoader = levelLoader;
            OnLevelStart();
        }
    
        public void OnLevelStart()
        {
            LevelStart?.Invoke();
        }

        public void OnPlayGame()
        {
            PlayGame?.Invoke();
        }

        public void OnPauseGame()
        {
            if (!_onPaused)
            {
                StopGame?.Invoke();
                _onPaused = true;
            }
            else
            {
                OnPlayGame();
                _onPaused = false;
            }
        }

        public void OnFinishingGame()
        {
            FinishGame?.Invoke();
        }

        public void OnRestartGame()
        {
            _levelLoader.RestartScene();
        }
    }
}
