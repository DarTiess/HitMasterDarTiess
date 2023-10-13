using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

namespace Player.States
{
    public class StateMove: State
    {
        private Transform _target;
        private NavMeshAgent _navMesh;
        private Transform _transform;
        private float _stopDistance;
        

        public StateMove(StateMachine stateMachine, PlayerAnimator animator,
                         NavMeshAgent navMesh, Transform transform,
                         float speed, float stopDistance) : base(stateMachine, animator)
        {
            _navMesh = navMesh;
           
            _navMesh.speed = speed;
            _transform = transform;
            _stopDistance = stopDistance;
        }
        public override void Enter(Transform target)
        {
            _navMesh.isStopped = false;
            _animator.WalkAnimation();
            _target = target;
        }

        public override void Update()
        {
            if (_target == null)
            {
                return;
            }
            _navMesh.SetDestination(_target.position);
            if (Vector3.Distance(_transform.position, _target.transform.position) <= _stopDistance)
            {
                _stateMachine.SetState<StateAttack>(_target);
            }
        }

        public override void Exit()
        {
            _navMesh.isStopped = true;
        }
    }
}