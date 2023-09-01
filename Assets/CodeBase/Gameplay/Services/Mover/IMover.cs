using CodeBase.Common.Observable;
using CodeBase.Gameplay.Services.PathFinder;
using CodeBase.Gameplay.Tiles;

namespace CodeBase.Gameplay.Services.Mover
{
    public interface IMover
    {
        void Enable();
        void Disable();
        
        bool IsMovableAt(Tile tile);
        void MoveActiveUnit(Tile tile);
        IReadOnlyObservable<PathFindingResults> CurrentPathFindingResults { get; }
    }
}