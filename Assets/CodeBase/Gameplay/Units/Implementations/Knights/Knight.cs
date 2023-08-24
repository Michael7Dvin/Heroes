namespace CodeBase.Gameplay.Units.Implementations.Knights
{
    public class Knight : Unit
    {
        public Knight(int count, TeamID teamID, int initiative) : base(count, teamID, initiative)
        {
        }

        public override UnitType Type => UnitType.Knight;
    }
}