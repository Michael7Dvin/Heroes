using System.Collections.Generic;
using CodeBase.Gameplay.Units;
using CodeBase.Infrastructure.Services.Logging;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace CodeBase.Gameplay.Services.TileMapService
{
    public class MapService : IMapService
    {
        private readonly ICustomLogger _logger;
        
        private readonly Dictionary<Vector3Int, Unit> _occupiedTiles = new();
        private Tilemap _currentTilemap;

        public MapService(ICustomLogger logger)
        {
            _logger = logger;
        }

        private BoundsInt CurrentMapBounds => 
            _currentTilemap.cellBounds;
        
        public void Reset(Tilemap newTilemap)
        {
            _occupiedTiles.Clear();
            _currentTilemap = newTilemap;
        }

        public bool IsTileOccupied(Vector3Int position) => 
            ValidateIsInBounds(position) && _occupiedTiles.ContainsKey(position);

        public bool TryGetUnitsGroupAtTile(Vector3Int position, out Unit unit)
        {
            if (ValidateIsInBounds(position) == false)
            {
                unit = null;
                return false;
            }
            
            return _occupiedTiles.TryGetValue(position, out unit);
        }

        public Vector3 GetTileCenter(Vector3Int position)
        {
            ValidateIsInBounds(position);
            return _currentTilemap.GetCellCenterWorld(position);
        }

        public void OccupyTile(Vector3Int position, Unit unit)
        {
            ValidateIsInBounds(position);
            
            if (IsTileOccupied(position) == true)
            {
                _logger.LogError($"Unable to occupy Tile. Tile at: '{position}' is already occupied.");
                return;
            }
            
            _occupiedTiles.Add(position, unit);
        }

        public void ReleaseTile(Vector3Int position)
        {
            ValidateIsInBounds(position);
            
            if (IsTileOccupied(position) == false)
            {
                _logger.LogWarning($"Trying to release not occupied Tile at: '{position}'");
                return;
            }

            _occupiedTiles.Remove(position);
        }

        private bool ValidateIsInBounds(Vector3Int position)
        {
            if (CurrentMapBounds.Contains(position) == true) 
                return true;
            
            _logger.LogError($"{nameof(position)}: {position} is out of {nameof(CurrentMapBounds)}");
            return false;
        }
    }
}