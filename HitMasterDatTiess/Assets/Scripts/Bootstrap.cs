using System.Collections.Generic;
using CamFollow;
using DefaultNamespace;
using Infrastructure.Level;
using Player.States;
using UI;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [Header("Ui Settings")]
    [SerializeField] private UIControl _canvasPrefab;
    [Space(20)]
    [Header("Level Settings")]
    [SerializeField] private LevelLoader _levelLoader;
    [Space(20)]
    [Header("Player Settings")]
    [SerializeField] private PlayerContainer _playerPrefab;
    [SerializeField] private Transform[] _waypointsList;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _stopDistance;
    [Space(20)]
    [Header("Enemies Settings")]
    [SerializeField] private List<EnemiesWave> _enemyWaves;
    [SerializeField] private EnemyContainer _enemyPrefab;
    [SerializeField] private EnemySpawner _enemySpawnerPrefab;

    private GameEventsActions _gameEventsActions;
    private CamFollower _camFollower;
    private PlayerContainer _player;
    private UIControl _ui;
    private WayPoints _wayPoint;
    private EnemySpawner _enemySpawner;

    private void Awake()
   {
       CreateGameEventsActions();
       CreateUICanvas();

       _wayPoint = new WayPoints(_waypointsList);

       _player = Instantiate(_playerPrefab);
       _player.Initialize(_gameEventsActions,_wayPoint,_walkSpeed, _stopDistance);

       InitCameraFollower();

       _enemySpawner = Instantiate(_enemySpawnerPrefab);
       _enemySpawner.Initialize(_enemyPrefab, _enemyWaves, _gameEventsActions);
   }

   private void InitCameraFollower()
   {
       _camFollower = Camera.main.GetComponent<CamFollower>();
       _camFollower.Init(_gameEventsActions, _player.transform);
   }

   private void CreateUICanvas()
   {
       _ui = Instantiate(_canvasPrefab);
       _ui.Init(_gameEventsActions, _gameEventsActions);
   }

   private void CreateGameEventsActions()
   {
       _gameEventsActions = new GameEventsActions(_levelLoader);
   }
}