using CodeBase.Gameplay.Units.Logic.Parts.Attacker;
using CodeBase.Gameplay.Units.Logic.Parts.Coordinates;
using CodeBase.Gameplay.Units.Logic.Parts.Death;
using CodeBase.Gameplay.Units.Logic.Parts.Health;
using CodeBase.Gameplay.Units.Logic.Parts.Mover;
using CodeBase.Gameplay.Units.Logic.Parts.Stack;
using CodeBase.Gameplay.Units.Logic.Parts.Team;

namespace CodeBase.Gameplay.Units.Logic
{
    public class UnitLogic
    {
        public UnitLogic(UnitType type,
            int initiative,
            UnitTeam team,
            UnitCoordinates coordinates,
            UnitStack stack,
            IUnitMover mover,
            IUnitAttacker attacker,
            IUnitHealth health,
            IUnitDeath death)
        {
            Type = type;
            Initiative = initiative;
            Team = team;
            Coordinates = coordinates;
            Stack = stack;
            Mover = mover;
            Attacker = attacker;
            Health = health;
            Death = death;
        }

        public UnitType Type { get; }
        public int Initiative { get; }

        public UnitTeam Team { get; }
        public UnitCoordinates Coordinates { get; }
        public UnitStack Stack { get; }
        public IUnitMover Mover { get; }
        public IUnitAttacker Attacker { get; }
        public IUnitHealth Health { get; }
        public IUnitDeath Death { get; }
    }
}