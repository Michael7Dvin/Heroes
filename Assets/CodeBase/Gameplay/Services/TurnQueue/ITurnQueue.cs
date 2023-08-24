using System.Collections.Generic;
using CodeBase.Gameplay.Units;
using CodeBase.UI;

namespace CodeBase.Gameplay.Services.TurnQueue
{
    public interface ITurnQueue
    {
        IEnumerable<Unit> Groups { get; }
        Unit ActiveUnit { get; }
        
        void Initialize();
        void CleanUp();

        void SetNextTurnActiveGroup();
        void SetFirstTurnActiveGroup();
    }
}