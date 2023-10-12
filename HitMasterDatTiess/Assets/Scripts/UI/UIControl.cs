using Infrastructure.Level;
using UI.UIPanels;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class UIControl : MonoBehaviour
    {
        [Header("Panels")]
        [SerializeField] private StartMenu _panelMenu;
        [SerializeField] private GamePanel _panelInGame;
        [SerializeField] private LostPanel _panelFinished;

        private IGameActions _gameActions;
        private IGameEvents _gameEvents;
       
        public void Init(IGameActions gameActions, IGameEvents gameEvents)
        {
            _gameActions = gameActions;
            _gameEvents = gameEvents;
         
     
            _gameEvents.LevelStart += OnGameStart;
            _gameEvents.FinishGame += OnFinishGame;

            _panelMenu.ClickedPanel += OnPlayGame;
            _panelFinished.ClickedPanel += OnRestartGame;
            _panelInGame.ClickedPanel += OnPauseGame;
          
            OnGameStart();
        }

        private void OnDisable()
        {
            _gameEvents.LevelStart -= OnGameStart;
          
            _panelMenu.ClickedPanel -= OnPlayGame;
            _panelFinished.ClickedPanel -= OnRestartGame;
            _panelInGame.ClickedPanel -= OnPauseGame;
           
        }

        private void OnGameStart()      
        {   
            HideAllPanels();
            _panelMenu.Show();
        }

        private void OnFinishGame()
        {
            Debug.Log("Level Finished");  
            HideAllPanels();
            _panelFinished.Show();
        }

        private void OnPauseGame()
        {
            _gameActions.OnPauseGame();
        }

        private void OnPlayGame()
        { 
            _gameActions.OnPlayGame();
            HideAllPanels(); 
            _panelInGame.Show();         
        }

        private void OnRestartGame()
        {
            _gameActions.OnRestartGame();
        }

        private void HideAllPanels()
        {
            _panelMenu.Hide();
            _panelFinished.Hide();
            _panelInGame.Hide();
        }
    
    }
}
