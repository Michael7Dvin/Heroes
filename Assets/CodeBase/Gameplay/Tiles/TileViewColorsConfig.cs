using UnityEngine;

namespace CodeBase.Gameplay.Tiles
{
    [CreateAssetMenu(fileName = "Tile View Colors Config", menuName = "Configs/UI/Tile View Colors")]
    public class TileViewColorsConfig : ScriptableObject
    {
        [field: SerializeField] public Color EmptySelectionOutline { get; private set; }
        
        [field: SerializeField] public Color MoveTargetOutline { get; private set; }
        [field: SerializeField] public Color MovableHighlight { get; private set; }
        [field: SerializeField] public Color PathHighlight { get; private set; }
        
        [field: SerializeField] public Color AttackTargetOutline { get; private set; }
        [field: SerializeField] public Color AttackableHighlight { get; private set; }
        
        [field: SerializeField] public Color AllyOutline { get; private set; }
        
        [field: SerializeField] public Color ActiveUnitOutline { get; private set; }
        [field: SerializeField] public Color ActiveUnitHighlight { get; private set; }
    }
}