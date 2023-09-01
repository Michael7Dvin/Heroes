using System.Collections.Generic;
using CodeBase.Common.Observable;
using CodeBase.Gameplay.Services.MapService;
using CodeBase.Gameplay.Services.PathFinder;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Gameplay.Tiles;
using CodeBase.Gameplay.Units;
using CodeBase.Infrastructure.Services.Logging;
using UnityEngine;

namespace CodeBase.Gameplay.Services.Mover
{
    public class Mover : IMover
    {
        private readonly IPathFinder _pathFinder;
        private readonly ITurnQueue _turnQueue;
        private readonly IMapService _mapService;
        private readonly ICustomLogger _logger;

        private readonly Observable<PathFindingResults> _currentPathFindingResults = new();
        private int _activeUnitMovePoints;

        public Mover(IPathFinder pathFinder, ITurnQueue turnQueue, IMapService mapService, ICustomLogger logger)
        {
            _pathFinder = pathFinder;
            _turnQueue = turnQueue;
            _mapService = mapService;
            _logger = logger;
        }

        public IReadOnlyObservable<PathFindingResults> CurrentPathFindingResults => _currentPathFindingResults;

        public void Enable() => 
            _turnQueue.NewTurnStarted += OnTurnStarted;

        public void Disable() => 
            _turnQueue.NewTurnStarted -= OnTurnStarted;

        public bool IsMovableAt(Tile tile) =>
            _currentPathFindingResults.Value.IsMovableAt(tile.View.Coordinates);

        public void MoveActiveUnit(Tile tile)
        {
            if (IsMovableAt(tile) == false)
            {
                _logger.LogError($"Unable to move active unit. Can't find path to {nameof(Tile)} at: '{tile.View.Coordinates}'");
                return;
            }

            List<Vector2Int> pathPoints = _currentPathFindingResults.Value.GetPathTo(tile.View.Coordinates);

            Unit activeUnit = _turnQueue.ActiveUnit;
            Tile unitTile = _mapService.GetTile(activeUnit.Coordinates.Observable.Value);
            
            unitTile.Logic.Release();

            ChangeUnitWorldPosition(activeUnit, tile.View.transform.position);
            activeUnit.Coordinates.Set(tile.View.Coordinates);
            
            tile.Logic.Occupy(activeUnit);

            int pathDistance = pathPoints.Count;
            SetActiveUnitMovePoints(_activeUnitMovePoints - pathDistance);
            UpdatePathFindingResults(activeUnit.Coordinates.Observable.Value, _activeUnitMovePoints);
        }

        private void SetActiveUnitMovePoints(int movePoints) => 
            _activeUnitMovePoints = movePoints;

        private void UpdatePathFindingResults(Vector2Int startCoordinates, int movePoints) => 
            _currentPathFindingResults.Value = _pathFinder.FindPathsByBFS(startCoordinates, movePoints);

        private void ChangeUnitWorldPosition(Unit unit, Vector3 position) =>
            unit.GameObject.transform.position = position;

        private void OnTurnStarted(Unit activeUnit)
        {
            SetActiveUnitMovePoints(activeUnit.MovePoints);
            UpdatePathFindingResults(activeUnit.Coordinates.Observable.Value, activeUnit.MovePoints);
        }
    }
}