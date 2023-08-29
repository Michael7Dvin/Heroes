using CodeBase.Gameplay.Services.TeamWinObserver;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Infrastructure.GameFSM.FSM;
using CodeBase.Infrastructure.GameFSM.States.Base;
using CodeBase.Infrastructure.Services.InputService;
using CodeBase.Infrastructure.Services.SceneLoader;
using CodeBase.Infrastructure.Services.Updater;

namespace CodeBase.Infrastructure.GameFSM.States
{
    public class InitializationState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IInputService _inputService;
        private readonly IUpdater _updater;
        private readonly IWinService _winService;
        private readonly ITurnQueue _turnQueue;

        public InitializationState(IGameStateMachine gameStateMachine,
            ISceneLoader sceneLoader,
            IInputService inputService,
            IUpdater updater,
            IWinService winService,
            ITurnQueue turnQueue)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _inputService = inputService;
            _updater = updater;
            _winService = winService;
            _turnQueue = turnQueue;
        }

        public void Enter()
        {
            _sceneLoader.Initialize();
            _inputService.Initialize();
            _winService.Initialize();
            _turnQueue.Initialize();
            
            _updater.StartUpdating();
            
            _gameStateMachine.EnterState<WarmUppingState>();
        }

        public void Exit()
        {
        }
    }
}