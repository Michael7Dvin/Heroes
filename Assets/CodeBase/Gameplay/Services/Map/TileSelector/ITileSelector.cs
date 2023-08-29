using CodeBase.Common.Observable;
using CodeBase.Gameplay.Tiles;

namespace CodeBase.Gameplay.Services.Map.TileSelector
{
    public interface ITileSelector
    {
        public IReadOnlyObservable<Tile> CurrentTile { get;}
        public IReadOnlyObservable<Tile> PreviousTile  { get;}
        
        void Enable();
        void Disable();
    }
}