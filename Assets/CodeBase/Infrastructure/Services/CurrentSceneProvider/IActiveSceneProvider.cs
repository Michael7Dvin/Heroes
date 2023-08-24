using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.Services.CurrentSceneProvider
{
    public interface IActiveSceneProvider
    {
        Transform Root { get; }
        
        void ResetActiveScene(Scene scene);
    }
}