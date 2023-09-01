using System.Collections.Generic;
using CodeBase.Gameplay.Level;
using CodeBase.Gameplay.Services.MapGenerator;
using CodeBase.Gameplay.Services.MapService;
using CodeBase.Gameplay.Tiles;
using CodeBase.Infrastructure.GameFSM.FSM;
using CodeBase.Infrastructure.GameFSM.States.Base;
using CodeBase.Infrastructure.Services.CameraFactory;
using CodeBase.Infrastructure.Services.SceneLoader;
using CodeBase.Infrastructure.Services.StaticDataProvider;
using CodeBase.UI.Services.UiUtilitiesFactory;
using CodeBase.UI.Services.WindowsFactory;
using CodeBase.UI.Windows;

namespace CodeBase.Infrastructure.GameFSM.States
{
    public class LevelLoadingState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IMapGenerator _mapGenerator;
        private readonly IMapService _mapService;
        private readonly ICameraFactory _cameraFactory;
        private readonly IUiUtilitiesFactory _uiUtilitiesFactory;
        private readonly IWindowsFactory _windowsFactory;
        
        private readonly LevelConfig _levelConfig;

        public LevelLoadingState(IGameStateMachine gameStateMachine,
            ISceneLoader sceneLoader,
            IMapGenerator mapGenerator,
            IMapService mapService,
            ICameraFactory cameraFactory,
            IUiUtilitiesFactory uiUtilitiesFactory,
            IWindowsFactory windowsFactory,
            IStaticDataProvider staticDataProvider)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _mapGenerator = mapGenerator;
            _mapService = mapService;
            _cameraFactory = cameraFactory;
            _uiUtilitiesFactory = uiUtilitiesFactory;
            _windowsFactory = windowsFactory;

            _levelConfig = staticDataProvider.Configs.Level;
        }

        public async void Enter()
        {
            await _sceneLoader.Load(SceneID.BattleField);

            IEnumerable<Tile> map = await _mapGenerator.Generate();
            _mapService.ResetMap(map);

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