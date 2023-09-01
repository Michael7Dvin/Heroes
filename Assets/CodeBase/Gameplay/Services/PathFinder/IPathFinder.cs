using UnityEngine;

namespace CodeBase.Gameplay.Services.PathFinder
{
    public interface IPathFinder
    {
        PathFindingResults FindPathsByBFS(Vector2Int start, int maxDistance);
    }
}