using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Gameplay.Units.View.Parts.PathMoveAnimator
{
    public class GroundPathMoveAnimator : IPathMoveAnimator
    {
        private readonly Transform _transform;
        private readonly float _moveAnimationSpeed;

        public GroundPathMoveAnimator(Transform transform, float moveAnimationSpeed)
        {
            _transform = transform;
            _moveAnimationSpeed = moveAnimationSpeed;
        }

        public async UniTask MoveAlongPath(Vector3[] pathPositions)
        {
            await _transform
                .DOPath(pathPositions, _moveAnimationSpeed)
                .SetSpeedBased()
                .SetEase(Ease.Linear)
                .ToUniTask();
        }
    }
}