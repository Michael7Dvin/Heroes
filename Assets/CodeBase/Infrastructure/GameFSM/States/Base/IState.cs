namespace CodeBase.Infrastructure.GameFSM.States.Base
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}