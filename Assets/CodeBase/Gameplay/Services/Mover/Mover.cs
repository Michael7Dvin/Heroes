using CodeBase.Gameplay.Services.MapService;
using CodeBase.Gameplay.Units;
using CodeBase.Infrastructure.Services.Logging;
using UnityEngine;

namespace CodeBase.Gameplay.Services.Mover
{
    public class Mover : IMover
    {
        private readonly IMapService _mapService;
        private readonly ICustomLogger _logger;

        public Mover(IMapService mapService, ICustomLogger logger)
        {
            _mapService = mapService;
            _logger = logger;
        }

        public void Move(Vector3Int position, Unit unit)
        {
            if (_mapService.IsTileOccupied(position) == true)
            { 
                _logger.LogError($"Unable to move {nameof(unit)}: {unit.Type}. Tile already occupied");
                return;
            }

            _mapService.ReleaseTile(unit.Coordinates.Current);
            
            Vector3 moveWorldPosition = _mapService.GetTileCenter(position);
            unit.GameObject.transform.position = moveWorldPosition;
            _mapService.OccupyTile(position, unit);
        }
    }
}