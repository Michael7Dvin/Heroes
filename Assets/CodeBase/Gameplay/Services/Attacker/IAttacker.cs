using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Units;

namespace CodeBase.Gameplay.Services.Attacker
{
    public interface IAttacker
    {
        event Action AttackableEnemiesUpdated;
        IReadOnlyList<Unit> AttackableEnemies { get; }

        void Enable();
        void Disable();
        bool CanAttackUnit(Unit unit);
        void AttackUnit(Unit unit);
    }
}