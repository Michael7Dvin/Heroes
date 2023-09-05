using CodeBase.Gameplay.Units.Logic.Parts.Health;

namespace CodeBase.Gameplay.Units.Logic.Parts.Attacker
{
    public interface IUnitAttacker
    {
        int AttackDistance { get; }
        bool IsRanged { get; }
        void Attack(IUnitHealth attackedUnitHealth);
    }
}