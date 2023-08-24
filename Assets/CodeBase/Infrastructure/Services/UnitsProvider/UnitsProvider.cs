using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Units;

namespace CodeBase.Infrastructure.Services.UnitsProvider
{
    public class UnitsProvider : IUnitsProvider
    {
        private readonly List<Unit> _units = new();
        
        public event Action<Unit> Added;
        public event Action<Unit> Removed;
        
        public void Add(Unit unit)
        {
            _units.Add(unit);
            unit.Died += OnGroupDied;
            Added?.Invoke(unit);

            void OnGroupDied()
            {
                unit.Died -= OnGroupDied;
                Remove(unit);
            }
        }

        private void Remove(Unit unit)
        {
            _units.Remove(unit);
            Removed?.Invoke(unit);
        }
    }
}