using CodeBase.Gameplay.Units;
using CodeBase.Gameplay.Units.Logic.Parts.Team;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Gameplay.Services.UnitsSpawner
{
    public interface IUnitsSpawner
    {
        UniTask<Unit> Spawn(Vector2Int coordinates, UnitType unitType, int unitsAmount, TeamID teamID);
    }
}