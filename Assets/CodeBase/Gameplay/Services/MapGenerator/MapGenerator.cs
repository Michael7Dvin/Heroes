using System.Collections.Generic;
using CodeBase.Gameplay.Tiles;
using CodeBase.Infrastructure.Services.TileFactory;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Gameplay.Services.MapGenerator
{
    public class MapGenerator : IMapGenerator 
    {
        private const int ColsCount = 15;
        private const int RowsCount = 11;
        private const float TilesXGap = 0.93f;

        private readonly ITileFactory _tileFactory;

        public MapGenerator(ITileFactory tileFactory)
        {
            _tileFactory = tileFactory;
        }

        private Vector3 CenteringOffset => new(-6.975f, 3.4875f, 0f);
        private float OddRowsXOffset => TilesXGap * 0.5f;
        private float TilesYGap => TilesXGap * 0.75f;
        
        public async UniTask<IEnumerable<Tile>> Generate()
        {
            List<Tile> tiles = new(ColsCount * RowsCount);

            for (int row = 0; row < RowsCount; row++)
            {
                for (int col = 0; col < ColsCount; col++)
                {
                    Vector3 position = new Vector3(col * TilesXGap, -row * TilesYGap) + CenteringOffset;
                    
                    if (IsOdd(row) == true)
                    {
                        position.x += OddRowsXOffset;
                    }
                    
                    Vector2Int coordinates = new Vector2Int(col, row);
                    
                    Tile tile = await _tileFactory.Create(position, coordinates, true);
                    tiles.Add(tile);
                }
            }
            
            return tiles;
        }
        
        private bool IsOdd(int number) => 
            number % 2 != 0;
    }
}