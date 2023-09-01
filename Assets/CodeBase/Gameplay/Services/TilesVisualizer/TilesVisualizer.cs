using CodeBase.Gameplay.Services.MapService;
using CodeBase.Gameplay.Services.Mover;
using CodeBase.Gameplay.Services.TileSelector;
using CodeBase.Gameplay.Services.TilesVisualizer.Visualizers;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Infrastructure.Services.StaticDataProvider;

namespace CodeBase.Gameplay.Services.TilesVisualizer
{
    public class TilesVisualizer : ITilesVisualizer
    {
        private readonly ActiveUnitTileVisualizer _activeUnitTileVisualizer;
        private readonly TileSelectionVisualizer _tileSelectionVisualizer;
        private readonly TilesMovementVisualizer _tilesMovementVisualizer;
        
        public TilesVisualizer(ITurnQueue turnQueue,
            IMapService mapService,
            IMover mover,
            ITileSelector tileSelector,
            IStaticDataProvider staticDataProvider)
        {
            _activeUnitTileVisualizer = new ActiveUnitTileVisualizer(turnQueue, mapService, staticDataProvider);
            
            _tileSelectionVisualizer = new TileSelectionVisualizer(tileSelector,
                turnQueue,
                mover,
                _activeUnitTileVisualizer,
                staticDataProvider);
            
            _tilesMovementVisualizer = new TilesMovementVisualizer(mapService, mover, tileSelector, staticDataProvider);
        }

        public void Enable()
        {
            _activeUnitTileVisualizer.Enable(); 
            _tileSelectionVisualizer.Enable();
            _tilesMovementVisualizer.Enable();
        }

        public void Disable()
        {
            _activeUnitTileVisualizer.Disable(); 
            _tileSelectionVisualizer.Disable();
            _tilesMovementVisualizer.Disable();
        }
    }
}