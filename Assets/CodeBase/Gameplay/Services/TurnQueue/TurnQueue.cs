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

        private readonly LinkedList<Group> _groups = new();
        private LinkedListNode<Group> _activeGroupNode;

        public TurnQueue(IRandomService randomService, IGroupsProvider groupsProvider, ICustomLogger logger)
        {
            _randomService = randomService;
            _groupsProvider = groupsProvider;
            _logger = logger;
        }

        public IEnumerable<Group> Groups => _groups;
        public Group ActiveGroup => _activeGroupNode.Value;
        
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
        
        private void Add(Group group)
        {
            if (_groups.Count == 0)
            {
                _groups.AddFirst(group);
                return;
            }
            
            int newGroupInitiative = group.Initiative;
            
            LinkedListNode<Group> currentNode = _groups.First;

            while (currentNode != null)
            {
                int currentNodeGroupInitiative = currentNode.Value.Initiative;

                if (newGroupInitiative == currentNodeGroupInitiative)
                {
                    if (_randomService.DoFiftyFifty() == false)
                    {
                        _groups.AddBefore(currentNode, group);
                        return;
                    }
                }

                if (newGroupInitiative < currentNodeGroupInitiative)
                {
                    _groups.AddBefore(currentNode, group);
                    return;
                }

                if (currentNode == _groups.Last)
                {
                    _groups.AddLast(group);
                    return;
                }

                currentNode = currentNode.Next;
            }
        }

        private  void Remove(Group group)
        {
            if (group == _activeGroupNode.Value)
                _logger.LogError($"Unable to remove {nameof(ActiveGroup)}. Feature not implemented");

            _groups.Remove(group);
        }
    }
}