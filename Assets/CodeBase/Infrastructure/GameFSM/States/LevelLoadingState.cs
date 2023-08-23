using CodeBase.Gameplay.Groups;
using CodeBase.Gameplay.Services.GroupFactory;
using CodeBase.Gameplay.Services.MapFactory;
using CodeBase.Gameplay.Services.MapService;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Infrastructure.GameFSM.FSM;
using CodeBase.Infrastructure.GameFSM.States.Base;
using CodeBase.Infrastructure.Services.SceneLoading;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace CodeBase.Infrastructure.GameFSM.States
{
    public class LevelLoadingState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly ITileMapFactory _tileMapFactory;
        private readonly ITurnQueue _turnQueue;
        private readonly IGroupFactory _groupFactory;
        private readonly IMapService _mapService;

        public LevelLoadingState(IGameStateMachine gameStateMachine, ISceneLoader sceneLoader, ITileMapFactory tileMapFactory, ITurnQueue turnQueue, IGroupFactory groupFactory, IMapService mapService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _tileMapFactory = tileMapFactory;
            _turnQueue = turnQueue;
            _groupFactory = groupFactory;
            _mapService = mapService;
        }

        public async void Enter()
        {
            await _sceneLoader.Load(SceneID.BattleField);

            Tilemap tilemap = await _tileMapFactory.Create();
            _mapService.Reset(tilemap);

            _turnQueue.Initialize();

            Debug.Log(_mapService.IsTileOccupied(new Vector3Int(0, 0, 0)));
            UnitsGroup zombies = _groupFactory.Create(UnitType.Zombie, 1);
            _mapService.OccupyTile(new Vector3Int(0, 0, 0), zombies);
            Debug.Log(_mapService.IsTileOccupied(new Vector3Int(0, 0, 0)));

            _mapService.TryGetUnitsGroupAtTile(new Vector3Int(0, 0, 0), out UnitsGroup unitAtTile);
            Debug.Log(unitAtTile.UnitType);
            
            _mapService.ReleaseTile(new Vector3Int(0, 0, 0));
            Debug.Log(_mapService.IsTileOccupied(new Vector3Int(0, 0, 0)));
            
            Debug.Log(_mapService.TryGetUnitsGroupAtTile(new Vector3Int(0, 0, 0), out unitAtTile));
            Debug.Log(unitAtTile);
            
            Debug.Log(_mapService.GetTileCenter(tilemap.cellBounds.min));
            
            _gameStateMachine.EnterState<BattleState>();
        }

        public void Exit()
        {
        }
    }
}