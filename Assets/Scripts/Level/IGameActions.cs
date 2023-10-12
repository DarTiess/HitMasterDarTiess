namespace Infrastructure.Level
{
    public interface IGameActions
    {
        void OnLevelStart();
        void OnPauseGame();
        void OnPlayGame();
        void OnRestartGame();
        void OnFinishingGame();
    }
}