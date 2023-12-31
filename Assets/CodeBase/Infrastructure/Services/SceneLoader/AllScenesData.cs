﻿using UnityEngine;

namespace CodeBase.Infrastructure.Services.SceneLoader
{
    [CreateAssetMenu(fileName = "All Scenes Data", menuName = "Scenes Data/All")]
    public class AllScenesData : ScriptableObject
    {
        [field: SerializeField] public SceneData BattleField { get; private set; }
    }
}