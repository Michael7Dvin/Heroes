using UnityEngine;

namespace CodeBase.Gameplay.Units.Configs
{
    [CreateAssetMenu(fileName = "All Units Configs", menuName = "Configs/Units/All")]
    public class AllUnitsConfigs : ScriptableObject
    {
        [field: SerializeField] public KnightConfig Knight { get; private set; }
        [field: SerializeField] public ArcherConfig Archer { get; private set; }
        [field: SerializeField] public ZombieConfig Zombie { get; private set; }
        [field: SerializeField] public SkeletonNecromancerConfig SkeletonNecromancer { get; private set; }
        [field: SerializeField] public SkeletonConfig Skeleton { get; private set; }
    }
}