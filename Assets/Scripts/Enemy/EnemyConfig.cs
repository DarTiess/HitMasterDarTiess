using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(menuName = "Configs/Enemy", fileName = "EnemyConfig", order = 52)]
    public class EnemyConfig: ScriptableObject
    {
        [SerializeField] private EnemyContainer _enemyPrefab;
        [SerializeField] private int _countClipsAnimation;
        [SerializeField] private float _health;
        [SerializeField] private float _healthBarDuration;

        public EnemyContainer EnemyPrefab => _enemyPrefab;
        public int CountClipsAnimation => _countClipsAnimation;
        public float Health => _health;
        public float HealthBarDuration => _healthBarDuration;
    }
}