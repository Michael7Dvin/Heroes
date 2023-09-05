using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Services.RandomService;
using CodeBase.Gameplay.Units;
using CodeBase.Infrastructure.Services.Logging;
using CodeBase.Infrastructure.Services.UnitsProvider;

namespace CodeBase.Gameplay.Services.TurnQueue
{
    public class TurnQueue : ITurnQueue
    {
        private readonly IRandomService _randomService;
        private readonly IUnitsProvider _unitsProvider;
        private readonly ICustomLogger _logger;

        private readonly LinkedList<Unit> _units = new();
        private LinkedListNode<Unit> _activeUnitNode;

        public TurnQueue(IRandomService randomService, IUnitsProvider unitsProvider, ICustomLogger logger)
        {
            _randomService = randomService;
            _unitsProvider = unitsProvider;
            _logger = logger;
        }

        public IEnumerable<Unit> Units => _units;
        public Unit ActiveUnit => _activeUnitNode.Value;
        public event Action<Unit> NewTurnStarted;

        public void Initialize()
        {
            _unitsProvider.Spawned += Add;
            _unitsProvider.Died += Remove;
        }
        
        public void CleanUp()
        {
            _unitsProvider.Spawned -= Add;
            _unitsProvider.Died -= Remove;
            
            _units.Clear();
            _activeUnitNode = null;
        }

        public void SetNextTurn()
        {
            if (_activeUnitNode == _units.First)
                _activeUnitNode = _units.Last;
            else
                _activeUnitNode = _activeUnitNode.Previous;
            
            NewTurnStarted?.Invoke(ActiveUnit);
        }
        
        public void SetFirstTurn()
        {
            _activeUnitNode = _units.Last;
            NewTurnStarted?.Invoke(ActiveUnit);
        }

        private void Add(Unit unit)
        {
            if (_units.Count == 0)
            {
                _units.AddFirst(unit);
                return;
            }
            
            int newGroupInitiative = unit.Logic.Initiative;
            
            LinkedListNode<Unit> currentNode = _units.First;

            while (currentNode != null)
            {
                int currentNodeGroupInitiative = currentNode.Value.Logic.Initiative;

                if (newGroupInitiative == currentNodeGroupInitiative)
                {
                    if (_randomService.DoFiftyFifty() == false)
                    {
                        _units.AddBefore(currentNode, unit);
                        return;
                    }
                }

                if (newGroupInitiative < currentNodeGroupInitiative)
                {
                    _units.AddBefore(currentNode, unit);
                    return;
                }

                if (currentNode == _units.Last)
                {
                    _units.AddLast(unit);
                    return;
                }

                currentNode = currentNode.Next;
            }
        }

        private void Remove(Unit unit)
        {
            if (unit == _activeUnitNode.Value)
                _logger.LogError($"Unable to remove {nameof(ActiveUnit)}. Feature not implemented");

            _units.Remove(unit);
        }
    }
}