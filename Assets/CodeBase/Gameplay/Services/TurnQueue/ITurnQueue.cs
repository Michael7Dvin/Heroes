using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Units;
using CodeBase.Gameplay.Units.Logic;

namespace CodeBase.Gameplay.Services.TurnQueue
{
    public interface ITurnQueue
    {
        IEnumerable<Unit> Units { get; }
        Unit ActiveUnit { get; }

        event Action<Unit> NewTurnStarted; 

        void Initialize();
        void CleanUp();

        void SetNextTurn();
        void SetFirstTurn();
    }
}