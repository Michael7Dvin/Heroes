using UnityEngine;

namespace CodeBase.Gameplay.Units.Implementations.Zombies
{
    public class Zombie : Unit
    {
        public Zombie(int count, TeamID teamID, int initiative) : base(count, teamID, initiative)
        {
        }

        public override UnitType Type => UnitType.Zombie;
    }
}