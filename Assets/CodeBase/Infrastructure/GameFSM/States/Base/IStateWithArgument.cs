namespace CodeBase.Infrastructure.GameFSM.States.Base
{
    public interface IStateWithArgument<in TArgument> : IExitableState
    {
        void Enter(TArgument argument);
    }
}