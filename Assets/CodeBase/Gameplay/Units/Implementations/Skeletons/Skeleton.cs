namespace CodeBase.Gameplay.Units.Implementations.Skeletons
{
    public class Skeleton : Unit
    {
        public Skeleton(int count, TeamID teamID, int initiative, int health, int damage) : base(count, teamID, initiative, health, damage)
        {
        }

        public override UnitType Type => UnitType.Skeleton;
    }
}