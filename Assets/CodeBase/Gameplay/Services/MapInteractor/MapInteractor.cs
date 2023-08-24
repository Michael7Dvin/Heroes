﻿using CodeBase.Gameplay.Services.MapService;
using CodeBase.Gameplay.Services.Mover;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Gameplay.Units;
using CodeBase.Infrastructure.Services.InputService;
using UnityEngine;

namespace CodeBase.Gameplay.Services.MapInteractor
{
    public class MapInteractor : IMapInteractor
    {
        private readonly IMapService _mapService;
        private readonly IInputService _inputService;
        private readonly ITurnQueue _turnQueue;
        private readonly IMover _mover;

        public MapInteractor(IMapService mapService, IInputService inputService, ITurnQueue turnQueue, IMover mover)
        {
            _mapService = mapService;
            _inputService = inputService;
            _turnQueue = turnQueue;
            _mover = mover;
        }

        public void Initialize() => 
            _inputService.NormalInteracted += OnNormalInteraction;

        public void CleanUp() => 
            _inputService.NormalInteracted -= OnNormalInteraction;

        private void OnNormalInteraction()
        {
            Vector3 clickPosition = _inputService.MouseCursorWorldPosition;

            if (_mapService.TryGetCellCoordinates(clickPosition, out Vector3Int coordinates) == true)
            {
                if (_mapService.TryGetUnitAtTile(coordinates, out Unit unit) == true)
                {
                    
                }
                else
                {
                    _mover.Move(coordinates, _turnQueue.ActiveUnit);
                }
            }
        }
    }
}