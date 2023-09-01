using System.Collections.Generic;
using CodeBase.Gameplay.Tiles;
using UnityEngine;

namespace CodeBase.Gameplay.Services.MapService
{
    public interface IMapService
    {
        void ResetMap(IEnumerable<Tile> map);
        bool TryGetTile(Vector2Int coordinates, out Tile tile);
        Tile GetTile(Vector2Int coordinates);
        IEnumerable<Tile> GetNeighbors(Vector2Int coordinates);
    }
}