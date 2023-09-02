using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Gameplay.Units.View.Parts.PathMoveAnimator
{
    public interface IPathMoveAnimator
    {
        UniTask MoveAlongPath(Vector3[] pathPositions);
    }
}