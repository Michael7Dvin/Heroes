using System;
using CodeBase.Gameplay.Groups;

namespace CodeBase.Gameplay.Services.GroupsProvider
{
    public interface IGroupsProvider
    {
        event Action<UnitsGroup> Added;
        event Action<UnitsGroup> Removed;

        void Add(UnitsGroup unitsGroup);
    }
}