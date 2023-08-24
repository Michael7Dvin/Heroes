using CodeBase.Gameplay.Services.MapInteractor;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Infrastructure.GameFSM.FSM;
using CodeBase.Infrastructure.GameFSM.States.Base;
using CodeBase.Infrastructure.Services.InputService;
using CodeBase.Infrastructure.Services.SceneLoader;

namespace CodeBase.Infrastructure.GameFSM.States
{
    public class InitializationState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IMapInteractor _mapInteractor;
        private readonly IInputService _inputService;
        private readonly ITurnQueue _turnQueue;

        public InitializationState(IGameStateMachine gameStateMachine,
            ISceneLoader sceneLoader,
            IMapInteractor mapInteractor,
            IInputService inputService,
            ITurnQueue turnQueue)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _mapInteractor = mapInteractor;
            _inputService = inputService;
            _turnQueue = turnQueue;
        }

        public void Enter()
        {
            _sceneLoader.Initialize();
            _mapInteractor.Initialize();
            _inputService.Initialize();
            _turnQueue.Initialize();
            
            _gameStateMachine.EnterState<WarmUppingState>();
        }

        public void Exit()
        {
        }
    }
}