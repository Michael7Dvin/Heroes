using UnityEngine;

namespace CodeBase.Gameplay.Units.Implementations.Skeletons
{
    public class Skeleton : Unit
    {
        public Skeleton(int count, TeamID teamID, int initiative) : base(count, teamID, initiative)
        {
        }

        public override UnitType Type => UnitType.Skeleton;
    }
}