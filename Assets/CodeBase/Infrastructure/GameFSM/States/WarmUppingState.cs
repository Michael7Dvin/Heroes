using CodeBase.Infrastructure.GameFSM.FSM;
using CodeBase.Infrastructure.GameFSM.States.Base;
using CodeBase.Infrastructure.Services.CameraFactory;
using CodeBase.Infrastructure.Services.TileFactory;
using CodeBase.Infrastructure.Services.UnitFactory;
using CodeBase.UI.Services.UiUtilitiesFactory;
using CodeBase.UI.Services.WindowsFactory;

namespace CodeBase.Infrastructure.GameFSM.States
{
    public class WarmUppingState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ITileFactory _tileFactory;
        private readonly IUnitFactory _unitFactory;
        private readonly ICameraFactory _cameraFactory;
        private readonly IUiUtilitiesFactory _uiUtilitiesFactory;
        private readonly IWindowsFactory _windowsFactory;

        public WarmUppingState(IGameStateMachine gameStateMachine,
            ITileFactory tileFactory,
            IUnitFactory unitFactory,
            ICameraFactory cameraFactory,
            IUiUtilitiesFactory uiUtilitiesFactory,
            IWindowsFactory windowsFactory)
        {
            _gameStateMachine = gameStateMachine;
            _tileFactory = tileFactory;
            _unitFactory = unitFactory;
            _cameraFactory = cameraFactory;
            _uiUtilitiesFactory = uiUtilitiesFactory;
            _windowsFactory = windowsFactory;
        }

        public async void Enter()
        {
            await _tileFactory.WarmUp();
            await _unitFactory.WarmUp();
            await _cameraFactory.WarmUp();
            await _uiUtilitiesFactory.WarmUp();
            await _windowsFactory.WarmUp();
            
            _gameStateMachine.EnterState<LevelLoadingState>();
        }

        public void Exit()
        {
        }
    }
}