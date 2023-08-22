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
            LevelLoadingState levelLoadingState,
            BattleState battleState,
            ResultsState resultsState)
        {
            _gameStateMachine = gameStateMachine;
            
            _gameStateMachine.AddState(initializationState);
            _gameStateMachine.AddState(levelLoadingState);
            _gameStateMachine.AddState(battleState);
            _gameStateMachine.AddState(resultsState);
        }
        
        public void Initialize() => 
            _gameStateMachine.EnterState<InitializationState>();
    }
}