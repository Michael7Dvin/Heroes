using System;

namespace CodeBase.Gameplay.Units.Parts.Death
{
    public interface IUnitDeath
    {
        event Action Died;
        void Die();
    }
}