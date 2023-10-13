using UnityEngine;

namespace Enemy
{
    public class EnemyAnimator
    {
        private Animator _animator;
        private readonly int _countClips;
        private static readonly int IDLE = Animator.StringToHash("Idle");

        public EnemyAnimator(Animator animator, int countClips)
        {
            _animator = animator;
            _countClips = countClips;
            RandomIdleAnimation();
        }

        private void RandomIdleAnimation()
        {
            float randomAnimation = Random.Range(0, _countClips);
            _animator.SetFloat(IDLE, randomAnimation);
        }
    }
}