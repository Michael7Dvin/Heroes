using CodeBase.Gameplay.Units;
using CodeBase.Gameplay.Units.Logic;
using CodeBase.Infrastructure.Services.Logging;

namespace CodeBase.Gameplay.Tiles
{
    public class TileLogic
    {
        private readonly ICustomLogger _logger;
        
        private bool _isWalkable;
        private Unit _unit;

        public TileLogic(ICustomLogger logger)
        {
            _logger = logger;
        }

        public void Construct(bool isWalkable)
        {
            _isWalkable = isWalkable;
        }

        public bool IsWalkable => 
            _isWalkable && IsOccupied == false;

        public bool IsOccupied { get; private set; }

        public void Occupy(Unit unit)
        {
            if (IsOccupied == true)
            {
                _logger.LogError($"Unable to occupy. {nameof(TileLogic)} already occupied.");
                return;
            }

            _unit = unit;
            IsOccupied = true;
            unit.Logic.Death.Died += OnUnitDied;
        }

        public void Release()
        {
            if (IsOccupied == false)
                _logger.LogWarning($"Trying to release not occupied {nameof(TileLogic)}");

            _unit.Logic.Death.Died -= OnUnitDied;
            
            _unit = null;
            IsOccupied = false;
        }

        public bool TryGetUnit(out Unit unit)
        {
            if (IsOccupied == true)
            {
                unit = _unit;
                return true;
            }

            unit = null;
            return false;
        }
        
        private void OnUnitDied()
        {
            _unit.Logic.Death.Died -= OnUnitDied;
            Release();
        }
    }
}