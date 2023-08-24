using CodeBase.Gameplay.Units;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace CodeBase.Gameplay.Services.TileMapService
{
    public interface IMapService
    {
        void Reset(Tilemap newTilemap);
        
        bool IsTileOccupied(Vector3Int position);
        bool TryGetUnitsGroupAtTile(Vector3Int position, out Unit unit);
        Vector3 GetTileCenter(Vector3Int position);
        
        void OccupyTile(Vector3Int position, Unit unit);
        void ReleaseTile(Vector3Int position);
    }
}