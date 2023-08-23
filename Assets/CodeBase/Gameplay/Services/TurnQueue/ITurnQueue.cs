using System.Collections.Generic;
using CodeBase.Gameplay.Groups;

namespace CodeBase.Gameplay.Services.TurnQueue
{
    public interface ITurnQueue
    {
        IEnumerable<UnitsGroup> Groups { get; }
        UnitsGroup ActiveUnitsGroup { get; }
        
        void Initialize();
        void CleanUp();

        void SetNextTurnActiveGroup();
        void SetFirstTurnActiveGroup();
    }
}