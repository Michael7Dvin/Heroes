using CodeBase.Gameplay.Level;
using CodeBase.Gameplay.Services.MapService;
using CodeBase.Infrastructure.GameFSM.FSM;
using CodeBase.Infrastructure.GameFSM.States.Base;
using CodeBase.Infrastructure.Services.CameraFactory;
using CodeBase.Infrastructure.Services.SceneLoader;
using CodeBase.Infrastructure.Services.StaticDataProvider;
using CodeBase.Infrastructure.Services.TileMapFactory;
using CodeBase.UI.Services.UiUtilitiesFactory;
using CodeBase.UI.Services.WindowsFactory;
using CodeBase.UI.Windows;
using UnityEngine.Tilemaps;

namespace CodeBase.Infrastructure.GameFSM.States
{
    public class LevelLoadingState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly ITileMapFactory _tileMapFactory;
        private readonly IMapService _mapService;
        private readonly ICameraFactory _cameraFactory;
        private readonly IUiUtilitiesFactory _uiUtilitiesFactory;
        private readonly IWindowsFactory _windowsFactory;
        
        private readonly LevelConfig _levelConfig;

        public LevelLoadingState(IGameStateMachine gameStateMachine,
            ISceneLoader sceneLoader,
            ITileMapFactory tileMapFactory,
            IMapService mapService,
            ICameraFactory cameraFactory,
            IUiUtilitiesFactory uiUtilitiesFactory,
            IWindowsFactory windowsFactory,
            IStaticDataProvider staticDataProvider)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _tileMapFactory = tileMapFactory;
            _mapService = mapService;
            _cameraFactory = cameraFactory;
            _uiUtilitiesFactory = uiUtilitiesFactory;
            _windowsFactory = windowsFactory;

            _levelConfig = staticDataProvider.LevelConfig;
        }

        public async void Enter()
        {
            await _sceneLoader.Load(SceneID.BattleField);
            
            Tilemap tilemap = await _tileMapFactory.Create();
            _mapService.Reset(tilemap);

            await _uiUtilitiesFactory.CreateCanvas();
            await _uiUtilitiesFactory.CreateEventSystem();

            await _windowsFactory.Create(WindowID.BattleField);
            
            await _cameraFactory.Create();

            _gameStateMachine.EnterState<UnitsPlacingState, LevelConfig>(_levelConfig);
        }

        public void Exit()
        {
        }
    }
}