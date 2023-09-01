using CodeBase.Common.Observable;
using CodeBase.Gameplay.Services.MapService;
using CodeBase.Gameplay.Tiles;
using CodeBase.Infrastructure.Services.InputService;
using CodeBase.Infrastructure.Services.Updater;
using UnityEngine;

namespace CodeBase.Gameplay.Services.TileSelector
{
    public class TileSelector : ITileSelector
    {
        private readonly IMapService _mapService;
        private readonly IInputService _inputService;
        private readonly IUpdater _updater;

        private readonly Observable<Tile> _currentTile = new();
        private readonly Observable<Tile> _previousTile = new();

        public TileSelector(IMapService mapService, IInputService inputService, IUpdater updater)
        {
            _mapService = mapService;
            _inputService = inputService;
            _updater = updater;
        }

        public IReadOnlyObservable<Tile> SelectedTile => _currentTile;
        public IReadOnlyObservable<Tile> PreviousTile => _previousTile;

        public void Enable() => 
            _updater.Updated += SelectTile;

        public void Disable() => 
            _updater.Updated -= SelectTile;

        private void SelectTile(float deltaTime)
        {
            if (TryRaycast(out TileView tileView))
            {
                _previousTile.Value = _currentTile.Value;
                _currentTile.Value = _mapService.GetTile(tileView.Coordinates);
            }
            else
            {
                _previousTile.Value = _currentTile.Value;
                _currentTile.Value = null;
            }
        }

        private bool TryRaycast(out TileView tileView)
        {
            RaycastHit2D hit = Physics2D.Raycast(_inputService.MouseCursorWorldPosition, Vector2.zero);
            
            if (hit.collider != null)
            {
                if (hit.collider.TryGetComponent(out tileView))
                    return true;
            }

            tileView = null;
            return false;
        }
    }
}