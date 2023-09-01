using System.Collections.Generic;
using CodeBase.Gameplay.Tiles;
using Cysharp.Threading.Tasks;

namespace CodeBase.Gameplay.Services.MapGenerator
{
    public interface IMapGenerator
    {
        UniTask<IEnumerable<Tile>> Generate();
    }
}