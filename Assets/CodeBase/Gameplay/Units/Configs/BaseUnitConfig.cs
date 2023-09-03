using UnityEngine;

namespace CodeBase.Gameplay.Units.Configs
{
    public class BaseUnitConfig : ScriptableObject
    {
        [field: SerializeField] public int Initiative { get; private set; }
        
        [field: SerializeField] public int MovePoints { get; private set; }
        [field: SerializeField] public bool IsMoveThroughObstacles { get; private set; }

        [field: SerializeField] public int Health { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }
    }
}