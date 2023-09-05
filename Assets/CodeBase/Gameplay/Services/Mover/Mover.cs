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
using CodeBase.Infrastructure.Services.UnitsProvider;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Gameplay.Services.Mover
{
    public class Mover : IMover
    {
        private readonly IUnitsProvider _unitsProvider;
        private readonly IPathFinder _pathFinder;
        private readonly ITurnQueue _turnQueue;
        private readonly IMapService _mapService;
        private readonly ICustomLogger _logger;
        
        private readonly Observable<PathFindingResults> _currentPathFindingResults = new();

        private int _activeUnitMovePoints;

        public Mover(IUnitsProvider unitsProvider,
            IPathFinder pathFinder,
            ITurnQueue turnQueue,
            IMapService mapService,
            ICustomLogger logger)
        {
            _unitsProvider = unitsProvider;
            _pathFinder = pathFinder;
            _turnQueue = turnQueue;
            _mapService = mapService;
            _logger = logger;
        }

        public IReadOnlyObservable<PathFindingResults> CurrentPathFindingResults => _currentPathFindingResults;
        public bool IsActiveUnitMoving { get; private set; }

        public void Enable()
        {
            _turnQueue.NewTurnStarted += OnTurnStarted;
            _mapService.TileChanged += OnMapTileChanged;
        }

        public void Disable()
        {
            _turnQueue.NewTurnStarted -= OnTurnStarted;
            _mapService.TileChanged -= OnMapTileChanged;
        }

        public bool IsMovableAt(Tile tile) =>
            _currentPathFindingResults.Value.IsMovableAt(tile.Logic.Coordinates);

        public async void MoveActiveUnit(Tile tile)
        {
            if (IsMovableAt(tile) == false)
            {
                _logger.LogError($"Unable to move active unit. Can't find path to {nameof(Tile)} at: '{tile.Logic.Coordinates}'");
                return;
            }

            IsActiveUnitMoving = true;
            
            Vector2Int destination = tile.Logic.Coordinates;
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
                pathPositions[i] = _mapService.GetTile(pathCoordinates[i]).transform.position;

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

        private void OnMapTileChanged(Tile tile)
        {
            UnitLogic activeUnitLogic = _turnQueue.ActiveUnit.Logic; 
            
            UpdatePathFindingResults(activeUnitLogic.Coordinates.Observable.Value,
                _activeUnitMovePoints,
                activeUnitLogic.Mover.IsMoveThroughObstacles);
        }
    }
}