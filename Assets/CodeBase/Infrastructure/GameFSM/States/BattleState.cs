using CodeBase.Infrastructure.GameFSM.FSM;
using CodeBase.Infrastructure.GameFSM.States.Base;

namespace CodeBase.Infrastructure.GameFSM.States
{
    public class BattleState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;

        public BattleState(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
        
        public void Enter()
        {
            _gameStateMachine.EnterState<ResultsState>();
        }

        public void Exit()
        {
        }
    }
}