using CodeBase.Gameplay.Services.MapService;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Gameplay.Tiles;
using CodeBase.Gameplay.Units;
using CodeBase.Gameplay.Units.Parts.Team;
using CodeBase.Infrastructure.Services.InputService;
using CodeBase.Infrastructure.Services.Logging;
using UnityEngine;

namespace CodeBase.Gameplay.Services.MapInteractor
{
    public class MapInteractor : IMapInteractor
    {
        private readonly IMapService _mapService;
        private readonly IInputService _inputService;
        private readonly ITurnQueue _turnQueue;
        private readonly ICustomLogger _logger;

        public MapInteractor(IMapService mapService,
            IInputService inputService,
            ITurnQueue turnQueue,
            ICustomLogger logger)
        {
            _mapService = mapService;
            _inputService = inputService;
            _turnQueue = turnQueue;
            _logger = logger;
        }

        public void Initialize() => 
            _inputService.NormalInteracted += Interact;

        public void CleanUp() => 
            _inputService.NormalInteracted -= Interact;

        private void Interact()
        {
            if (TryRaycast(out TileView tileView))
            {
                Tile tile = _mapService.GetTile(tileView.Coordinates);
                Unit activeUnit = _turnQueue.ActiveUnit;

                if (tile.Logic.IsOccupied == true)
                    AttackUnit(tile, activeUnit);
                else
                    MoveUnit(tile, activeUnit);
            }
        }

        private bool TryRaycast(out TileView tileView)
        {
            RaycastHit2D hit = Physics2D.Raycast(_inputService.MouseCursorWorldPosition, Vector2.zero);
            
            if (hit.collider != null)
            {
                if (hit.collider.TryGetComponent(out tileView))
                    return true;
            }

            tileView = null;
            return false;
        }

        private static void AttackUnit(Tile tile, Unit unit)
        {
            Unit unitAtTile = tile.Logic.Unit;

            TeamID activeUnitTeamID = unit.Team.Current.Value;
            TeamID unitAtTileTeamID = unitAtTile.Team.Current.Value;

            if (activeUnitTeamID != unitAtTileTeamID)
                unit.Attacker.Attack(unitAtTile);
        }

        private void MoveUnit(Tile tile, Unit unit)
        {
            if (tile.Logic.IsOccupied == true)
            {
                _logger.LogError($"Unable to move {nameof(Unit)}: {unit.Type}. {nameof(TileLogic)} already occupied.");
                return;
            }
            
            _mapService.GetTile(unit.Coordinates.Value).Logic.Release();

            unit.Coordinates.Set(tile.View.Coordinates);
            unit.GameObject.transform.position = tile.View.transform.position;
            tile.Logic.Occupy(unit);
        }
    }
}