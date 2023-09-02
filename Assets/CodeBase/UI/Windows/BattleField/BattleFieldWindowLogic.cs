using CodeBase.Gameplay.Services.Mover;
using CodeBase.Gameplay.Services.TurnQueue;

namespace CodeBase.UI.Windows.BattleField
{
    public class BattleFieldWindowLogic
    {
        private readonly ITurnQueue _turnQueue;
        private readonly IMover _mover;

        public BattleFieldWindowLogic(ITurnQueue turnQueue, IMover mover)
        {
            _turnQueue = turnQueue;
            _mover = mover;
        }

        public void EndTurn()
        {
            if (_mover.IsActiveUnitMoving == false) 
                _turnQueue.SetNextTurn();
        }
    }
}