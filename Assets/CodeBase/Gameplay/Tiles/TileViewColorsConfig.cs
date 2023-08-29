using UnityEngine;

namespace CodeBase.Gameplay.Tiles
{
    [CreateAssetMenu(fileName = "Tile View Colors Config", menuName = "Configs/UI/Tile View Colors")]
    public class TileViewColorsConfig : ScriptableObject
    {
        [field: SerializeField] public Color MoveTargetOutlineColor { get; private set; }
        [field: SerializeField] public Color AttackTargetOutlineColor { get; private set; }
        [field: SerializeField] public Color MovableHighlightColor { get; private set; }
        [field: SerializeField] public Color AttackableHighlightColor { get; private set; }
    }
}