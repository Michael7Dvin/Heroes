using CodeBase.Gameplay.Groups;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace CodeBase.Gameplay.Services.MapService
{
    public interface IMapService
    {
        void Reset(Tilemap newTilemap);
        
        bool IsTileOccupied(Vector3Int position);
        bool TryGetUnitsGroupAtTile(Vector3Int position, out UnitsGroup unitsGroup);
        Vector3 GetTileCenter(Vector3Int position);
        
        void OccupyTile(Vector3Int position, UnitsGroup unitsGroup);
        void ReleaseTile(Vector3Int position);
    }
}