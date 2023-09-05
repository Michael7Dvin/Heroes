using CodeBase.Gameplay.Services.Attacker;
using CodeBase.Gameplay.Services.Mover;
using CodeBase.Gameplay.Services.TileSelector;
using CodeBase.Gameplay.Tiles;
using CodeBase.Gameplay.Units;
using CodeBase.Infrastructure.Services.InputService;

namespace CodeBase.Gameplay.Services.TileInteractor
{
    public class TileInteractor : ITileInteractor
    {
        private readonly IInputService _inputService;
        private readonly ITileSelector _tileSelector;
        private readonly IMover _mover;
        private readonly IAttacker _attacker;

        public TileInteractor(IInputService inputService,
            ITileSelector tileSelector,
            IMover mover,
            IAttacker attacker)
        {
            _inputService = inputService;
            _tileSelector = tileSelector;
            _mover = mover;
            _attacker = attacker;
        }

        private Tile SelectedTile =>
            _tileSelector.SelectedTile.Value;
        
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
                    Unit selectedUnit = SelectedTile.Logic.Unit;
                    
                    if (_attacker.CanAttackUnit(selectedUnit) == true) 
                        _attacker.AttackUnit(selectedUnit);
                }
                else if (_mover.IsMovableAt(SelectedTile) == true && _mover.IsActiveUnitMoving == false) 
                    _mover.MoveActiveUnit(SelectedTile);
            }
        }
    }
}