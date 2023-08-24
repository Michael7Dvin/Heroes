using CodeBase.Infrastructure.GameFSM.FSM;
using CodeBase.Infrastructure.GameFSM.States;
using Zenject;

namespace CodeBase.Infrastructure
{
    public class Bootstrapper : IInitializable
    {
        private readonly IGameStateMachine _gameStateMachine;

        public Bootstrapper(IGameStateMachine gameStateMachine,
            InitializationState initializationState,
            WarmUppingState warmUppingState,
            LevelLoadingState levelLoadingState,
            UnitsPlacingState unitsPlacingState,
            BattleState battleState,
            RestartState restartState)
        {
            _gameStateMachine = gameStateMachine;
            
            _gameStateMachine.AddState(initializationState);
            _gameStateMachine.AddState(warmUppingState);
            _gameStateMachine.AddState(levelLoadingState);
            _gameStateMachine.AddState(unitsPlacingState);
            _gameStateMachine.AddState(battleState);
            _gameStateMachine.AddState(restartState);
        }
        
        public void Initialize() => 
            _gameStateMachine.EnterState<InitializationState>();
    }
}