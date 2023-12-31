using CodeBase.Common.Observable;
using CodeBase.Gameplay.Tiles;

namespace CodeBase.Gameplay.Services.TileSelector
{
    public interface ITileSelector
    {
        public IReadOnlyObservable<Tile> SelectedTile { get;}
        public IReadOnlyObservable<Tile> PreviousTile  { get;}
        
        void Enable();
        void Disable();
    }
}