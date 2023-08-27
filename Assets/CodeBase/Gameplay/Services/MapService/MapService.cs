using System.Collections.Generic;
using CodeBase.Gameplay.Tiles;
using CodeBase.Infrastructure.Services.Logging;
using UnityEngine;

namespace CodeBase.Gameplay.Services.MapService
{
    public class MapService : IMapService
    {
        private readonly ICustomLogger _logger;
        private readonly Dictionary<Vector2Int,Tile> _tiles = new();

        public MapService(ICustomLogger logger)
        {
            _logger = logger;
        }
        
        public Tile GetTile(Vector2Int coordinates)
        {
            if (_tiles.TryGetValue(coordinates, out Tile tile))
                return tile;
            
            _logger.LogWarning($"Unable to get {nameof(Tile)}. {nameof(_tiles)} don't contain coordinates: '{coordinates}'");
            return null;
        }
        
        public void ResetMap(IEnumerable<Tile> map)
        {
            _tiles.Clear();

            foreach (Tile tile in map) 
                _tiles[tile.View.Coordinates] = tile;
        }
    }
}