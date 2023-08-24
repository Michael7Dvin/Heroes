using System.Collections.Generic;
using CodeBase.Gameplay.Services.RandomService;
using CodeBase.Gameplay.Units;
using CodeBase.Infrastructure.Services.Logging;
using CodeBase.Infrastructure.Services.UnitsProvider;
using CodeBase.UI;

namespace CodeBase.Gameplay.Services.TurnQueue
{
    public class TurnQueue : ITurnQueue
    {
        private readonly IRandomService _randomService;
        private readonly IUnitsProvider _unitsProvider;
        private readonly ICustomLogger _logger;

        private readonly LinkedList<Unit> _groups = new();
        private LinkedListNode<Unit> _activeGroupNode;

        public TurnQueue(IRandomService randomService, IUnitsProvider unitsProvider, ICustomLogger logger)
        {
            _randomService = randomService;
            _unitsProvider = unitsProvider;
            _logger = logger;
        }

        public IEnumerable<Unit> Groups => _groups;
        public Unit ActiveUnit => _activeGroupNode.Value;
        
        public void Initialize()
        {
            _unitsProvider.Added += Add;
            _unitsProvider.Removed += Remove;
        }

        public void CleanUp()
        {
            _unitsProvider.Added -= Add;
            _unitsProvider.Removed -= Remove;
            
            _groups.Clear();
            _activeGroupNode = null;
        }

        public void SetNextTurnActiveGroup()
        {
            if (_activeGroupNode == _groups.First)
                _activeGroupNode = _groups.Last;
            else
                _activeGroupNode = _activeGroupNode.Previous;
            
        }
        
        public void SetFirstTurnActiveGroup() => 
            _activeGroupNode = _groups.Last;
        
        private void Add(Unit unit)
        {
            if (_groups.Count == 0)
            {
                _groups.AddFirst(unit);
                return;
            }
            
            int newGroupInitiative = unit.Initiative;
            
            LinkedListNode<Unit> currentNode = _groups.First;

            while (currentNode != null)
            {
                int currentNodeGroupInitiative = currentNode.Value.Initiative;

                if (newGroupInitiative == currentNodeGroupInitiative)
                {
                    if (_randomService.DoFiftyFifty() == false)
                    {
                        _groups.AddBefore(currentNode, unit);
                        return;
                    }
                }

                if (newGroupInitiative < currentNodeGroupInitiative)
                {
                    _groups.AddBefore(currentNode, unit);
                    return;
                }

                if (currentNode == _groups.Last)
                {
                    _groups.AddLast(unit);
                    return;
                }

                currentNode = currentNode.Next;
            }
        }

        private  void Remove(Unit unit)
        {
            if (unit == _activeGroupNode.Value)
                _logger.LogError($"Unable to remove {nameof(ActiveUnit)}. Feature not implemented");

            _groups.Remove(unit);
        }
    }
}