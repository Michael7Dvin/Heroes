using CodeBase.Common.Observable;
using UnityEngine;

namespace CodeBase.Gameplay.Units.Logic.Parts.Coordinates
{
    public class UnitCoordinates
    {
        private readonly Observable<Vector2Int> _observable = new();
        
        public IReadOnlyObservable<Vector2Int> Observable => _observable;

        public void Set(Vector2Int coordinates) =>
            _observable.Value = coordinates;
    }
}