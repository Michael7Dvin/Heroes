using System;
using CodeBase.Gameplay.Units.Logic.Parts.Stack;

namespace CodeBase.Gameplay.Units.Logic.Parts.Death
{
    public class UnitDeath : IUnitDeath
    {
        private readonly UnitStack _unitStack;

        public UnitDeath(UnitStack unitStack)
        {
            _unitStack = unitStack;
            
            _unitStack.Amount.Changed += OnStackAmountChanged;
        }

        private void OnStackAmountChanged(int amount)
        {
            if (amount <= 0)
            {
                _unitStack.Amount.Changed -= OnStackAmountChanged;
                Die();
            }
        }

        public event Action Died;

        public void Die() => 
            Died?.Invoke();
    }
}