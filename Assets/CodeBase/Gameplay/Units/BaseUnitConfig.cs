using UnityEngine;

namespace CodeBase.Gameplay.Units
{
    public class BaseUnitConfig : ScriptableObject
    {
        [field: SerializeField] public int Initiative { get; private set; }
        [field: SerializeField] public int Health { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }
    }
}