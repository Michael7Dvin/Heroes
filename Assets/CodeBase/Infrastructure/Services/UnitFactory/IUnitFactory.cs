using CodeBase.Gameplay.Units;
using CodeBase.Gameplay.Units.Logic.Parts.Team;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.UnitFactory
{
    public interface IUnitFactory
    {
        UniTask WarmUp();
        UniTask<Unit> Create(Vector3 position, UnitType unitType, int unitsCount, TeamID teamID);
    }
}