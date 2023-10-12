using UnityEngine;

namespace Player.States
{
    public abstract class State
    {
        protected readonly StateMachine _stateMachine;
        protected readonly PlayerAnimator _animator;

        public State(StateMachine stateMachine,PlayerAnimator animator)
        {
            _stateMachine = stateMachine;
            _animator = animator;
        }

        public virtual void Enter(Transform target){}
        public virtual void Exit(){}
        public virtual void Update(){}
    }
}