using CodeBase.Gameplay.Units.View.Parts.DeathAnimator;
using CodeBase.Gameplay.Units.View.Parts.PathMoveAnimator;

namespace CodeBase.Gameplay.Units.View
{
    public class UnitView
    {
        public UnitView(IDeathAnimator deathAnimator, IPathMoveAnimator pathMoveAnimator)
        {
            DeathAnimator = deathAnimator;
            PathMoveAnimator = pathMoveAnimator;
        }

        public IDeathAnimator DeathAnimator { get; }
        public IPathMoveAnimator PathMoveAnimator { get; }
    }
}