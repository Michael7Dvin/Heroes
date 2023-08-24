using CodeBase.Gameplay.Units;
using UnityEngine;

namespace CodeBase.Gameplay.Services.Mover
{
    public interface IMover
    {
        void Move(Vector3Int position, Unit unit);
    }
}