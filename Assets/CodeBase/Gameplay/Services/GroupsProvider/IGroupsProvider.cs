using System;
using CodeBase.Gameplay.Groups;

namespace CodeBase.Gameplay.Services.GroupsProvider
{
    public interface IGroupsProvider
    {
        event Action<Group> Added;
        event Action<Group> Removed;

        void Add(Group group);
    }
}