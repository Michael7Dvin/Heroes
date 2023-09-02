using CodeBase.Gameplay.Units.Logic.Parts.Health;
using CodeBase.Gameplay.Units.Logic.Parts.Stack;

namespace CodeBase.Gameplay.Units.Logic.Parts.Attacker
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

        public void Attack(IUnitHealth attackedUnitHealth)
        {
            int damage = _damage * _unitStack.Amount.Value;
            attackedUnitHealth.TakeDamage(damage);
        }
    }
}