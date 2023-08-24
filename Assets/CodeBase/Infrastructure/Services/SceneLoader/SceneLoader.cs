using System.Collections.Generic;
using CodeBase.Infrastructure.Services.CurrentSceneProvider;
using CodeBase.Infrastructure.Services.Logging;
using CodeBase.Infrastructure.Services.StaticDataProvider;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace CodeBase.Infrastructure.Services.SceneLoader
{
    public class SceneLoader : ISceneLoader
    {
        private readonly IActiveSceneProvider _activeSceneProvider;
        private readonly ICustomLogger _logger;
        private readonly AllScenesData _allScenesData;
        
        private Dictionary<SceneID, AssetReference> _scenes;
        
        public SceneLoader(IActiveSceneProvider activeSceneProvider,
            ICustomLogger logger,
            IStaticDataProvider staticDataProvider)
        {
            _activeSceneProvider = activeSceneProvider;
            _logger = logger;
            
            _allScenesData = staticDataProvider.ScenesData;
        }

        public void Initialize()
        {
            SceneData battleField = _allScenesData.BattleField;

            _scenes = new Dictionary<SceneID, AssetReference>
            {
                { battleField.SceneID, battleField.AssetReference },
            };
        }

        public async UniTask Load(SceneID id)
        {
            if (_scenes.ContainsKey(id) == false)
            {
                _logger.LogError($"Unable to load scene. Can't find {nameof(SceneID)}: '{id}' in {nameof(_scenes)}");
                return;
            }

            AssetReference sceneReference = _scenes[id];

            await Load(sceneReference);
            _logger.Log($"Loaded: '{id}' scene");
        }

        private async UniTask Load(AssetReference sceneReference)
        {
            if (sceneReference.RuntimeKeyIsValid() == false)
            {
                _logger.LogError($"Unable to load scene. {nameof(AssetReference)} is null");
                return;
            }
            
            AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(sceneReference);
            await handle.Task;
            _activeSceneProvider.ResetActiveScene(handle.Result.Scene);
        }
    }
}