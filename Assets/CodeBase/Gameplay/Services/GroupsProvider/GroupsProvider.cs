using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Groups;

namespace CodeBase.Gameplay.Services.GroupsProvider
{
    public class GroupsProvider : IGroupsProvider
    {
        private readonly List<UnitsGroup> _groups = new();
        
        public event Action<UnitsGroup> Added;
        public event Action<UnitsGroup> Removed;
        
        public void Add(UnitsGroup unitsGroup)
        {
            _groups.Add(unitsGroup);
            unitsGroup.Died += OnGroupDied;
            Added?.Invoke(unitsGroup);

            void OnGroupDied()
            {
                unitsGroup.Died -= OnGroupDied;
                Remove(unitsGroup);
            }
        }

        private void Remove(UnitsGroup unitsGroup)
        {
            _groups.Remove(unitsGroup);
            Removed?.Invoke(unitsGroup);
        }
    }
}