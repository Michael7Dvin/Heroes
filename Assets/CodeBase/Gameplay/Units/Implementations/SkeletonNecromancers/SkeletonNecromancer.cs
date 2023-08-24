using UnityEngine;

namespace CodeBase.Gameplay.Units.Implementations.SkeletonNecromancers
{
    public class SkeletonNecromancer : Unit
    {
        public SkeletonNecromancer(int count, TeamID teamID, int initiative) : base(count, teamID, initiative)
        {
        }

        public override UnitType Type => UnitType.SkeletonNecromancer;
    }
}