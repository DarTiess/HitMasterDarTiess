using UnityEngine;

namespace Level
{
    public class StartGame : MonoBehaviour
    {
        public LevelLoader LevelLoader;
        
        private void Awake()
        {
            LevelLoader.StartGame();    
        }
      
    }
}
