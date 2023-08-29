using CodeBase.Infrastructure.Services.AddressablesLoader.AssetAddresses;
using CodeBase.Infrastructure.Services.SceneLoader;

namespace CodeBase.Infrastructure.Services.StaticDataProvider
{
    public interface IStaticDataProvider
    {
        AllScenesData ScenesData { get; }
        AllAssetsAddresses AssetsAddresses { get; }
        AllConfigs Configs { get; }
    }
}