using System;
using CodeBase.Common.Observable;
using UnityEngine;

namespace CodeBase.Gameplay.Units
{
    public abstract class Unit
    {
        private readonly Observable<int> _count = new();
        private readonly Observable<TeamID> _teamID = new();

        protected Unit(int count, TeamID teamID, int initiative)
        {
            Initiative = initiative;

            _count.Value = count;
            _teamID.Value = teamID;
        }

        public Vector3Int PositionOnMap { get; private set; }

        public void Construct(GameObject gameObject) => 
            GameObject = gameObject;

        public abstract UnitType Type { get; }
        
        public IReadOnlyObservable<int> Count => _count;
        public IReadOnlyObservable<TeamID> TeamID => _teamID;

        public GameObject GameObject { get; private set; }
        public int Initiative { get; }
        
        public event Action Died;

        public void SetPositionOnMap(Vector3Int postion) => 
            PositionOnMap = postion;

        public void KillOne() => 
            _count.Value--;

        public void KillAll()
        {
            _count.Value = 0;
            Died?.Invoke();
        }
    }
}