using System.Collections.Generic;
using CodeBase.Common.Observable;
using CodeBase.Gameplay.Services.MapService;
using CodeBase.Gameplay.Services.PathFinder;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Gameplay.Tiles;
using CodeBase.Gameplay.Units;
using CodeBase.Gameplay.Units.Logic;
using CodeBase.Gameplay.Units.Logic.Parts.Mover;
using CodeBase.Infrastructure.Services.Logging;
using Cysharp.Threading.Tasks;
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
        public bool IsActiveUnitMoving { get; private set; }

        public void Enable() => 
            _turnQueue.NewTurnStarted += OnTurnStarted;

        public void Disable() => 
            _turnQueue.NewTurnStarted -= OnTurnStarted;

        public bool IsMovableAt(Tile tile) =>
            _currentPathFindingResults.Value.IsMovableAt(tile.View.Coordinates);

        public async void MoveActiveUnit(Tile tile)
        {
            if (IsMovableAt(tile) == false)
            {
                _logger.LogError($"Unable to move active unit. Can't find path to {nameof(Tile)} at: '{tile.View.Coordinates}'");
                return;
            }

            IsActiveUnitMoving = true;
            
            Vector2Int destination = tile.View.Coordinates;
            List<Vector2Int> pathCoordinates = _currentPathFindingResults.Value.GetPathTo(destination);

            Unit activeUnit = _turnQueue.ActiveUnit;
            Tile unitTile = _mapService.GetTile(activeUnit.Logic.Coordinates.Observable.Value);
            
            unitTile.Logic.Release();

            await MoveUnitAlongPath(activeUnit, pathCoordinates);

            activeUnit.Logic.Coordinates.Set(destination);
            tile.Logic.Occupy(activeUnit);

            int pathDistance = pathCoordinates.Count;
            SetActiveUnitMovePoints(_activeUnitMovePoints - pathDistance);
            
            UpdatePathFindingResults(activeUnit.Logic.Coordinates.Observable.Value,
                _activeUnitMovePoints,
                activeUnit.Logic.Mover.IsMoveThroughObstacles);
            
            IsActiveUnitMoving = false;
        }

        private void SetActiveUnitMovePoints(int movePoints) => 
            _activeUnitMovePoints = movePoints;

        private void UpdatePathFindingResults(Vector2Int startCoordinates, int movePoints, bool isFlying) => 
            _currentPathFindingResults.Value = _pathFinder.CalculatePaths(startCoordinates, movePoints, isFlying);

        private async UniTask MoveUnitAlongPath(Unit unit, IReadOnlyList<Vector2Int> pathCoordinates)
        {
            int pointsCount = pathCoordinates.Count;
            Vector3[] pathPositions = new Vector3[pointsCount]; 
            
            for (int i = 0; i < pointsCount; i++)
                pathPositions[i] = _mapService.GetTile(pathCoordinates[i]).View.transform.position;

            await unit.View.PathMoveAnimator.MoveAlongPath(pathPositions);
        }

        private void OnTurnStarted(Unit activeUnit)
        {
            IUnitMover mover = activeUnit.Logic.Mover;
            
            SetActiveUnitMovePoints(mover.MovePoints);
            
            UpdatePathFindingResults(activeUnit.Logic.Coordinates.Observable.Value,
                mover.MovePoints,
                mover.IsMoveThroughObstacles);
        }
    }
}