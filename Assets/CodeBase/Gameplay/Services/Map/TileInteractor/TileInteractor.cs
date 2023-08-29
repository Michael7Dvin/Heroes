using CodeBase.Gameplay.Services.Map.MapService;
using CodeBase.Gameplay.Services.Map.TileSelector;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Gameplay.Tiles;
using CodeBase.Gameplay.Units;
using CodeBase.Gameplay.Units.Parts.Coordinates;
using CodeBase.Gameplay.Units.Parts.Team;
using CodeBase.Infrastructure.Services.InputService;
using CodeBase.Infrastructure.Services.Logging;

namespace CodeBase.Gameplay.Services.Map.TileInteractor
{
    public class TileInteractor : ITileInteractor
    {
        private readonly IInputService _inputService;
        private readonly ITurnQueue _turnQueue;
        private readonly ITileSelector _tileSelector;
        private readonly ICustomLogger _logger;
        private readonly IMapService _mapService;

        public TileInteractor(IInputService inputService,
            ITurnQueue turnQueue,
            ITileSelector tileSelector,
            ICustomLogger logger,
            IMapService mapService)
        {
            _inputService = inputService;
            _turnQueue = turnQueue;
            _tileSelector = tileSelector;
            _logger = logger;
            _mapService = mapService;
        }

        private Tile SelectedTile =>
            _tileSelector.CurrentTile.Value;

        private Unit ActiveUnit => 
            _turnQueue.ActiveUnit;
        
        public void Enable() => 
            _inputService.NormalInteracted += Interact;

        public void Disable() => 
            _inputService.NormalInteracted -= Interact;

        private void Interact()
        {
            if (SelectedTile != null)
            {
                if (SelectedTile.Logic.IsOccupied == true)
                {
                    if (IsEnemy(SelectedTile.Logic.Unit) == true)
                        AttackUnit(SelectedTile.Logic.Unit);
                }
                else
                    MoveActiveUnit(SelectedTile);   
            }
        }

        private bool IsEnemy(Unit unit)
        {
            TeamID activeUnitTeamID = ActiveUnit.Team.Current.Value;
            TeamID unitAtTileTeamID = unit.Team.Current.Value;

            return activeUnitTeamID != unitAtTileTeamID;
        }

        private void AttackUnit(Unit unit) => 
            ActiveUnit.Attacker.Attack(unit);

        private void MoveActiveUnit(Tile tile)
        {
            if (tile.Logic.IsOccupied == true)
            {
                _logger.LogError($"Unable to move {nameof(Unit)}: {ActiveUnit.Type}. {nameof(TileLogic)} already occupied.");
                return;
            }

            UnitCoordinates activeUnitCoordinates = _turnQueue.ActiveUnit.Coordinates;
            
            _mapService.GetTile(activeUnitCoordinates.Value).Logic.Release();

            activeUnitCoordinates.Set(tile.View.Coordinates);
            ActiveUnit.GameObject.transform.position = tile.View.transform.position;
            tile.Logic.Occupy(ActiveUnit);
        }
    }
}