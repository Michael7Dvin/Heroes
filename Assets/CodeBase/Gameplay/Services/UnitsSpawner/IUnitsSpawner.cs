using CodeBase.Gameplay.Teams;
using CodeBase.Gameplay.Units;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Gameplay.Services.UnitsSpawner
{
    public interface IUnitsSpawner
    {
        UniTask<Unit> Spawn(Vector3Int position, UnitType unitType, int unitsAmount, TeamID teamID);
    }
}