using UnityEngine;

namespace CodeBase.Gameplay.Units.Parts.Coordinates
{
    public class UnitCoordinates
    {
        public Vector2Int Value { get; private set; }

        public void Set(Vector2Int coordinates)
        {
            Value = coordinates;
        }
    }
}