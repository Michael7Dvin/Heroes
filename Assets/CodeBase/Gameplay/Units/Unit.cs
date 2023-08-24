using System;
using CodeBase.Common.Observable;

namespace CodeBase.Gameplay.Units
{
    public class Unit
    {
        private readonly Observable<int> _count = new();
        private readonly Observable<TeamID> _teamID = new();

        public Unit(UnitType type, int count, TeamID teamID, int initiative)
        {
            Type = type;
            Initiative = initiative;

            _count.Value = count;
            _teamID.Value = teamID;
        }

        public IReadOnlyObservable<int> Count => _count;
        public IReadOnlyObservable<TeamID> TeamID => _teamID;

        public UnitType Type { get; }
        public int Initiative { get; }
        
        public event Action Died;

        public void KillOne() => 
            _count.Value--;

        public void KillAll()
        {
            _count.Value = 0;
            Died?.Invoke();
        }
    }
}