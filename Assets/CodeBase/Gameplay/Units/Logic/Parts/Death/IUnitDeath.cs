using System;

namespace CodeBase.Gameplay.Units.Logic.Parts.Death
{
    public interface IUnitDeath
    {
        event Action Died;
        void Die();
    }
}