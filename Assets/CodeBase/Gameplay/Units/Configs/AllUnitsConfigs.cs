using UnityEngine;

namespace CodeBase.Gameplay.Units.Configs
{
    [CreateAssetMenu(fileName = "All Units Configs", menuName = "Configs/Units/All")]
    public class AllUnitsConfigs : ScriptableObject
    {
        [field: SerializeField] public KnightConfig Knight { get; private set; }
    }
}