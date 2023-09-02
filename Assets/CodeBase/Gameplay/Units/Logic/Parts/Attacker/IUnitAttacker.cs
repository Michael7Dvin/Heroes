using CodeBase.Gameplay.Units.Logic.Parts.Health;

namespace CodeBase.Gameplay.Units.Logic.Parts.Attacker
{
    public interface IUnitAttacker
    {
        void Attack(IUnitHealth attackedUnitHealth);
    }
}