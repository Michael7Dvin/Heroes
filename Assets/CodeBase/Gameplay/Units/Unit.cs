using CodeBase.Gameplay.Units.Parts.Attacker;
using CodeBase.Gameplay.Units.Parts.Death;
using CodeBase.Gameplay.Units.Parts.Health;
using CodeBase.Gameplay.Units.Parts.Position;
using CodeBase.Gameplay.Units.Parts.Stack;
using CodeBase.Gameplay.Units.Parts.Team;
using UnityEngine;

namespace CodeBase.Gameplay.Units
{
    public class Unit
    {
        public Unit(UnitType type,
            int initiative,
            GameObject gameObject,
            UnitCoordinates coordinates,
            UnitTeam team,
            UnitStack stack,
            IUnitAttacker attacker,
            IUnitHealth health,
            IUnitDeath death)
        {
            Type = type;
            Initiative = initiative;
            GameObject = gameObject;
            Coordinates = coordinates;
            Team = team;
            Stack = stack;
            Attacker = attacker;
            Health = health;
            Death = death;
        }

        public UnitType Type { get; }
        public int Initiative { get; }
        public GameObject GameObject { get; }

        public UnitCoordinates Coordinates { get; }
        public UnitTeam Team { get; }
        public UnitStack Stack { get; }
        public IUnitAttacker Attacker { get; }
        public IUnitHealth Health { get; }
        public IUnitDeath Death { get; }
    }
}