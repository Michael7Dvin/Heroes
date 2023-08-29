using CodeBase.Gameplay.Services.Map.TileSelector;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Gameplay.Tiles;
using CodeBase.Gameplay.Units;
using CodeBase.Gameplay.Units.Parts.Team;

namespace CodeBase.Gameplay.Services.Map.MapVisualizer
{
    public class MapVisualizer : IMapVisualizer
    {
        private readonly ITileSelector _tileSelector;
        private readonly ITurnQueue _turnQueue;

        public MapVisualizer(ITileSelector tileSelector, ITurnQueue turnQueue)
        {
            _tileSelector = tileSelector;
            _turnQueue = turnQueue;
        }

        public void Enable()
        {
            _tileSelector.CurrentTile.Changed += VisualiseTileSelection;
            _tileSelector.PreviousTile.Changed += ClearTileSelectionVisualisation;
        }

        public void Disable()
        {
            _tileSelector.CurrentTile.Changed -= VisualiseTileSelection;
            _tileSelector.PreviousTile.Changed -= ClearTileSelectionVisualisation;
        }
        
        private void VisualiseTileSelection(Tile tile)
        {
            if (tile != null)
            {
                if (tile.Logic.TryGetUnit(out Unit unit))
                {
                    if (IsEnemy(unit))
                    {
                        tile.View.SwitchAttackTargetEffect(true);
                        tile.View.SwitchAttackableEffect(true);
                    }
                }

                if (tile.Logic.IsOccupied == false)
                {
                    tile.View.SwitchMoveTargetEffect(true);
                    tile.View.SwitchMovableEffect(true);
                }
            }
        }

        private void ClearTileSelectionVisualisation(Tile tile)
        {
            if (tile != null)
            {
                tile.View.SwitchMovableEffect(false);
                tile.View.SwitchMoveTargetEffect(false);
                tile.View.SwitchAttackTargetEffect(false);   
            }
        }
        
        private bool IsEnemy(Unit unit)
        {
            TeamID activeUnitTeamID = _turnQueue.ActiveUnit.Team.Current.Value;
            TeamID unitAtTileTeamID = unit.Team.Current.Value;

            return activeUnitTeamID != unitAtTileTeamID;
        }
    }
}