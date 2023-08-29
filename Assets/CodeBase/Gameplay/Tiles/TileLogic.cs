using CodeBase.Gameplay.Units;
using CodeBase.Infrastructure.Services.Logging;

namespace CodeBase.Gameplay.Tiles
{
    public class TileLogic
    {
        private readonly ICustomLogger _logger;

        public TileLogic(ICustomLogger logger)
        {
            _logger = logger;
        }
        
        public bool IsOccupied { get; private set; }
        public Unit Unit { get; private set; }

        public void Occupy(Unit unit)
        {
            if (IsOccupied == true)
            {
                _logger.LogError($"Unable to occupy. {nameof(TileLogic)} already occupied.");
                return;
            }

            Unit = unit;
            IsOccupied = true;
            unit.Death.Died += OnUnitDied;
        }

        public void Release()
        {
            if (IsOccupied == false)
                _logger.LogWarning($"Trying to release not occupied {nameof(TileLogic)}");

            Unit.Death.Died -= OnUnitDied;
            
            Unit = null;
            IsOccupied = false;
        }

        public bool TryGetUnit(out Unit unit)
        {
            if (IsOccupied == true)
            {
                unit = Unit;
                return true;
            }

            unit = null;
            return false;
        }
        
        private void OnUnitDied()
        {
            Unit.Death.Died -= OnUnitDied;
            Release();
        }
    }
}