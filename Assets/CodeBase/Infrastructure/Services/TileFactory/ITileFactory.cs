using CodeBase.Gameplay.Tiles;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.TileFactory
{
    public interface ITileFactory
    {
        UniTask WarmUp();
        UniTask<Tile> Create(Vector3 position, Vector2Int coordinates);
    }
}