using System;
using System.Collections.Generic;
using Infrastructure.Level;
using UnityEngine;

namespace DefaultNamespace
{
    public class EnemySpawner: MonoBehaviour
    {
        private List<EnemiesWave> _waves;

        private EnemiesWave _currentEnemiesWave;
        private ObjectPoole<EnemyContainer> _pooler;
        private int _currentWaveIndex = 0;
        private EnemyContainer _enemyPrefab;
        private IGameEvents _gameEvents;

        public void Initialize(EnemyContainer enemyPrefab, List<EnemiesWave> enemiesWaves, IGameEvents gameEvents )
        {
            _pooler = new ObjectPoole<EnemyContainer>();
            _waves = enemiesWaves;
            _enemyPrefab = enemyPrefab;
            _gameEvents = gameEvents;
            _gameEvents.PlayGame += OnPlayGame;
        }

        private void OnPlayGame()
        {
            _currentEnemiesWave = _waves[_currentWaveIndex];
            foreach (EnemiesWave wave in _waves)
            {
                SetWaves(wave);
            }
        }

        private void Update()
        {
            if (_currentEnemiesWave == null)
            {
                return;
            }

            InstantiateEnemy();
        }

        private void InstantiateEnemy()
        {
            foreach (var position in _currentEnemiesWave.Positions)
            {
                EnemyContainer enemy = _pooler.GetObject();
                if (enemy != null)
                {
                    enemy.transform.SetPositionAndRotation(position.position, position.rotation);
                    //  enemy.Init(_player);
                    // enemy.Dying += OnEnemyDying;
                }
            }
           
        }

        private void SetWaves(EnemiesWave wave)
        {
            _pooler.CreatePool(_enemyPrefab, wave.Positions.Length, transform);
        }

    }
}