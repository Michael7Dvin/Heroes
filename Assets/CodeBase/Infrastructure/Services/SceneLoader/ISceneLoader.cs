using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.Services.SceneLoader
{
    public interface ISceneLoader
    {
        void Initialize();
        
        UniTask Load(SceneID id);
    }
}