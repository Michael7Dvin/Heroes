namespace CodeBase.Gameplay.Units.Implementations.SkeletonNecromancers
{
    public class SkeletonNecromancer : Unit
    {
        public SkeletonNecromancer(int count, TeamID teamID, int initiative, int health, int damage) : base(count, teamID, initiative, health, damage)
        {
        }

        public override UnitType Type => UnitType.SkeletonNecromancer;
    }
}