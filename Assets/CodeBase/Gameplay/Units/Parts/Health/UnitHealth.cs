using CodeBase.Gameplay.Units.Parts.Death;
using CodeBase.Gameplay.Units.Parts.Stack;
using UnityEngine;

namespace CodeBase.Gameplay.Units.Parts.Health
{
    public class UnitHealth : IUnitHealth
    {
        private readonly int _singleUnitHealth;
        
        private readonly UnitStack _unitStack;
        private readonly IUnitDeath _unitDeath;
        
        private int _remainingDamage;

        public UnitHealth(int singleUnitHealth, UnitStack unitStack, IUnitDeath unitDeath)
        {
            _unitStack = unitStack;
            _unitDeath = unitDeath;
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
            {
                _unitStack.SetAmount(0);
                _unitDeath.Die();
            }
            else
            {
                _unitStack.SetAmount(remainingStacks);
            }
        }
    }
}