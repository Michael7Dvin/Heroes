namespace CodeBase.Gameplay.Tiles
{
    public class Tile
    {
        public Tile(TileLogic logic, TileView view)
        {
            Logic = logic;
            View = view;
        }

        public TileLogic Logic { get; }
        public TileView View { get; }
    }
}