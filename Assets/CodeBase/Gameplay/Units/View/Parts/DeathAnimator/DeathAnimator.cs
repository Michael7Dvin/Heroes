using CodeBase.Gameplay.Units.Logic.Parts.Death;
using UnityEngine;

namespace CodeBase.Gameplay.Units.View.Parts.DeathAnimator
{
    public class DeathAnimator : IDeathAnimator
    {
        private readonly GameObject _gameObject;
        private readonly IUnitDeath _unitDeath;

        public DeathAnimator(GameObject gameObject, IUnitDeath unitDeath)
        {
            _gameObject = gameObject;
            _unitDeath = unitDeath;

            _unitDeath.Died += Die;
        }

        public void Die()
        {
            _unitDeath.Died -= Die;
            Object.Destroy(_gameObject);
        }
    }
}