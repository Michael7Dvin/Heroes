using CodeBase.Infrastructure.GameFSM.FSM;
using CodeBase.Infrastructure.GameFSM.States;

namespace CodeBase.UI.Windows.ResultsWindow
{
    public class ResultsWindowLogic
    {
        private readonly IGameStateMachine _gameStateMachine;

        public ResultsWindowLogic(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Restart() => 
            _gameStateMachine.EnterState<RestartState>();
    }
}