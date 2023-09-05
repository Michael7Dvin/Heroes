using UnityEngine;

namespace CodeBase.Gameplay.Tiles
{
    [RequireComponent(typeof(SpriteRenderer), typeof(PolygonCollider2D))]
    public class Tile : MonoBehaviour
    {
        public void Construct(TileLogic logic)
        {
            Logic = logic;
            
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            Material material = spriteRenderer.material;
            View = new TileView(material);
        }
        
        public TileView View { get; private set; }
        public TileLogic Logic { get; private set; }

        private void OnDestroy() => 
            Logic.CleanUp();
    }
}