using UnityEngine;

namespace CodeBase.Gameplay.Level
{
    [CreateAssetMenu(fileName = "Level Config", menuName = "Configs/Level")]
    public class LevelConfig : ScriptableObject
    {
        [field: SerializeField] public UnitPlacementConfig[] Units { get; private set; }
    }
}