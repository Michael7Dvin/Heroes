using CodeBase.Gameplay.Services.MapService;
using CodeBase.Gameplay.Tiles;
using CodeBase.Gameplay.Units;
using CodeBase.Gameplay.Units.Parts.Team;
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

        public async UniTask<Unit> Spawn(Vector2Int coordinates, UnitType unitType, int unitsAmount, TeamID teamID)
        {
            Tile tile = _mapService.GetTile(coordinates);
            
            if (tile.Logic.IsWalkable == false)
            {
                _logger.LogError($"Unable to spawn {nameof(Unit)}: {unitType} at: {coordinates}. {nameof(TileLogic)} not walkable");
                return null;
            }
            
            if (tile.Logic.IsOccupied == true)
            {
                _logger.LogError($"Unable to spawn {nameof(Unit)}: {unitType} at: {coordinates}. {nameof(TileLogic)} already occupied");
                return null;
            }

            Vector3 tilePosition = tile.View.transform.position;
            
            Unit unit = await _factory.Create(tilePosition, unitType, unitsAmount, teamID);
            unit.Coordinates.Set(tile.View.Coordinates);
            tile.Logic.Occupy(unit);
            _provider.Add(unit);

            return unit;
        }
    }
}