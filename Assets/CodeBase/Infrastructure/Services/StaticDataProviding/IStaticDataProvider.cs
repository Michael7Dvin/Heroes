using CodeBase.Gameplay;
using CodeBase.Gameplay.Level;
using CodeBase.Gameplay.Units;
using CodeBase.Infrastructure.Services.ResourcesLoading.AssetAddresses;
using CodeBase.Infrastructure.Services.SceneLoading;
using CodeBase.UI;

namespace CodeBase.Infrastructure.Services.StaticDataProviding
{
    public interface IStaticDataProvider
    {
        AllScenesData ScenesData { get; }
        AllAssetsAddresses AssetsAddresses { get; }
        TeamColors TeamColors { get; }
        AllUnitsConfigs AllUnitsConfigs { get; }
        LevelConfig LevelConfig { get; }
    }
}