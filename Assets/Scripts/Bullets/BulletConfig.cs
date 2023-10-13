using UnityEngine;

namespace Bullets
{
    [CreateAssetMenu(menuName = "Configs/Bullets", fileName = "BulletConfigs", order = 52)]
    public class BulletConfig: ScriptableObject
    {
     [SerializeField] private Bullet _bulletPrefab;
     [SerializeField] private int _bulletCount;
     [SerializeField] private float _hitDuration;

     public Bullet BulletPrefab => _bulletPrefab;
     public int BulletCount => _bulletCount;
     public float HitDuration => _hitDuration;
    }
}