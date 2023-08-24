using System.Collections.Generic;
using CodeBase.Gameplay.Units;
using CodeBase.Infrastructure.Services.Logging;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace CodeBase.Gameplay.Services.MapService
{
    public class MapService : IMapService
    {
        private const int CellsZCoordinates = 0;
        
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

        public bool TryGetCellCoordinates(Vector3 worldPoint, out Vector3Int coordinates)
        {
            coordinates = _currentTilemap.WorldToCell(worldPoint);
            coordinates.z = CellsZCoordinates;

            if (CurrentMapBounds.Contains(coordinates) == false)
            {
                coordinates = Vector3Int.zero;
                return false;
            }
            
            return true;
        }

        public bool IsTileOccupied(Vector3Int coordinates) => 
            ValidateIsInBounds(coordinates) && _occupiedTiles.ContainsKey(coordinates);

        public bool TryGetUnitAtTile(Vector3Int coordinates, out Unit unit)
        {
            if (ValidateIsInBounds(coordinates) == false)
            {
                unit = null;
                return false;
            }
            
            return _occupiedTiles.TryGetValue(coordinates, out unit);
        }

        public Vector3 GetTileCenter(Vector3Int coordinates)
        {
            ValidateIsInBounds(coordinates);
            return _currentTilemap.GetCellCenterWorld(coordinates);
        }

        public void OccupyTile(Vector3Int coordinates, Unit unit)
        {
            ValidateIsInBounds(coordinates);
            
            if (IsTileOccupied(coordinates) == true)
            {
                _logger.LogError($"Unable to occupy Tile. Tile at: '{coordinates}' is already occupied.");
                return;
            }
            
            unit.SetPositionOnMap(coordinates);
            _occupiedTiles.Add(coordinates, unit);
        }

        public void ReleaseTile(Vector3Int coordinates)
        {
            ValidateIsInBounds(coordinates);
            
            if (IsTileOccupied(coordinates) == false)
            {
                _logger.LogWarning($"Trying to release not occupied Tile at: '{coordinates}'");
                return;
            }

            _occupiedTiles.Remove(coordinates);
        }

        private bool ValidateIsInBounds(Vector3Int coordinates)
        {
            if (CurrentMapBounds.Contains(coordinates) == true) 
                return true;
            
            _logger.LogError($"{nameof(coordinates)}: {coordinates} is out of {nameof(CurrentMapBounds)}");
            return false;
        }
    }
}