using CodeBase.Infrastructure.Services.SceneLoading;

namespace CodeBase.Infrastructure.Services.StaticDataProviding
{
    public interface IStaticDataProvider
    {
        AllScenesData ScenesData { get; }
    }
}