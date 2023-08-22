using CodeBase.Gameplay.Groups;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Infrastructure.GameFSM.FSM;
using CodeBase.Infrastructure.GameFSM.States.Base;
using CodeBase.Infrastructure.Services.Logging;

namespace CodeBase.Infrastructure.GameFSM.States
{
    public class BattleState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ITurnQueue _turnQueue;
        private readonly ICustomLogger _logger;

        public BattleState(IGameStateMachine gameStateMachine, ITurnQueue turnQueue, ICustomLogger logger)
        {
            _gameStateMachine = gameStateMachine;
            _turnQueue = turnQueue;
            _logger = logger;
        }

        public void Enter()
        {
            _turnQueue.SetFirstTurnActiveGroup();

            _logger.Log($"Active group: {_turnQueue.ActiveGroup.UnitType}, {_turnQueue.ActiveGroup.Initiative}");
            
            foreach (Group group in _turnQueue.Groups)
            {
                _logger.Log($"{group.UnitType}, {group.Initiative}");
            }
            
            _gameStateMachine.EnterState<ResultsState>();
        }

        public void Exit() => 
            _turnQueue.CleanUp();
    }
}