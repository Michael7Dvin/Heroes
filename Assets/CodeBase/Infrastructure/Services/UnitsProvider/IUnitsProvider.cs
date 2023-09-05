using System;
using CodeBase.Gameplay.Units;
using CodeBase.Gameplay.Units.Logic;

namespace CodeBase.Infrastructure.Services.UnitsProvider
{
    public interface IUnitsProvider
    {
        event Action UnitsAmountChanged; 
        event Action<Unit> Spawned;
        event Action<Unit> Died;

        void Add(Unit unit);
    }
}