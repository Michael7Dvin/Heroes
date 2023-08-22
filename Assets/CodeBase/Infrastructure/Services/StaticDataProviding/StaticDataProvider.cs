using CodeBase.Infrastructure.Services.SceneLoading;

namespace CodeBase.Infrastructure.Services.StaticDataProviding
{
    public class StaticDataProvider : IStaticDataProvider
    {
        public StaticDataProvider(AllScenesData scenesData)
        {
            ScenesData = scenesData;
        }

        public AllScenesData ScenesData { get; }
    }
}