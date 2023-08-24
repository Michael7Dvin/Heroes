namespace CodeBase.Gameplay.Units.Implementations.Knights
{
    public class Knight : Unit
    {
        public Knight(int count, TeamID teamID, int initiative, int health, int damage) : base(count, teamID, initiative, health, damage)
        {
        }

        public override UnitType Type => UnitType.Knight;
    }
}