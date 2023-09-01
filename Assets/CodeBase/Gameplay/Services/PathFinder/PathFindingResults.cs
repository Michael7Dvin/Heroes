using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Gameplay.Services.PathFinder
{
    public readonly struct PathFindingResults
    {
        private readonly Dictionary<Vector2Int, Vector2Int?> _visitedTiles;

        public PathFindingResults(Dictionary<Vector2Int, Vector2Int?> visitedTiles)
        {
            _visitedTiles = visitedTiles;
        }

        public IEnumerable<Vector2Int> WalkableCoordinates =>
            _visitedTiles.Keys.Skip(1);

        public bool IsMovableAt(Vector2Int coordinates) =>
            WalkableCoordinates.Contains(coordinates);
        
        public List<Vector2Int> GetPathTo(Vector2Int coordinates)
        {
            if (IsMovableAt(coordinates) == false)
                return null;

            Vector2Int current = coordinates;
            List<Vector2Int> path = new List<Vector2Int> { current };

            while (_visitedTiles[current] != null)
            {
                path.Add(_visitedTiles[current].Value);
                current = _visitedTiles[current].Value;
            }
            
            path.Reverse();
            path.RemoveAt(0);
            return path;
        }
    }
}