using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.Services.CurrentSceneProvider
{
    public class ActiveSceneProvider : IActiveSceneProvider
    {
        private Scene _activeScene;
        
        public Transform Root => _activeScene.GetRootGameObjects()[0].transform;
        
        public void ResetActiveScene(Scene scene) => 
            _activeScene = scene;
    }
}