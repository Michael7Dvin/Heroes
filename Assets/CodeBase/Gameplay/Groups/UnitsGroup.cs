using System;

namespace CodeBase.Gameplay.Groups
{
    public class UnitsGroup
    {
        public UnitsGroup(UnitType unitType, int initiative)
        {
            UnitType = unitType;
            Initiative = initiative;
        }

        public event Action Died; 

        public UnitType UnitType { get; }
        public int Initiative { get; }

        public void Kill() => 
            Died?.Invoke();
    }
}