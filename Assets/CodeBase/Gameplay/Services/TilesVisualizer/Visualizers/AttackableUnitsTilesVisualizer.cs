using System.Collections.Generic;
using CodeBase.Gameplay.Services.Attacker;
using CodeBase.Gameplay.Services.MapService;
using CodeBase.Gameplay.Tiles;
using CodeBase.Gameplay.Units;
using CodeBase.Infrastructure.Services.StaticDataProvider;

namespace CodeBase.Gameplay.Services.TilesVisualizer.Visualizers
{
    public class AttackableUnitsTilesVisualizer
    {
        private readonly IAttacker _attacker;
        private readonly IMapService _mapService;
        private readonly TileViewColorsConfig _tileViewColors;

        private readonly List<TileView> _visualizedTiles = new();

        public AttackableUnitsTilesVisualizer(IAttacker attacker,
            IMapService mapService,
            IStaticDataProvider staticDataProvider)
        {
            _attacker = attacker;
            _mapService = mapService;
            _tileViewColors = staticDataProvider.Configs.TileViewColors;
        }

        public void Enable() => 
            _attacker.AttackableEnemiesUpdated += ClearAndVisualizeAttackableTiles;

        public void Disable() => 
            _attacker.AttackableEnemiesUpdated -= ClearAndVisualizeAttackableTiles;

        private void ClearAndVisualizeAttackableTiles()
        {
            foreach (TileView tileView in _visualizedTiles) 
                tileView.SwitchHighlight(false);

            _visualizedTiles.Clear();
            
            foreach (Unit unit in _attacker.AttackableEnemies)
            {
                TileView tileView = _mapService.GetTile(unit.Logic.Coordinates.Observable.Value).View;
                tileView.SwitchHighlight(true);
                tileView.ChangeHighlightColor(_tileViewColors.AttackableHighlight);
                
                _visualizedTiles.Add(tileView);
            }
        }
    }
}