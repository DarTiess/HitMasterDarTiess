using System;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(HealthBar))]
    public class EnemyContainer : MonoBehaviour, IDamageable
    {
        [SerializeField] private GameObject _rig;
        
        private Animator _animator;
        private CapsuleCollider _mainCollider;
        private EnemyAnimator _enemyAnimator;
        private Rigidbody _mainRigidbody;
        private HealthBar _healthBar;
        private Collider[] _ragDollColliders;
        private Rigidbody[] _ragDollRigidbodies;
         
        public event Action<EnemyContainer> Dying;

        public void Initialize(EnemyConfig config)
        {
            _animator = GetComponent<Animator>();
            _mainRigidbody = GetComponent<Rigidbody>();
            _mainCollider = GetComponent<CapsuleCollider>();
            _healthBar = GetComponent<HealthBar>();
            _healthBar.Initialize(config.Health, config.HealthBarDuration);
            GetRagdollParts();
            RegDollModeOff(true);
            _enemyAnimator = new EnemyAnimator(_animator, config.CountClipsAnimation);
        }

        public void TakeDamage()
        {
            RegDollModeOff(false);
            _healthBar.GetDamage();
            _animator.enabled = false;
            Dying?.Invoke(this);
        }

        private void GetRagdollParts()
        {
            _ragDollColliders = _rig.GetComponentsInChildren<Collider>();
            _ragDollRigidbodies = _rig.GetComponentsInChildren<Rigidbody>();
        }

        private void RegDollModeOff(bool active)
        {
            foreach (Collider collider in _ragDollColliders)
            {
                collider.enabled = !active;
            }

            foreach (Rigidbody rigidbody in _ragDollRigidbodies)
            {
                rigidbody.isKinematic = active; 
            }

            _animator.enabled = active;
            _mainCollider.enabled = active;
            _mainRigidbody.isKinematic = !active;
        }
    }
}