using CodeBase.Gameplay.Services.MapService;
using CodeBase.Gameplay.Teams;
using CodeBase.Gameplay.Units;
using CodeBase.Infrastructure.Services.Logging;
using CodeBase.Infrastructure.Services.UnitFactory;
using CodeBase.Infrastructure.Services.UnitsProvider;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Gameplay.Services.UnitsSpawner
{
    public class UnitsSpawner : IUnitsSpawner
    {
        private readonly ICustomLogger _logger;
        private readonly IMapService _mapService;
        private readonly IUnitsProvider _provider;
        private readonly IUnitFactory _factory;

        public UnitsSpawner(ICustomLogger logger, IMapService mapService, IUnitsProvider provider, IUnitFactory factory)
        {
            _logger = logger;
            _mapService = mapService;
            _provider = provider;
            _factory = factory;
        }

        public async UniTask<Unit> Spawn(Vector3Int position, UnitType unitType, int unitsAmount, TeamID teamID)
        {
            if (_mapService.IsTileOccupied(position) == true)
            {
                _logger.LogError($"Unable to spawn {nameof(Unit)}: {unitType} at: {position}. Tile already occupied");
                return null;
            }

            Vector3 tileCenter = _mapService.GetTileCenter(position);

            Unit unit = await _factory.Create(tileCenter, unitType, unitsAmount, teamID);
            _mapService.OccupyTile(position, unit);
            _provider.Add(unit);

            return unit;
        }
    }
}