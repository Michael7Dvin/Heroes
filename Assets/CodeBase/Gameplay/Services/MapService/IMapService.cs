using System.Collections.Generic;
using CodeBase.Gameplay.Tiles;
using UnityEngine;

namespace CodeBase.Gameplay.Services.MapService
{
    public interface IMapService
    {
        void ResetMap(IEnumerable<Tile> map);
        Tile GetTile(Vector2Int coordinates);
    }
}