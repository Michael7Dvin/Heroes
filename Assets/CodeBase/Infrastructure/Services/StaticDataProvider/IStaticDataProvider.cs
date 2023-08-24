using CodeBase.Gameplay.Level;
using CodeBase.Gameplay.Teams;
using CodeBase.Gameplay.Units;
using CodeBase.Infrastructure.Services.AddressablesLoader.AssetAddresses;
using CodeBase.Infrastructure.Services.SceneLoader;

namespace CodeBase.Infrastructure.Services.StaticDataProvider
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