using CodeBase.Gameplay.Services.Mover;
using CodeBase.Gameplay.Services.TileSelector;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Gameplay.Tiles;
using CodeBase.Gameplay.Units;
using CodeBase.Gameplay.Units.Parts.Team;
using CodeBase.Infrastructure.Services.InputService;

namespace CodeBase.Gameplay.Services.TileInteractor
{
    public class TileInteractor : ITileInteractor
    {
        private readonly IInputService _inputService;
        private readonly ITurnQueue _turnQueue;
        private readonly ITileSelector _tileSelector;
        private readonly IMover _mover;

        public TileInteractor(IInputService inputService,
            ITurnQueue turnQueue,
            ITileSelector tileSelector,
            IMover mover)
        {
            _inputService = inputService;
            _turnQueue = turnQueue;
            _tileSelector = tileSelector;
            _mover = mover;
        }

        private Tile SelectedTile =>
            _tileSelector.SelectedTile.Value;

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
                if (SelectedTile.Logic.TryGetUnit(out Unit unit))
                {
                    if (IsEnemy(unit) == true)
                        AttackUnit(unit);
                }
                else if (_mover.IsMovableAt(SelectedTile)) 
                    _mover.MoveActiveUnit(SelectedTile);
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

        
    }
}