using CodeBase.Gameplay.Level;
using CodeBase.Gameplay.Tiles;
using CodeBase.Gameplay.Units.Configs;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.StaticDataProvider
{
    [CreateAssetMenu(fileName = "All Configs", menuName = "Configs/All")]
    public class AllConfigs : ScriptableObject
    {
        [field: SerializeField] public LevelConfig Level { get; private set; }
        [field: SerializeField] public AllUnitsConfigs AllUnits { get; private set; }
        [field: SerializeField] public TeamColorsConfig TeamColors { get; private set; }
        [field: SerializeField] public TileViewColorsConfig TileViewColors { get; private set; }
    }
}