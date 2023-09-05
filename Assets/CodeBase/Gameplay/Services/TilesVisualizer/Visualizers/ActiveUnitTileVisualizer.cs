using CodeBase.Gameplay.Services.MapService;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Gameplay.Tiles;
using CodeBase.Gameplay.Units;
using CodeBase.Gameplay.Units.Logic;
using CodeBase.Infrastructure.Services.StaticDataProvider;
using UnityEngine;

namespace CodeBase.Gameplay.Services.TilesVisualizer.Visualizers
{
    public class ActiveUnitTileVisualizer
    {
        private readonly ITurnQueue _turnQueue;
        private readonly IMapService _mapService;
        private readonly TileViewColorsConfig _tileViewColors;
        
        private UnitLogic _activeUnitLogic;

        public ActiveUnitTileVisualizer(ITurnQueue turnQueue,
            IMapService mapService,
            IStaticDataProvider staticDataProvider)
        {
            _turnQueue = turnQueue;
            _mapService = mapService;
            
            _tileViewColors = staticDataProvider.Configs.TileViewColors;
        }

        public TileView ActiveUnitTileView { get; private set; }

        public void Enable() => 
            _turnQueue.NewTurnStarted += OnNewTurnStarted;

        public void Disable() => 
            _turnQueue.NewTurnStarted -= OnNewTurnStarted;

        private void OnNewTurnStarted(Unit unit)
        {
            if (_activeUnitLogic != null) 
                _activeUnitLogic.Coordinates.Observable.Changed -= ClearPreviousTileAndVisualizeNew;

            _activeUnitLogic = _turnQueue.ActiveUnit.Logic;
            _activeUnitLogic.Coordinates.Observable.Changed += ClearPreviousTileAndVisualizeNew;

            ClearPreviousTileAndVisualizeNew(_activeUnitLogic.Coordinates.Observable.Value);
        }
        
        private void ClearPreviousTileAndVisualizeNew(Vector2Int coordinates)
        {
            ClearVisualization();
            
            TileView tileView = _mapService.GetTile(coordinates).View;
            Visualize(tileView);
        }

        private void ClearVisualization()
        {
            if (ActiveUnitTileView != null)
            {
                ActiveUnitTileView.SwitchHighlight(false);
                ActiveUnitTileView.SwitchOutLine(false);
            }
        }

        private void Visualize(TileView tileView)
        {
            tileView.SwitchHighlight(true);
            tileView.ChangeHighlightColor(_tileViewColors.ActiveUnitHighlight);
            
            tileView.SwitchOutLine(true);
            tileView.ChangeOutLineColor(_tileViewColors.ActiveUnitOutline);

            ActiveUnitTileView = tileView;
        }
    }
}