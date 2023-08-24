using CodeBase.Gameplay.Services.TurnQueue;

namespace CodeBase.UI.Windows.BattleField
{
    public class BattleFieldWindowLogic
    {
        private readonly ITurnQueue _turnQueue;

        public BattleFieldWindowLogic(ITurnQueue turnQueue)
        {
            _turnQueue = turnQueue;
        }

        public void EndTurn() => 
            _turnQueue.SetNextTurn();
    }
}