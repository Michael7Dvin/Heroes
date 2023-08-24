using CodeBase.Gameplay.Teams;

namespace CodeBase.Gameplay.Units.Implementations.Zombies
{
    public class Zombie : Unit
    {
        public Zombie(int count, TeamID teamID, int initiative, int health, int damage) : base(count, teamID, initiative, health, damage)
        {
        }

        public override UnitType Type => UnitType.Zombie;
    }
}