using System;
using Bullets;
using DG.Tweening;
using Enemy;
using UnityEngine;

namespace Player.States
{
    public class StateAttack : State, IDisposable
    {
        private readonly float _attackRadius;
        private ObjectPoole<Bullet> _bulletList;
        private readonly Transform _bulletPosition;
        private float _hitDuration;
        private readonly Transform _originTransform;
        private IEnemySpawner _enemySpawner;
        private bool _allEnemiesDied;
        private Camera _camera;
        public event Action Finishing;

        public StateAttack(StateMachine stateMachine, PlayerAnimator animator,Transform originTransform,
                           Transform bulletPosition, BulletConfig bulletConfig, IEnemySpawner enemySpawner) : 
        base
        (stateMachine, 
                                                                                                                                                     animator)
        {
            _originTransform = originTransform;
            _bulletPosition = bulletPosition;
            _hitDuration =bulletConfig.HitDuration;
            _enemySpawner = enemySpawner;
            _enemySpawner.EnemiesDied += OnHitEnemies;
            _enemySpawner.AllEnemiesDied += OnFinishing;
            _bulletList = new ObjectPoole<Bullet>();
            _bulletList.CreatePool(bulletConfig.BulletPrefab,bulletConfig.BulletCount,bulletPosition);
            _camera = Camera.main;
        }

        public void Dispose()
        {
            _enemySpawner.EnemiesDied -= OnHitEnemies;
            _enemySpawner.AllEnemiesDied -= OnFinishing;
        }

        public override void Enter(Transform target)
        {
            _animator.AttackAnimation();
            if (_allEnemiesDied)
            {
                Finishing?.Invoke();
            }
        }

        public override void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100000f))
                {
                    PushBullet(hit.point);
                    var look = Quaternion.LookRotation(new Vector3(hit.point.x, _bulletPosition.position.y, hit.point.z) - _bulletPosition.position);

                    _originTransform.DORotateQuaternion(look, 0.5f);
                }
            }
        }

        private void OnFinishing()
        {
            _allEnemiesDied = true;
        }

        private void OnHitEnemies()
        {
            _stateMachine.SetState<StateIdle>(_originTransform);
        }

        private void PushBullet(Vector3 hit)
        {
            Bullet bullet = _bulletList.GetObject();
            bullet.Initialize(_hitDuration);
            bullet.transform.SetPositionAndRotation(_bulletPosition.position, _bulletPosition.rotation);
            bullet.Move(hit);
        }
    }
}