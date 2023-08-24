using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Infrastructure.GameFSM.States.Base;

namespace CodeBase.Infrastructure.GameFSM.States
{
    public class BattleState : IState
    {
        private readonly ITurnQueue _turnQueue;

        public BattleState(ITurnQueue turnQueue)
        {
            _turnQueue = turnQueue;
        }

        public void Enter()
        {
            _turnQueue.SetFirstTurnActiveGroup();
        }

        public void Exit() => 
            _turnQueue.CleanUp();
    }
}