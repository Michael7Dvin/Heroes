using CodeBase.Gameplay.Level;
using CodeBase.Gameplay.Services.TileMapService;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Infrastructure.GameFSM.FSM;
using CodeBase.Infrastructure.GameFSM.States.Base;
using CodeBase.Infrastructure.Services.SceneLoading;
using CodeBase.Infrastructure.Services.StaticDataProviding;
using CodeBase.Infrastructure.Services.TileMapFactory;
using UnityEngine.Tilemaps;

namespace CodeBase.Infrastructure.GameFSM.States
{
    public class LevelLoadingState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly ITileMapFactory _tileMapFactory;
        private readonly ITurnQueue _turnQueue;
        private readonly IMapService _mapService;
        private readonly LevelConfig _levelConfig;

        public LevelLoadingState(IGameStateMachine gameStateMachine,
            ISceneLoader sceneLoader,
            ITileMapFactory tileMapFactory,
            ITurnQueue turnQueue,
            IMapService mapService,
            IStaticDataProvider staticDataProvider)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _tileMapFactory = tileMapFactory;
            _turnQueue = turnQueue;
            _mapService = mapService;
            
            _levelConfig = staticDataProvider.LevelConfig;
        }

        public async void Enter()
        {
            await _sceneLoader.Load(SceneID.BattleField);
            
            Tilemap tilemap = await _tileMapFactory.Create();
            _mapService.Reset(tilemap);

            _turnQueue.Initialize();

            _gameStateMachine.EnterState<UnitsPlacingState, LevelConfig>(_levelConfig);
        }

        public void Exit()
        {
        }
    }
}