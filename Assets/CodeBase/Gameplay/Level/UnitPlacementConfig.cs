using System;
using CodeBase.Gameplay.Units;
using CodeBase.Gameplay.Units.Parts.Team;
using UnityEngine;

namespace CodeBase.Gameplay.Level
{
    [Serializable]
    public class UnitPlacementConfig
    {
        [field: SerializeField] public Vector2Int Position { get; private set; }
        [field: SerializeField] public UnitType UnitType { get; private set; }
        [field: SerializeField] public int Amount { get; private set; }
        [field: SerializeField] public TeamID TeamID { get; private set; }
    }
}