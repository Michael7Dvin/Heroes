using CodeBase.Gameplay.Units.Parts.Attacker;
using CodeBase.Gameplay.Units.Parts.Death;
using CodeBase.Gameplay.Units.Parts.Health;
using CodeBase.Gameplay.Units.Parts.Stack;
using CodeBase.Gameplay.Units.Parts.Team;
using UnityEngine;

namespace CodeBase.Gameplay.Units
{
    public class Unit
    {
        public Unit(UnitType type,
            GameObject gameObject,
            int initiative,
            UnitTeam team,
            UnitCoordinates coordinates,
            UnitStack stack,
            IUnitAttacker attacker,
            IUnitHealth health,
            IUnitDeath death)
        {
            Type = type;
            GameObject = gameObject;
            Initiative = initiative;
            Team = team;
            Coordinates = coordinates;
            Stack = stack;
            Attacker = attacker;
            Health = health;
            Death = death;
        }

        public UnitType Type { get; }
        public GameObject GameObject { get; }
        public int Initiative { get; }

        public UnitTeam Team { get; }
        public UnitCoordinates Coordinates { get; }
        public UnitStack Stack { get; }
        public IUnitAttacker Attacker { get; }
        public IUnitHealth Health { get; }
        public IUnitDeath Death { get; }
    }
}