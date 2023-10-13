using System;
using System.Collections.Generic;
using Level;
using UnityEngine;

namespace Enemy
{
    public class EnemySpawner: MonoBehaviour, IEnemySpawner
    {
        private List<EnemiesWave> _waves;

        private EnemiesWave _currentEnemiesWave;
        private ObjectPoole<EnemyContainer> _pooler;
        private int _currentWaveIndex = 0;
        private EnemyConfig _config;
        private IGameEvents _gameEvents;
        private int _waveSize;
        private int _countClips;
        public event Action EnemiesDied;
        public event Action AllEnemiesDied;

        public void Initialize(EnemyConfig enemyConfig, List<EnemiesWave> enemiesWaves, IGameEvents gameEvents )
        {
            _pooler = new ObjectPoole<EnemyContainer>();
            _waves = enemiesWaves;
            _gameEvents = gameEvents;
            _config = enemyConfig;
            _currentEnemiesWave = _waves[_currentWaveIndex];
            foreach (EnemiesWave wave in _waves)
            {
                SetWaves(wave);
            }
            _gameEvents.PlayGame += OnPlayGame;
        }

        private void OnDisable()
        {
            _gameEvents.PlayGame -= OnPlayGame;
        }

        private void OnPlayGame()
        {
            InstantiateEnemy();
        }

        private void InstantiateEnemy()
        {
            _waveSize = _currentEnemiesWave.Positions.Length;
            foreach (var position in _currentEnemiesWave.Positions)
            {
                EnemyContainer enemy = _pooler.GetObject();
                if (enemy != null)
                {
                    enemy.transform.SetPositionAndRotation(position.position, position.rotation);
                  enemy.Initialize(_config);
                    enemy.Dying += OnEnemyDying;
                }
            }
           
        }

        private void OnEnemyDying(EnemyContainer enemy)
        {
            enemy.Dying -= OnEnemyDying;
            _waveSize-=1;
            if (_waveSize == 0)
            {
                EnemiesDied?.Invoke();
                _currentWaveIndex += 1;
                if (_currentWaveIndex >= _waves.Count)
                {
                    AllEnemiesDied?.Invoke();
                    return;
                }
                _currentEnemiesWave = _waves[_currentWaveIndex];
                InstantiateEnemy();
            }
        }

        private void SetWaves(EnemiesWave wave)
        {
            _pooler.CreatePool(_config.EnemyPrefab, wave.Positions.Length, transform);
        }

    }
}