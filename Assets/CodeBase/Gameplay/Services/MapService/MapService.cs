﻿using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Tiles;
using CodeBase.Infrastructure.Services.Logging;
using UnityEngine;

namespace CodeBase.Gameplay.Services.MapService
{
    public class MapService : IMapService
    {
        private const int MaxNeighborsCount = 6;
        
        private readonly ICustomLogger _logger;
        private readonly Dictionary<Vector2Int, Tile> _tiles = new();

        private readonly Dictionary<int, Vector2Int[]> _neighborsDirections = new(2)
        {
            { 0, new Vector2Int[] {
                new (1, 0), 
                new (0, -1), 
                new (-1, -1),
                new (-1, 0), 
                new (-1, 1), 
                new (0, 1) 
            }},
            
            { 1, new Vector2Int[] {
                new (1, 0), 
                new (1, -1),
                new (0, -1),
                new (-1, 0),
                new (0, 1), 
                new (1, 1) 
            }}
        };

        public MapService(ICustomLogger logger)
        {
            _logger = logger;
        }

        public event Action<Tile> TileChanged; 

        public void ResetMap(IEnumerable<Tile> map)
        {
            foreach (KeyValuePair<Vector2Int, Tile> keyValuePair in _tiles)
                keyValuePair.Value.Logic.Changed -= OnTileLogicChanged;

            _tiles.Clear();

            foreach (Tile tile in map)
            {
                tile.Logic.Changed += OnTileLogicChanged;
                _tiles[tile.Logic.Coordinates] = tile;
            }
        }

        private void OnTileLogicChanged(TileLogic tileLogic)
        {
            Tile tile = GetTile(tileLogic.Coordinates);
            TileChanged?.Invoke(tile);
        }

        public bool TryGetTile(Vector2Int coordinates, out Tile tile)
        {
            if (_tiles.ContainsKey(coordinates) == true)
            {
                tile = _tiles[coordinates];
                return true;
            }

            tile = null;
            return false;
        }
        
        public Tile GetTile(Vector2Int coordinates)
        {
            if (_tiles.TryGetValue(coordinates, out Tile tile) == true)
                return tile;
            
            _logger.LogWarning($"Unable to get {nameof(Tile)}. {nameof(_tiles)} don't contains key: '{coordinates}'");
            return null;
        }

        public IEnumerable<Tile> GetNeighbors(Vector2Int coordinates)
        {
            List<Tile> neighbors = new(MaxNeighborsCount);

            int parity = coordinates.y % 2;

            foreach (Vector2Int direction in _neighborsDirections[parity])
            {
                Vector2Int neighborCoordinates = coordinates + direction;

                if (TryGetTile(neighborCoordinates, out Tile tile)) 
                    neighbors.Add(tile);
            }

            return neighbors;
        }
    }
}