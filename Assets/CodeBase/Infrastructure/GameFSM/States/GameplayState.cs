using CodeBase.Gameplay.Services.Mover;
using CodeBase.Gameplay.Services.TileInteractor;
using CodeBase.Gameplay.Services.TileSelector;
using CodeBase.Gameplay.Services.TilesVisualizer;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Infrastructure.GameFSM.States.Base;

namespace CodeBase.Infrastructure.GameFSM.States
{
    public class GameplayState : IState
    {
        private readonly ITileSelector _tileSelector;
        private readonly ITileInteractor _tileInteractor;
        private readonly ITilesVisualizer _tilesVisualizer;
        private readonly IMover _mover;
        private readonly ITurnQueue _turnQueue;

        public GameplayState(ITileSelector tileSelector,
            ITileInteractor tileInteractor,
            ITilesVisualizer tilesVisualizer,
            IMover mover,
            ITurnQueue turnQueue)
        {
            _tileSelector = tileSelector;
            _tileInteractor = tileInteractor;
            _tilesVisualizer = tilesVisualizer;
            _mover = mover;
            _turnQueue = turnQueue;
        }

        public void Enter()
        {
            _tileSelector.Enable();
            _tileInteractor.Enable();
            _tilesVisualizer.Enable();
            _mover.Enable();
            
            _turnQueue.SetFirstTurn();
        }

        public void Exit()
        {
            _tileSelector.Disable();
            _tileInteractor.Disable();
            _tilesVisualizer.Disable();
            _mover.Disable();
            
            _turnQueue.CleanUp();
        }
    }
}