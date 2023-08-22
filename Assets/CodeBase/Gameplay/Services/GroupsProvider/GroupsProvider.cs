using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Groups;

namespace CodeBase.Gameplay.Services.GroupsProvider
{
    public class GroupsProvider : IGroupsProvider
    {
        private readonly List<Group> _groups = new();
        
        public event Action<Group> Added;
        public event Action<Group> Removed;
        
        public void Add(Group group)
        {
            _groups.Add(group);
            group.Died += OnGroupDied;
            Added?.Invoke(group);

            void OnGroupDied()
            {
                group.Died -= OnGroupDied;
                Remove(group);
            }
        }

        private void Remove(Group group)
        {
            _groups.Remove(group);
            Removed?.Invoke(group);
        }
    }
}