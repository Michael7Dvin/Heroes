using CodeBase.Gameplay.Units.Implementations.Archers;
using CodeBase.Gameplay.Units.Implementations.Knights;
using CodeBase.Gameplay.Units.Implementations.SkeletonNecromancers;
using CodeBase.Gameplay.Units.Implementations.Skeletons;
using CodeBase.Gameplay.Units.Implementations.Zombies;
using UnityEngine;

namespace CodeBase.Gameplay.Units
{
    [CreateAssetMenu(fileName = "All Units Configs", menuName = "Configs/Units/All")]
    public class AllUnitsConfigs : ScriptableObject
    {
        [field: SerializeField] public KnightConfig Knight { get; private set; }
        [field: SerializeField] public ArcherConfig Archer { get; private set; }
        [field: SerializeField] public ZombieConfig Zombie { get; private set; }
        [field: SerializeField] public SkeletonNecromancerConfig SkeletonNecromancer { get; private set; }
        [field: SerializeField] public SkeletonConfig Skeletons { get; private set; }
    }
}