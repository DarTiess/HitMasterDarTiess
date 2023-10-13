using Bullets;
using Enemy;
using Level;
using Player.States;
using UnityEngine;
using UnityEngine.AI;
using StateMachine = Player.States.StateMachine;

namespace Player
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Animator))]
    public class PlayerContainer : MonoBehaviour
    {
        [SerializeField] private Transform _bulletPosition;
        private StateMachine _stateMachine;
        private PlayerAnimator _playerAnimator;
        private NavMeshAgent _navMesh;
        private Animator _animator;
        private IGameEvents _gameEvents;
        private IGameActions _gameActions;
        private bool _playing;
        private  StateAttack _attackState;

        public void Initialize(IGameEvents gameEvents, IGameActions gameAction, IWayPoints waypoint, IEnemySpawner enemySpawner,
                               float walkSpeed, float stopDistance, 
                               BulletConfig bulletConfig)
        {
            _gameEvents = gameEvents;
            _gameActions = gameAction;
            _gameEvents.PlayGame += OnPlayGame;
            _navMesh = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            _playerAnimator = new PlayerAnimator(_animator);
            CreateStates(waypoint, enemySpawner, walkSpeed, stopDistance, bulletConfig);
        }

        private void Update()
        {
            if (!_playing)
            {
                return;
            }
            _stateMachine.Update();
        }

        private void OnDisable()
        {
            _gameEvents.PlayGame -= OnPlayGame;
            _attackState.Finishing -= OnFinishing;
        }

        private void CreateStates(IWayPoints waypoint, IEnemySpawner enemySpawner, float walkSpeed, float stopDistance, BulletConfig bulletConfig)
        {
            _stateMachine = new StateMachine();
            _stateMachine.AddState(new StateIdle(_stateMachine, _playerAnimator, transform, waypoint));
            _stateMachine.AddState(new StateMove(_stateMachine, _playerAnimator, _navMesh, transform, walkSpeed, stopDistance)); 
            _attackState=new StateAttack(_stateMachine, _playerAnimator, transform, _bulletPosition, bulletConfig, enemySpawner);
            _stateMachine.AddState(_attackState);
            _stateMachine.SetState<StateIdle>(waypoint.GetWayPoint());

            _attackState.Finishing += OnFinishing;
        }

        private void OnFinishing()
        {
            _gameActions.OnFinishingGame();
        }

        private void OnPlayGame()
        {
            _playing = true;
        }
    }
}
