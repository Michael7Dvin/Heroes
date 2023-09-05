using CodeBase.Gameplay.Services.Mover;
using CodeBase.Gameplay.Services.TileSelector;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Gameplay.Tiles;
using CodeBase.Gameplay.Units;
using CodeBase.Gameplay.Units.Logic;
using CodeBase.Gameplay.Units.Logic.Parts.Team;
using CodeBase.Infrastructure.Services.StaticDataProvider;

namespace CodeBase.Gameplay.Services.TilesVisualizer.Visualizers
{
    public class TileSelectionVisualizer
    {
        private readonly ITileSelector _tileSelector;
        private readonly ITurnQueue _turnQueue;
        private readonly IMover _mover;
        private readonly ActiveUnitTileVisualizer _activeUnitTileVisualization;
        private readonly TileViewColorsConfig _tileViewColors;

        private TileView _selectedTile;

        public TileSelectionVisualizer(ITileSelector tileSelector,
            ITurnQueue turnQueue,
            IMover mover,
            ActiveUnitTileVisualizer activeUnitTileVisualization,
            IStaticDataProvider staticDataProvider)
        {
            _tileSelector = tileSelector;
            _turnQueue = turnQueue;
            _mover = mover;
            _activeUnitTileVisualization = activeUnitTileVisualization;

            _tileViewColors = staticDataProvider.Configs.TileViewColors;
        }

        public void Enable() => 
            _tileSelector.SelectedTile.Changed += OnSelectedTileChanged;

        public void Disable() => 
            _tileSelector.SelectedTile.Changed -= OnSelectedTileChanged;

        private void OnSelectedTileChanged(Tile tile)
        {
            if (_selectedTile != null)
                ClearVisualization(_selectedTile);

            if (tile != null)
            {
                _selectedTile = tile.View;
                VisualiseSelection(tile);
            }
        }

        private void VisualiseSelection(Tile tile)
        {
            if (tile.Logic.IsOccupied == true)
            {
                Unit tileUnit = tile.Logic.Unit;
                
                VisualizeUnitSelection(tile.View, tileUnit.Logic);
                return;
            }

            if (IsWalkable(tile) == true)
            {
                VisualizeMoveTargetSelection(tile.View);
                return;
            }

            VisualizeEmptySelection(tile.View);
        }

        private void VisualizeUnitSelection(TileView tileView, UnitLogic unitLogic)
        {
            if (IsEnemy(unitLogic.Team.Current.Value) == true)
            {
                tileView.SwitchOutLine(true);
                tileView.ChangeOutLineColor(_tileViewColors.AttackTargetOutline);
            }
            else if (unitLogic != _turnQueue.ActiveUnit.Logic)
            {
                tileView.SwitchOutLine(true);
                tileView.ChangeOutLineColor(_tileViewColors.AllyOutline);
            }
        }

        private void VisualizeMoveTargetSelection(TileView tileView)
        {
            tileView.SwitchOutLine(true);
            tileView.ChangeOutLineColor(_tileViewColors.MoveTargetOutline);
        }

        private void VisualizeEmptySelection(TileView tileView)
        {
            tileView.SwitchOutLine(true);
            tileView.ChangeOutLineColor(_tileViewColors.EmptySelectionOutline);
        }

        private void ClearVisualization(TileView tileView)
        {
            if (tileView != _activeUnitTileVisualization.ActiveUnitTileView) 
                tileView.SwitchOutLine(false);
        }

        private bool IsEnemy(TeamID unitTeamID)
        {
            TeamID activeUnitTeamID = _turnQueue.ActiveUnit.Logic.Team.Current.Value;

            return activeUnitTeamID != unitTeamID;
        }
        
        private bool IsWalkable(Tile tile) => 
            _mover.CurrentPathFindingResults.Value.IsMovableAt(tile.Logic.Coordinates);
    }
}