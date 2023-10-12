using UnityEngine;

namespace Player.States
{
    public class PlayerAnimator
    {
        private readonly Animator _animator;
        private static readonly int WALK = Animator.StringToHash("Walk");
        private static readonly int ATTACK = Animator.StringToHash("Attack");
        private static readonly int IDLE = Animator.StringToHash("Idle");

        public PlayerAnimator(Animator animator)
        {
            _animator = animator;
        }
        
        public void WalkAnimation()
        {
            Debug.Log("Walk Anim");
            _animator.SetBool(IDLE, false);
            _animator.SetBool(WALK, true);
        }

        public void IdleAnimation()
        {
            Debug.Log("Idle Anim");
            _animator.SetBool(IDLE, true);
            _animator.SetBool(WALK, false);
        }

        public void AttackAnimation()
        {
            Debug.Log("Attack Anim");
            _animator.SetBool(WALK, false);
            _animator.SetBool(ATTACK, true);
        }
    }
}