using CodeBase.Infrastructure.Services.ResourcesLoading;
using CodeBase.Infrastructure.Services.SceneLoading;

namespace CodeBase.Infrastructure.Services.StaticDataProviding
{
    public class StaticDataProvider : IStaticDataProvider
    {
        public StaticDataProvider(AllScenesData scenesData, AllAssetsAddresses assetsAddresses)
        {
            ScenesData = scenesData;
            AssetsAddresses = assetsAddresses;
        }

        public AllScenesData ScenesData { get; }
        public AllAssetsAddresses AssetsAddresses { get; }
    }
}