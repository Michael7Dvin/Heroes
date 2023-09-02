namespace CodeBase.Gameplay.Units.Logic.Parts.Mover
{
    public class UnitMover : IUnitMover
    {
        public UnitMover(int movePoints)
        {
            MovePoints = movePoints;
        }

        public int MovePoints { get; }

    }
}