namespace CodeBase.Gameplay.Units.Logic.Parts.Mover
{
    public class UnitMover : IUnitMover
    {
        public UnitMover(int movePoints, bool isMoveThroughObstacles)
        {
            MovePoints = movePoints;
            IsMoveThroughObstacles = isMoveThroughObstacles;
        }

        public int MovePoints { get; }
        public bool IsMoveThroughObstacles { get; }
    }
}