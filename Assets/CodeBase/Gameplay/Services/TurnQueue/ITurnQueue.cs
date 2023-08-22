using System.Collections.Generic;
using CodeBase.Gameplay.Groups;

namespace CodeBase.Gameplay.Services.TurnQueue
{
    public interface ITurnQueue
    {
        IEnumerable<Group> Groups { get; }
        Group ActiveGroup { get; }
        
        void Initialize();
        void CleanUp();

        void SetNextTurnActiveGroup();
        void SetFirstTurnActiveGroup();
    }
}