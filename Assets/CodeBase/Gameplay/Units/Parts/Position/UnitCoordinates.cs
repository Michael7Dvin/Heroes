using UnityEngine;

namespace CodeBase.Gameplay.Units.Parts.Position
{
    public class UnitCoordinates
    {
        public Vector3Int Current { get; private set; }
        
        public void SetCurrent(Vector3Int position) => 
            Current = position;
    }
}