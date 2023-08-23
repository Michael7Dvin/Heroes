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

        public UnitsGroup Create(UnitType unitType, int initiative)
        {
            UnitsGroup unitsGroup = new UnitsGroup(unitType, initiative);
            _groupsProvider.Add(unitsGroup);
            return unitsGroup;
        }
    }
}