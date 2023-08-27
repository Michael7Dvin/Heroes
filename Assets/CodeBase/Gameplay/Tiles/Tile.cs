namespace CodeBase.Gameplay.Tiles
{
    public class Tile
    {
        public TileLogic Logic { get; }
        public TileView View { get; }

        public Tile(TileLogic logic, TileView view)
        {
            Logic = logic;
            View = view;
        }
    }
}