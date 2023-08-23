using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Infrastructure.GameFSM.FSM;
using CodeBase.Infrastructure.GameFSM.States.Base;

namespace CodeBase.Infrastructure.GameFSM.States
{
    public class BattleState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ITurnQueue _turnQueue;

        public BattleState(IGameStateMachine gameStateMachine, ITurnQueue turnQueue)
        {
            _gameStateMachine = gameStateMachine;
            _turnQueue = turnQueue;
        }

        public void Enter()
        {
            _turnQueue.SetFirstTurnActiveGroup();
            _gameStateMachine.EnterState<ResultsState>();
        }

        public void Exit() => 
            _turnQueue.CleanUp();
    }
}