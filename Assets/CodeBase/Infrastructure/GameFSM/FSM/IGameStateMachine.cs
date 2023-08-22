using CodeBase.Infrastructure.GameFSM.States;
using CodeBase.Infrastructure.GameFSM.States.Base;

namespace CodeBase.Infrastructure.GameFSM.FSM
{
    public interface IGameStateMachine
    {
        void EnterState<TState>() where TState : IState;
        void EnterState<TState, TArgument>(TArgument argument) where TState : IStateWithArgument<TArgument>;
        void AddState<TState>(TState state) where TState : IExitableState;
    }
}