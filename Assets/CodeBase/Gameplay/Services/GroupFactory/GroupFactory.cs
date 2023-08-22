using CodeBase.Gameplay.Groups;
using CodeBase.Gameplay.Services.GroupsProvider;

namespace CodeBase.Gameplay.Services.GroupFactory
{
    public class GroupFactory : IGroupFactory
    {
        private readonly IGroupsProvider _groupsProvider;

        public GroupFactory(IGroupsProvider groupsProvider)
        {
            _groupsProvider = groupsProvider;
        }

        public Group Create(UnitType unitType, int initiative)
        {
            Group group = new Group(unitType, initiative);
            _groupsProvider.Add(group);
            return group;
        }
    }
}