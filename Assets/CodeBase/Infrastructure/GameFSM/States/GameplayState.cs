using CodeBase.Gameplay.Services.Map.MapVisualizer;
using CodeBase.Gameplay.Services.Map.TileInteractor;
using CodeBase.Gameplay.Services.Map.TileSelector;
using CodeBase.Gameplay.Services.TeamWinObserver;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Gameplay.Units.Parts.Team;
using CodeBase.Infrastructure.GameFSM.States.Base;

namespace CodeBase.Infrastructure.GameFSM.States
{
    public class GameplayState : IState
    {
        private readonly ITurnQueue _turnQueue;
        private readonly ITileSelector _tileSelector;
        private readonly ITileInteractor _tileInteractor;
        private readonly IMapVisualizer _mapVisualizer;

        public GameplayState(ITurnQueue turnQueue,
            ITileSelector tileSelector,
            ITileInteractor tileInteractor,
            IMapVisualizer mapVisualizer)
        {
            _turnQueue = turnQueue;
            _tileSelector = tileSelector;
            _tileInteractor = tileInteractor;
            _mapVisualizer = mapVisualizer;
        }

        public void Enter()
        {
            _turnQueue.SetFirstTurn();
            
            _tileSelector.Enable();
            _tileInteractor.Enable();
            _mapVisualizer.Enable();
        }

        public void Exit()
        {
            _turnQueue.CleanUp();
            
            _tileSelector.Disable();
            _tileInteractor.Disable();
            _mapVisualizer.Disable();
        }
    }
}