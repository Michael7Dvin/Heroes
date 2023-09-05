using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Services.MapService;
using CodeBase.Gameplay.Services.PathFinder;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Gameplay.Tiles;
using CodeBase.Gameplay.Units;
using CodeBase.Gameplay.Units.Logic.Parts.Team;
using UnityEngine;

namespace CodeBase.Gameplay.Services.Attacker
{
    public class Attacker : IAttacker 
    {
        private readonly ITurnQueue _turnQueue;
        private readonly IPathFinder _pathFinder;
        private readonly IMapService _mapService;
        
        private readonly List<Unit> _attackableEnemies = new();
        private Unit _activeUnit;

        public Attacker(ITurnQueue turnQueue, IPathFinder pathFinder, IMapService mapService)
        {
            _turnQueue = turnQueue;
            _pathFinder = pathFinder;
            _mapService = mapService;
        }

        public IReadOnlyList<Unit> AttackableEnemies => _attackableEnemies;

        public event Action AttackableEnemiesUpdated;
        
        public void Enable()
        {
            _turnQueue.NewTurnStarted += OnNewTurnStarted;
            _mapService.TileChanged += OnMapTileChanged;
        }

        public void Disable()
        {
            _turnQueue.NewTurnStarted -= OnNewTurnStarted;
            _mapService.TileChanged -= OnMapTileChanged;

        }

        public bool CanAttackUnit(Unit unit) => 
            _attackableEnemies.Contains(unit);

        public void AttackUnit(Unit unit)
        {
            if (CanAttackUnit(unit) == true) 
                _turnQueue.ActiveUnit.Logic.Attacker.Attack(unit.Logic.Health);
        }

        private void OnNewTurnStarted(Unit activeUnit)
        {
            if (_activeUnit != null) 
                _activeUnit.Logic.Coordinates.Observable.Changed -= OnActiveUnitCoordinatesChanged;

            _activeUnit = activeUnit;
            _activeUnit.Logic.Coordinates.Observable.Changed += OnActiveUnitCoordinatesChanged;

            UpdateAttackableEnemies();
            
            void OnActiveUnitCoordinatesChanged(Vector2Int coordinates) => 
                UpdateAttackableEnemies();
        }

        private void UpdateAttackableEnemies()
        {
            _attackableEnemies.Clear();

            Unit activeUnit = _turnQueue.ActiveUnit;
            
            Vector2Int coordinates = activeUnit.Logic.Coordinates.Observable.Value;
            int maxAttackDistance = activeUnit.Logic.Attacker.AttackDistance;
            bool isRanged = activeUnit.Logic.Attacker.IsRanged;

            IReadOnlyDictionary<Vector2Int, Tile> obstacles = 
                _pathFinder.CalculatePaths(coordinates, maxAttackDistance, isRanged).Obstacles;

            foreach (KeyValuePair<Vector2Int, Tile> keyValuePair in obstacles)
            {
                TileLogic tileLogic = keyValuePair.Value.Logic;
                
                if (tileLogic.IsOccupied == true)
                {
                    Unit tileUnit = tileLogic.Unit;
                    TeamID tileUnitTeam = tileUnit.Logic.Team.Current.Value;
                    
                    if (IsEnemy(tileUnitTeam) == true) 
                        _attackableEnemies.Add(tileUnit);
                }
            }
            
            AttackableEnemiesUpdated?.Invoke();
        }

        private void OnMapTileChanged(Tile tile) => 
            UpdateAttackableEnemies();

        private bool IsEnemy(TeamID unitTeamID)
        {
            TeamID activeUnitTeamID = _turnQueue.ActiveUnit.Logic.Team.Current.Value;
            return activeUnitTeamID != unitTeamID;
        }
    }
}