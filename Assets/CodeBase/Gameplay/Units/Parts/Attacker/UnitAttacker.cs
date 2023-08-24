using CodeBase.Gameplay.Units.Parts.Stack;

namespace CodeBase.Gameplay.Units.Parts.Attacker
{
    public class UnitAttacker : IUnitAttacker
    {
        private readonly int _damage;
        private readonly UnitStack _unitStack;

        public UnitAttacker(int damage, UnitStack unitStack)
        {
            _damage = damage;
            _unitStack = unitStack;
        }

        public void Attack(Unit attackedUnit)
        {
            int damage = _damage * _unitStack.Amount.Value;
            attackedUnit.Health.TakeDamage(damage);
        }
    }
}