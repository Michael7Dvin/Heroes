using System;

namespace CodeBase.Gameplay.Units.Parts.Death
{
    public class UnitDeath : IUnitDeath
    {
        public event Action Died;

        public void Die() => 
            Died?.Invoke();
    }
}