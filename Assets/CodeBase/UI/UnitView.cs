using CodeBase.Gameplay.Units;
using UnityEngine;

namespace CodeBase.UI
{
    public class UnitView : MonoBehaviour
    {
        private Unit _unit;

        public void Construct(Unit unit)
        {
            _unit = unit;
            _unit.Died += Destroy;
        }

        private void Destroy()
        {
            _unit.Died -= Destroy;
            Destroy(gameObject);
        }
    }
}