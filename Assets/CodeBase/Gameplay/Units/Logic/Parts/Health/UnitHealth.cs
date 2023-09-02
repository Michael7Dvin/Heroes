using CodeBase.Gameplay.Units.Logic.Parts.Stack;

namespace CodeBase.Gameplay.Units.Logic.Parts.Health
{
    public class UnitHealth : IUnitHealth
    {
        private readonly int _singleUnitHealth;
        
        private readonly UnitStack _unitStack;
        
        private int _remainingDamage;

        public UnitHealth(int singleUnitHealth, UnitStack unitStack)
        {
            _unitStack = unitStack;
            _singleUnitHealth = singleUnitHealth;
            _unitStack = unitStack;
        }

        public void TakeDamage(int damage)
        {
            int allHealth = _unitStack.Amount.Value * _singleUnitHealth;
            allHealth -= damage + _remainingDamage;
            
            int remainingStacks = (allHealth + _singleUnitHealth - 1) / _singleUnitHealth;

            if (allHealth % _singleUnitHealth == 0)
                _remainingDamage = 0;
            else
                _remainingDamage = _singleUnitHealth - allHealth % _singleUnitHealth;

            if (remainingStacks <= 0)
                _unitStack.SetAmount(0);
            else
                _unitStack.SetAmount(remainingStacks);
        }
    }
}