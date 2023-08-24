using CodeBase.Gameplay.Units.Parts.Death;
using UnityEngine;

namespace CodeBase.Gameplay.Units
{
    public class UnitView : MonoBehaviour
    {
        private IUnitDeath _death;
        
        public void Construct(IUnitDeath death)
        {
            _death = death;
            _death.Died += Destroy;
        }

        private void Destroy()
        {
            _death.Died -= Destroy;
            Destroy(gameObject);
        }
    }
}