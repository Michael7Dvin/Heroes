using System.Collections.Generic;
using CodeBase.Gameplay.Units;
using CodeBase.UI;

namespace CodeBase.Gameplay.Services.TurnQueue
{
    public interface ITurnQueue
    {
        IEnumerable<Units.Unit> Units { get; }
        Units.Unit ActiveUnit { get; }
        
        void Initialize();
        void CleanUp();

        void SetNextTurn();
        void SetFirstTurn();
    }
}