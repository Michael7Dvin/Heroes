using CodeBase.Gameplay;
using CodeBase.Gameplay.Units;
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