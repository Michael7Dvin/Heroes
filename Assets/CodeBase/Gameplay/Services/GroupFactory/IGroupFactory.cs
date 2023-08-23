using CodeBase.Gameplay.Groups;

namespace CodeBase.Gameplay.Services.GroupFactory
{
    public interface IGroupFactory
    {
        UnitsGroup Create(UnitType unitType, int initiative);

    }
}