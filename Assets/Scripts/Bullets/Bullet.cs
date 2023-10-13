using System;
using DG.Tweening;
using Enemy;
using UnityEngine;

namespace Bullets
{
    public class Bullet: MonoBehaviour
    {
        private float _hitDuration;

        public void Initialize(float hitDuration)
        {
            _hitDuration = hitDuration;
        }

        private void OnDisable()
        {
            transform.DOKill(this);
        }

        public void Move(Vector3 target)
        {
           transform.DOMove(target, _hitDuration);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.TryGetComponent(out IDamageable enemy))
            {
                enemy.TakeDamage();
            }
            gameObject.SetActive(false);
        }
    }
}