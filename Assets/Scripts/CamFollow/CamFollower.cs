using Level;
using UnityEngine;

namespace CamFollow {
    public class CamFollower : MonoBehaviour
    {
        [SerializeField] private float _speedVector;
        [SerializeField] private float _vectorY = 10;
        [SerializeField] private float _vectorZ = 10;
        [SerializeField] private float _vectorX;
        [SerializeField] private bool _vectorXFrom0;
        [SerializeField] private bool _lookAtTarget;
        
        [SerializeField] private bool _dropTarget;
        private Transform _target;
        private Vector3 _temp;

        private IGameEvents _gameEventsActions;

        public void Init(IGameEvents gameEvents, Transform player)
        {
            _gameEventsActions = gameEvents;
            _target = player;
            _gameEventsActions.FinishGame += OnFinishGame;
        } 
    
       private void OnFinishGame()
        {
            SetStop();
        }
        private void LateUpdate()   
        {                 
            if (!_target) return;
            MoveVector();  
        }

       private void MoveVector()
        {
            _temp = _target.position;
            _temp.y += _vectorY;
            _temp.z -= _vectorZ;
            _temp.x = _vectorXFrom0 ? _vectorX : _temp.x + _vectorX ;   
                                  
            transform.position = Vector3.Lerp(transform.position,_temp,_speedVector * Time.fixedDeltaTime);
            if (_lookAtTarget) transform.LookAt(_target.position); 
        }
       
        public void SetStop()
        {
            if(!_dropTarget) return;  
            _target = null;  
        }

    }
}