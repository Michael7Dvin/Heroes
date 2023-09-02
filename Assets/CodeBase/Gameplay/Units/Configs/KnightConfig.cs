using UnityEngine;

namespace CodeBase.Gameplay.Units.Configs
{
    [CreateAssetMenu(fileName = "Knight Config", menuName = "Configs/Units/Knight")]
    public class KnightConfig : BaseUnitConfig
    {
        [field: SerializeField] public float AnimationMoveSpeed { get; private set; }
    }
}