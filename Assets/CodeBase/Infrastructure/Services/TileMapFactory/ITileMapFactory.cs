using Cysharp.Threading.Tasks;
using UnityEngine.Tilemaps;

namespace CodeBase.Infrastructure.Services.TileMapFactory
{
    public interface ITileMapFactory
    {
        UniTask WarmUp();
        UniTask<Tilemap> Create();
    }
}