using CodeBase.Gameplay.Groups;

namespace CodeBase.Gameplay.Services.GroupFactory
{
    public interface IGroupFactory
    {
        Group Create(UnitType unitType, int initiative);

    }
}