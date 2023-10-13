using UnityEngine;

namespace Player
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
            _animator.SetBool(IDLE, false);
            _animator.SetBool(WALK, true);
            _animator.SetBool(ATTACK, false);
        }

        public void IdleAnimation()
        {
            _animator.SetBool(IDLE, true);
            _animator.SetBool(WALK, false);
            _animator.SetBool(ATTACK, false);
        }

        public void AttackAnimation()
        {
            _animator.SetBool(WALK, false);
            _animator.SetBool(ATTACK, true);
        }
    }
}