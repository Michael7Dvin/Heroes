using UnityEngine;

namespace CodeBase.Gameplay.Tiles
{
    [RequireComponent(typeof(SpriteRenderer), typeof(PolygonCollider2D))]
    public class TileView : MonoBehaviour
    {
        private readonly int _enableOutline = Shader.PropertyToID("_EnableOutline");
        private readonly int _outlineColor = Shader.PropertyToID("_OutlineColor");
        private readonly int _enableHighLight = Shader.PropertyToID("_EnableHighlight");
        private readonly int _highlightColor = Shader.PropertyToID("_HighlightColor");

        private TileViewColorsConfig _config;
        private SpriteRenderer _renderer;
        private Material _tileMaterial;
        
        public void Construct(Vector2Int coordinates, TileViewColorsConfig tileViewColorsConfig)
        {
            Coordinates = coordinates;
            _config = tileViewColorsConfig;
        }

        public Vector2Int Coordinates { get; private set; }

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _tileMaterial = _renderer.material;
        }

        public void SwitchMovableEffect(bool value)
        {
            if (value == true)
            {
                SetColor(_highlightColor, _config.MovableHighlightColor);
                SetBool(_enableHighLight, true);
            }
            else
                SetBool(_enableHighLight, false);
        }
        
        public void SwitchAttackableEffect(bool value)
        {
            if (value == true)
            {
                SetColor(_highlightColor, _config.AttackableHighlightColor);
                SetBool(_enableHighLight, true);
            }
            else
                SetBool(_enableHighLight, false);
        }

        public void SwitchMoveTargetEffect(bool value)
        {
            if (value == true)
            {
                SetColor(_outlineColor, _config.MoveTargetOutlineColor);
                SetBool(_enableOutline, true);
            }
            else
                SetBool(_enableOutline, false);
        }

        public void SwitchAttackTargetEffect(bool value)
        {
            if (value == true)
            {
                SetColor(_outlineColor, _config.AttackTargetOutlineColor);
                SetBool(_enableOutline, true);
            }
            else
                SetBool(_enableOutline, false);
        }

        private void SetColor(int propertyID, Color color) => 
            _tileMaterial.SetColor(propertyID, color);
        
        private void SetBool(int propertyID, bool value)
        {
            int intValue = value ? 1 : 0;
            _tileMaterial.SetInt(propertyID, intValue);
        }
    }
}