using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.Services.SceneLoading
{
    public interface ISceneLoader
    {
        void Initialize();
        
        UniTask Load(SceneID id);
    }
}