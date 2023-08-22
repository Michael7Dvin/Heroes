using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.GameFSM.States.Base;
using CodeBase.Infrastructure.Services.Logging;

namespace CodeBase.Infrastructure.GameFSM.FSM
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly ICustomLogger _logger;
        
        private readonly Dictionary<Type, IExitableState> _states = new();
        private IExitableState _activeState;

        public GameStateMachine(ICustomLogger logger)
        {
            _logger = logger;
        }

        public void EnterState<TState>() where TState : IState
        {
            LogStateEnter<TState>();
            
            IExitableState newState = _states[typeof(TState)];

            _activeState?.Exit();
            _activeState = newState;
            ((IState)newState).Enter();
        }
        
        public void EnterState<TState, TArgs>(TArgs args) where TState : IStateWithArgument<TArgs>
        {
            LogStateEnter<TState>();
            
            IExitableState newState = _states[typeof(TState)];
        
            _activeState?.Exit();
            _activeState = newState;
            ((IStateWithArgument<TArgs>)newState).Enter(args);
        }
    
        public void AddState<TState>(TState state) where TState : IExitableState
        {
            Type stateType = typeof(TState); 
        
            if (_states.ContainsKey(stateType) == true) 
                return;
    
            _states.Add(stateType, state);
        }
        
        private void LogStateEnter<TState>() where TState : IExitableState => 
            _logger.Log($"Entered: {typeof(TState).Name}");
    }
}