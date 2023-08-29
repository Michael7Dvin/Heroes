using System.Collections.Generic;
using CodeBase.Gameplay.Tiles;
using Cysharp.Threading.Tasks;

namespace CodeBase.Gameplay.Services.Map.MapGenerator
{
    public interface IMapGenerator
    {
        UniTask<IEnumerable<Tile>> Generate();
    }
}