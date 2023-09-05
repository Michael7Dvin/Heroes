using CodeBase.Gameplay.Units.Logic.Parts.Health;
using CodeBase.Gameplay.Units.Logic.Parts.Stack;

namespace CodeBase.Gameplay.Units.Logic.Parts.Attacker
{
    public class UnitAttacker : IUnitAttacker
    {
        private readonly int _damage;
        private readonly UnitStack _unitStack;

        public UnitAttacker(int damage, UnitStack unitStack, int attackDistance, bool isRanged)
        {
            _damage = damage;
            _unitStack = unitStack;
            AttackDistance = attackDistance;
            IsRanged = isRanged;
        }

        public int AttackDistance { get; }
        public bool IsRanged { get; }

        public void Attack(IUnitHealth attackedUnitHealth)
        {
            int damage = _damage * _unitStack.Amount.Value;
            attackedUnitHealth.TakeDamage(damage);
        }
    }
}