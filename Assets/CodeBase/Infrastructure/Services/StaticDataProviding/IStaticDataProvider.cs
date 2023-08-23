using CodeBase.Infrastructure.Services.ResourcesLoading;
using CodeBase.Infrastructure.Services.SceneLoading;

namespace CodeBase.Infrastructure.Services.StaticDataProviding
{
    public interface IStaticDataProvider
    {
        AllScenesData ScenesData { get; }
        AllAssetsAddresses AssetsAddresses { get; }
    }
}