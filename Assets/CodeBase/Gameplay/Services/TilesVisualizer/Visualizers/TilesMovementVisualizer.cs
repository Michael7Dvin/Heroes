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
        private readonly TileViewColorsConfig _tileViewColors;

        private readonly List<TileView> _visualizedMovableTiles = new();
        private readonly List<TileView> _visualizedPathTiles = new();

        public TilesMovementVisualizer(IMapService mapService,
            IMover mover,
            ITileSelector tileSelector,
            IStaticDataProvider staticDataProvider)
        {
            _mapService = mapService;
            _mover = mover;
            _tileSelector = tileSelector;
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
            List<Vector2Int> coordinates = _mover.CurrentPathFindingResults.Value.GetPathTo(tile.View.Coordinates);

            foreach (Vector2Int coordinate in coordinates)
            {
                TileView pathTileView = _mapService.GetTile(coordinate).View;
                pathTileView.SwitchHighlight(true);
                pathTileView.ChangeHighlightColor(_tileViewColors.PathHighlight);
                _visualizedPathTiles.Add(pathTileView);
            }
        }

        private void VisualizePathAsMovable()
        {
            foreach (TileView pathTileView in _visualizedPathTiles)
            {
                pathTileView.SwitchHighlight(true);   
                pathTileView.ChangeHighlightColor(_tileViewColors.MovableHighlight);   
            }
            
            _visualizedPathTiles.Clear();
        }

        private void ClearPathVisualization()
        {
            foreach (TileView pathTileView in _visualizedPathTiles) 
                pathTileView.SwitchHighlight(false);

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
            foreach (TileView tileView in _visualizedMovableTiles)
                tileView.SwitchHighlight(false);

            _visualizedMovableTiles.Clear();
        }

        private bool IsWalkable(Tile tile) => 
            _mover.CurrentPathFindingResults.Value.IsMovableAt(tile.View.Coordinates);
    }
}