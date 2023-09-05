using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Units;

namespace CodeBase.Infrastructure.Services.UnitsProvider
{
    public class UnitsProvider : IUnitsProvider
    {
        private readonly List<Unit> _units = new();

        public event Action UnitsAmountChanged;
        public event Action<Unit> Spawned;
        public event Action<Unit> Died;
        
        public void Add(Unit unit)
        {
            _units.Add(unit);
            unit.Logic.Death.Died += OnUnitDied;
            Spawned?.Invoke(unit);
            UnitsAmountChanged?.Invoke();
            
            void OnUnitDied()
            {
                unit.Logic.Death.Died -= OnUnitDied;
                Remove(unit);
            }
        }

        private void Remove(Unit unit)
        {
            _units.Remove(unit);
            Died?.Invoke(unit);
            UnitsAmountChanged?.Invoke();
        }
    }
}