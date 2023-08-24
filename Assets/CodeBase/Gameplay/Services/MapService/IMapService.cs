using CodeBase.Gameplay.Units;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace CodeBase.Gameplay.Services.MapService
{
    public interface IMapService
    {
        void Reset(Tilemap newTilemap);

        bool TryGetCellCoordinates(Vector3 worldPoint, out Vector3Int coordinates);
        
        bool IsTileOccupied(Vector3Int coordinates);
        bool TryGetUnitAtTile(Vector3Int coordinates, out Unit unit);
        Vector3 GetTileCenter(Vector3Int coordinates);
        
        void OccupyTile(Vector3Int coordinates, Unit unit);
        void ReleaseTile(Vector3Int coordinates);
    }
}