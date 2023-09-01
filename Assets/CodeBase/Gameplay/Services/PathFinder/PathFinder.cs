using System.Collections.Generic;
using CodeBase.Gameplay.Services.MapService;
using CodeBase.Gameplay.Tiles;
using UnityEngine;

namespace CodeBase.Gameplay.Services.PathFinder
{
    public class PathFinder : IPathFinder
    {
        private readonly IMapService _mapService;

        public PathFinder(IMapService mapService)
        {
            _mapService = mapService;
        }

        public PathFindingResults FindPathsByBFS(Vector2Int start, int maxDistance)
        {
            Dictionary<Vector2Int, Vector2Int?> visited = new();
            Dictionary<Vector2Int, int> distances = new();
            Queue<Vector2Int> toVisit = new();
            
            toVisit.Enqueue(start);
            distances.Add(start, 0);
            visited.Add(start, null);

            while (toVisit.Count > 0)
            {
                Vector2Int calculatingTile = toVisit.Dequeue();

                foreach (Tile neighbor in _mapService.GetNeighbors(calculatingTile))
                {
                    Vector2Int neighborCoordinates = neighbor.View.Coordinates;

                    if (neighbor.Logic.IsWalkable == false)
                        continue;

                    int distance = distances[calculatingTile] + 1;

                    if (distance <= maxDistance)
                    {
                        if (visited.ContainsKey(neighborCoordinates) == false)
                        {
                            visited[neighborCoordinates] = calculatingTile;
                            distances[neighborCoordinates] = distance;
                            toVisit.Enqueue(neighborCoordinates);
                        }
                        else if (distances[neighborCoordinates] > distance)
                        {
                            visited[neighborCoordinates] = calculatingTile;
                            distances[neighborCoordinates] = distance;
                        }
                    }
                }
            }

            return new PathFindingResults(visited);
        }
    }
}