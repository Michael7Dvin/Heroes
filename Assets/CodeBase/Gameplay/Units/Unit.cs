using System;
using CodeBase.Common.Observable;
using CodeBase.Gameplay.Teams;
using UnityEngine;

namespace CodeBase.Gameplay.Units
{
    public abstract class Unit
    {
        private readonly Observable<int> _amount = new();
        private readonly Observable<TeamID> _teamID = new();

        protected Unit(int count, TeamID teamID, int initiative, int health, int damage)
        {
            _amount.Value = count;
            _teamID.Value = teamID;
            
            Initiative = initiative;
            Health = health;
            Damage = damage;
        }

        public Vector3Int PositionOnMap { get; private set; }

        public void Construct(GameObject gameObject) => 
            GameObject = gameObject;

        public abstract UnitType Type { get; }
        
        public IReadOnlyObservable<int> Amount => _amount;
        public IReadOnlyObservable<TeamID> TeamID => _teamID;

        public GameObject GameObject { get; private set; }
        public int Initiative { get; }
        public int Health { get; }
        public int Damage { get; }
        
        public event Action Died;

        public void SetPositionOnMap(Vector3Int position) => 
            PositionOnMap = position;

        public void Attack(Unit attackedUnit)
        {
            int damage = Damage * Amount.Value;
            attackedUnit.TakeDamage(damage);
        }

        public void TakeDamage(int damage)
        {
            int killedUnits = damage / Health;

            if (killedUnits >= _amount.Value)
            {
                _amount.Value = 0;
                Died?.Invoke();
            }
            else
            {
                _amount.Value -= killedUnits;
            }
        }
    }
}