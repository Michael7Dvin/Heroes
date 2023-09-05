using System.Collections.Generic;
using CodeBase.Gameplay.Services.MapService;
using CodeBase.Gameplay.Services.Mover;
using CodeBase.Gameplay.Services.PathFinder;
using CodeBase.Gameplay.Services.TileSelector;
using CodeBase.Gameplay.Tiles;
using CodeBase.Infrastructure.Services.StaticDataProvider;
using UnityEngine;

namespace CodeBase.Gameplay.Services.TilesVisualizer.Visualizers
{
    public class TilesMovementVisualizer
    {
        private readonly IMapService _mapService;
        private readonly IMover _mover;
        private readonly ITileSelector _tileSelector;
        private readonly ActiveUnitTileVisualizer _activeUnitTileVisualizer;
        private readonly TileViewColorsConfig _tileViewColors;

        private readonly List<TileView> _visualizedMovableTiles = new();
        private readonly List<Tile> _visualizedPathTiles = new();

        public TilesMovementVisualizer(IMapService mapService,
            IMover mover,
            ITileSelector tileSelector,
            ActiveUnitTileVisualizer activeUnitTileVisualizer,
            IStaticDataProvider staticDataProvider)
        {
            _mapService = mapService;
            _mover = mover;
            _tileSelector = tileSelector;
            _activeUnitTileVisualizer = activeUnitTileVisualizer;
            
            _tileViewColors = staticDataProvider.Configs.TileViewColors;
        }

        public void Enable()
        {
            _tileSelector.SelectedTile.Changed += OnSelectedTileChanged;
            _mover.CurrentPathFindingResults.Changed += OnCurrentPathFindingResultsChanged;
        }

        public void Disable()
        {
            _tileSelector.SelectedTile.Changed -= OnSelectedTileChanged;
            _mover.CurrentPathFindingResults.Changed -= OnCurrentPathFindingResultsChanged;
        }

        private void OnSelectedTileChanged(Tile tile)
        {
            VisualizePathAsMovable();
            
            if (tile != null)
            {
                if (IsWalkable(tile)) 
                    VisualizePath(tile);
            }
        }

        private void OnCurrentPathFindingResultsChanged(PathFindingResults results)
        {
            ClearPathVisualization();
            ClearMovableTilesVisualization();
            VisualizeMovableTiles(results.WalkableCoordinates);
        }

        private void VisualizePath(Tile tile)
        {
            List<Vector2Int> coordinates = _mover.CurrentPathFindingResults.Value.GetPathTo(tile.Logic.Coordinates);

            foreach (Vector2Int coordinate in coordinates)
            {
                Tile pathTile = _mapService.GetTile(coordinate);
                pathTile.View.SwitchHighlight(true);
                pathTile.View.ChangeHighlightColor(_tileViewColors.PathHighlight);
                _visualizedPathTiles.Add(pathTile);
            }
        }

        private void VisualizePathAsMovable()
        {
            foreach (Tile pathTile in _visualizedPathTiles)
            {
                TileView view = pathTile.View;
                
                if (_mover.CurrentPathFindingResults.Value.IsMovableAt(pathTile.Logic.Coordinates) == false)
                {
                    view.SwitchHighlight(false);
                    continue;
                }
                
                view.SwitchHighlight(true);   
                view.ChangeHighlightColor(_tileViewColors.MovableHighlight);   
            }
            
            _visualizedPathTiles.Clear();
        }

        private void ClearPathVisualization()
        {
            foreach (Tile pathTile in _visualizedPathTiles) 
                pathTile.View.SwitchHighlight(false);

            _visualizedPathTiles.Clear();
        }

        private void VisualizeMovableTiles(IEnumerable<Vector2Int> walkableCoordinates)
        {
            foreach (Vector2Int coordinate in walkableCoordinates)
            {
                TileView tileView = _mapService.GetTile(coordinate).View;

                tileView.SwitchHighlight(true);
                tileView.ChangeHighlightColor(_tileViewColors.MovableHighlight);

                _visualizedMovableTiles.Add(tileView);
            }
        }

        private void ClearMovableTilesVisualization()
        {
            _visualizedMovableTiles.Remove(_activeUnitTileVisualizer.ActiveUnitTileView); 
            
            foreach (TileView tileView in _visualizedMovableTiles)
                tileView.SwitchHighlight(false);

            _visualizedMovableTiles.Clear();
        }

        private bool IsWalkable(Tile tile) => 
            _mover.CurrentPathFindingResults.Value.IsMovableAt(tile.Logic.Coordinates);
    }
}