namespace CodeBase.Gameplay.Units.Implementations.Archers
{
    public class Archer : Unit
    {
        public Archer(int count, TeamID teamID, int initiative, int health, int damage) : base(count, teamID, initiative, health, damage)
        {
        }

        public override UnitType Type => UnitType.Archer;
    }
}