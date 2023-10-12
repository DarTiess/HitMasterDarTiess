using TMPro;
using UnityEngine;

namespace Player.States
{
    public class StateIdle : State
    {
        private readonly IWayPoints _waypoint;
        private Transform _target;
        private Transform _transform;

        public StateIdle(StateMachine stateMachine, PlayerAnimator animator, Transform transform, IWayPoints waypoint ) : base(stateMachine, animator)
        {
            _waypoint = waypoint;
            _transform = transform;
        }
        
        public override void Enter(Transform target)
        {
            Debug.Log("State Idle");
            _transform.position = target.position;
            _animator.IdleAnimation();
            
        }

        private void GetNextState()
        {
            Debug.Log("Get Steta");
            _target = _waypoint.GetWayPoint();
            if (_target != null)
            {
                _stateMachine.SetState<StateMove>(_target);
                _target = null;
            }
        }

        public override void Update()
        {
            GetNextState();
        }
    }
}