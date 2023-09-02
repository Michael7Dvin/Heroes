﻿using System;
using CodeBase.Gameplay.Units;
using CodeBase.Gameplay.Units.Logic;

namespace CodeBase.Infrastructure.Services.UnitsProvider
{
    public interface IUnitsProvider
    {
        event Action<Unit> Added;
        event Action<Unit> Removed;

        void Add(Unit unit);
    }
}