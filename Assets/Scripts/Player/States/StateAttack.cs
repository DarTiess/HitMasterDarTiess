using UnityEngine;

namespace Player.States
{
    public class StateAttack : State
    {
        private readonly float _attackRadius;

        public StateAttack(StateMachine stateMachine, PlayerAnimator animator) : base(stateMachine, animator)
        {
            
        }

        public override void Enter(Transform target)
        {
            Debug.Log("StateAtttack");
            //получаем таргет, и отмеряе  вокруг радиус, по которому стреляем
            
        }
    }
}