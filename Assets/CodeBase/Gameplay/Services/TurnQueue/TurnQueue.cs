using System.Collections.Generic;
using CodeBase.Gameplay.Groups;
using CodeBase.Gameplay.Services.GroupsProvider;
using CodeBase.Gameplay.Services.RandomService;
using CodeBase.Infrastructure.Services.Logging;

namespace CodeBase.Gameplay.Services.TurnQueue
{
    public class TurnQueue : ITurnQueue
    {
        private readonly IRandomService _randomService;
        private readonly IGroupsProvider _groupsProvider;
        private readonly ICustomLogger _logger;

        private readonly LinkedList<UnitsGroup> _groups = new();
        private LinkedListNode<UnitsGroup> _activeGroupNode;

        public TurnQueue(IRandomService randomService, IGroupsProvider groupsProvider, ICustomLogger logger)
        {
            _randomService = randomService;
            _groupsProvider = groupsProvider;
            _logger = logger;
        }

        public IEnumerable<UnitsGroup> Groups => _groups;
        public UnitsGroup ActiveUnitsGroup => _activeGroupNode.Value;
        
        public void Initialize()
        {
            _groupsProvider.Added += Add;
            _groupsProvider.Removed += Remove;
        }

        public void CleanUp()
        {
            _groupsProvider.Added -= Add;
            _groupsProvider.Removed -= Remove;
            
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
        
        private void Add(UnitsGroup unitsGroup)
        {
            if (_groups.Count == 0)
            {
                _groups.AddFirst(unitsGroup);
                return;
            }
            
            int newGroupInitiative = unitsGroup.Initiative;
            
            LinkedListNode<UnitsGroup> currentNode = _groups.First;

            while (currentNode != null)
            {
                int currentNodeGroupInitiative = currentNode.Value.Initiative;

                if (newGroupInitiative == currentNodeGroupInitiative)
                {
                    if (_randomService.DoFiftyFifty() == false)
                    {
                        _groups.AddBefore(currentNode, unitsGroup);
                        return;
                    }
                }

                if (newGroupInitiative < currentNodeGroupInitiative)
                {
                    _groups.AddBefore(currentNode, unitsGroup);
                    return;
                }

                if (currentNode == _groups.Last)
                {
                    _groups.AddLast(unitsGroup);
                    return;
                }

                currentNode = currentNode.Next;
            }
        }

        private  void Remove(UnitsGroup unitsGroup)
        {
            if (unitsGroup == _activeGroupNode.Value)
                _logger.LogError($"Unable to remove {nameof(ActiveUnitsGroup)}. Feature not implemented");

            _groups.Remove(unitsGroup);
        }
    }
}