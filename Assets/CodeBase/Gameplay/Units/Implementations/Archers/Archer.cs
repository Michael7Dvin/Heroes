using UnityEngine;

namespace CodeBase.Gameplay.Units.Implementations.Archers
{
    public class Archer : Unit
    {
        public Archer(int count, TeamID teamID, int initiative) : base(count, teamID, initiative)
        {
        }

        public override UnitType Type => UnitType.Archer;
    }
}