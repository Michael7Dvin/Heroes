using CodeBase.Gameplay.Units;
using CodeBase.Gameplay.Units.Parts.Team;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Gameplay.Services.UnitsSpawner
{
    public interface IUnitsSpawner
    {
        UniTask<Unit> Spawn(Vector3Int position, UnitType unitType, int unitsAmount, TeamID teamID);
    }
}