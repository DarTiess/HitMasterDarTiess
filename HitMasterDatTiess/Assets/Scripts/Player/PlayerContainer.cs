using Infrastructure.Level;
using Player.States;
using UnityEngine;
using UnityEngine.AI;
using StateMachine = Player.States.StateMachine;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class PlayerContainer : MonoBehaviour
{
    private StateMachine _stateMachine;
    private PlayerAnimator _playerAnimator;
    private NavMeshAgent _navMesh;
    private Animator _animator;
    private IGameEvents _gameEvents;
    private bool _playing;

    public void Initialize(IGameEvents gameEvents, IWayPoints waypoint,float walkSpeed, float stopDistance)
    {
        _gameEvents = gameEvents;
        _gameEvents.PlayGame += OnPlayGame;
        _navMesh = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _playerAnimator = new PlayerAnimator(_animator);
        _stateMachine = new StateMachine();
        _stateMachine.AddState(new StateIdle(_stateMachine, _playerAnimator, transform, waypoint));
        _stateMachine.AddState(new StateMove(_stateMachine, _playerAnimator,_navMesh,transform,walkSpeed,stopDistance));
        _stateMachine.AddState(new StateAttack(_stateMachine, _playerAnimator));
        _stateMachine.SetState<StateIdle>(waypoint.GetWayPoint());
    }

    private void OnPlayGame()
    {
        _playing = true;
    }

    private void Update()
    {
        if (!_playing)
        {
            return;
        }
        _stateMachine.Update();
    }
}
