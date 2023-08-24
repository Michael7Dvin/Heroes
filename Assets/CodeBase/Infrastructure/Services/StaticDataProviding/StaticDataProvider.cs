using CodeBase.Gameplay;
using CodeBase.Gameplay.Level;
using CodeBase.Gameplay.Units;
using CodeBase.Infrastructure.Services.ResourcesLoading.AssetAddresses;
using CodeBase.Infrastructure.Services.SceneLoading;
using CodeBase.UI;

namespace CodeBase.Infrastructure.Services.StaticDataProviding
{
    public class StaticDataProvider : IStaticDataProvider
    {
        public StaticDataProvider(AllScenesData scenesData,
            AllAssetsAddresses assetsAddresses,
            TeamColors teamColors,
            AllUnitsConfigs allUnitsConfigs,
            LevelConfig levelConfig)
        {
            ScenesData = scenesData;
            AssetsAddresses = assetsAddresses;
            TeamColors = teamColors;
            AllUnitsConfigs = allUnitsConfigs;
            LevelConfig = levelConfig;
        }

        public AllScenesData ScenesData { get; }
        public AllAssetsAddresses AssetsAddresses { get; }
        public TeamColors TeamColors { get; }
        public AllUnitsConfigs AllUnitsConfigs { get; }
        public LevelConfig LevelConfig { get; }
    }
}