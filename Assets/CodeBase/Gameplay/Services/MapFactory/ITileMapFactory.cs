using Cysharp.Threading.Tasks;
using UnityEngine.Tilemaps;

namespace CodeBase.Gameplay.Services.MapFactory
{
    public interface ITileMapFactory
    {
        UniTask<Tilemap> Create();
    }
}