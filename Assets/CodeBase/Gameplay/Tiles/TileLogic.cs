using System;
using CodeBase.Gameplay.Units;
using CodeBase.Infrastructure.Services.Logging;
using UnityEngine;

namespace CodeBase.Gameplay.Tiles
{
    public class TileLogic
    {
        private readonly ICustomLogger _logger;

        private bool _isWalkable;

        public TileLogic(ICustomLogger logger)
        {
            _logger = logger;
        }

        public void Construct(Vector2Int coordinates, bool isWalkable)
        {
            Coordinates = coordinates;
            _isWalkable = isWalkable;
        }
        
        public Vector2Int Coordinates { get; private set; }
        public Unit Unit { get; private set; }

        public bool IsWalkable => 
            _isWalkable && IsOccupied == false;

        public bool IsOccupied => 
            Unit != null;

        public event Action<TileLogic> Changed;
        
        public void CleanUp()
        {
            if (IsOccupied == true)
                Unit.Logic.Death.Died -= OnUnitDied;
        }

        public void Occupy(Unit unit)
        {
            if (IsOccupied == true)
            {
                _logger.LogError($"Unable to occupy. {nameof(TileLogic)} at: '{Coordinates}' already occupied.");
                return;
            }

            Unit = unit;
            Unit.Logic.Death.Died += OnUnitDied;
            Changed?.Invoke(this);
        }

        public void Release()
        {
            if (IsOccupied == false)
                _logger.LogWarning($"Trying to release not occupied {nameof(TileLogic)} at: '{Coordinates}'");

            Unit.Logic.Death.Died -= OnUnitDied;
            Unit = null;
            Changed?.Invoke(this);
        }
        
        
        private void OnUnitDied()
        {
            Unit.Logic.Death.Died -= OnUnitDied;
            Release();
        }
    }
}