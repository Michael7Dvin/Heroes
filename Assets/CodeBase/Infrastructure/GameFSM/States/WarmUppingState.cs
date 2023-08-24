using CodeBase.Infrastructure.GameFSM.FSM;
using CodeBase.Infrastructure.GameFSM.States.Base;
using CodeBase.Infrastructure.Services.CameraFactory;
using CodeBase.Infrastructure.Services.TileMapFactory;
using CodeBase.Infrastructure.Services.UnitFactory;

namespace CodeBase.Infrastructure.GameFSM.States
{
    public class WarmUppingState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ITileMapFactory _tileMapFactory;
        private readonly IUnitFactory _unitFactory;
        private readonly ICameraFactory _cameraFactory;

        public WarmUppingState(IGameStateMachine gameStateMachine, ITileMapFactory tileMapFactory, IUnitFactory unitFactory, ICameraFactory cameraFactory)
        {
            _gameStateMachine = gameStateMachine;
            _tileMapFactory = tileMapFactory;
            _unitFactory = unitFactory;
            _cameraFactory = cameraFactory;
        }

        public async void Enter()
        {
            await _tileMapFactory.WarmUp();
            await _unitFactory.WarmUp();
            await _cameraFactory.WarmUp();
            
            _gameStateMachine.EnterState<LevelLoadingState>();
        }

        public void Exit()
        {
        }
    }
}