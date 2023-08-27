using UnityEngine;

namespace CodeBase.Gameplay.Tiles
{
    [RequireComponent(typeof(SpriteRenderer), typeof(PolygonCollider2D))]
    public class TileView : MonoBehaviour
    {
        private SpriteRenderer _renderer;
        private Material _defaultMaterial;

        public void Construct(Vector2Int coordinates)
        {
            Coordinates = coordinates;
        }

        public Vector2Int Coordinates { get; private set; }

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _defaultMaterial = _renderer.material;
        }

        public void SetMaterial(Material material)
        {
            _renderer.material = material;
        }

        public void SetDefaultMaterial()
        {
            _renderer.material = _defaultMaterial;
        }
    }
}