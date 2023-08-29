using CodeBase.Infrastructure.Services.AddressablesLoader.AssetAddresses;
using CodeBase.Infrastructure.Services.SceneLoader;

namespace CodeBase.Infrastructure.Services.StaticDataProvider
{
    public class StaticDataProvider : IStaticDataProvider
    {
        public StaticDataProvider(AllScenesData scenesData, AllAssetsAddresses assetsAddresses, AllConfigs configs)
        {
            ScenesData = scenesData;
            AssetsAddresses = assetsAddresses;
            Configs = configs;
        }

        public AllScenesData ScenesData { get; }
        public AllAssetsAddresses AssetsAddresses { get; }
        public AllConfigs Configs { get; }
    }
}