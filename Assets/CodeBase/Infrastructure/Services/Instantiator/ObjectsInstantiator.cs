using CodeBase.Infrastructure.Services.CurrentSceneProvider;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Instantiator
{
    public class ObjectsInstantiator : IObjectsInstantiator
    {
        private readonly Zenject.IInstantiator _instantiator;
        private readonly IActiveSceneProvider _activeSceneProvider;

        public ObjectsInstantiator(Zenject.IInstantiator instantiator, IActiveSceneProvider activeSceneProvider)
        {
            _instantiator = instantiator;
            _activeSceneProvider = activeSceneProvider;
        }

        private Transform ActiveSceneRoot => _activeSceneProvider.Root;
        
        public GameObject InstantiatePrefab(Object prefab) => 
            _instantiator.InstantiatePrefab(prefab, ActiveSceneRoot);

        public GameObject InstantiatePrefab(Object prefab, Transform parentTransform) => 
            _instantiator.InstantiatePrefab(prefab, parentTransform);

        public GameObject InstantiatePrefab(Object prefab, Vector3 position, Quaternion rotation) => 
            _instantiator.InstantiatePrefab(prefab, position, rotation, ActiveSceneRoot);

        public GameObject InstantiatePrefab(Object prefab, Vector3 position, Quaternion rotation, Transform parentTransform) => 
            _instantiator.InstantiatePrefab(prefab, position, rotation, parentTransform);

        
        public T InstantiatePrefabForComponent<T>(Object prefab) => 
            _instantiator.InstantiatePrefabForComponent<T>(prefab, ActiveSceneRoot);

        public T InstantiatePrefabForComponent<T>(Object prefab, Transform parentTransform) => 
            _instantiator.InstantiatePrefabForComponent<T>(prefab, parentTransform);

        public T InstantiatePrefabForComponent<T>(Object prefab, Vector3 position, Quaternion rotation) => 
            _instantiator.InstantiatePrefabForComponent<T>(prefab, position, rotation, ActiveSceneRoot);

        public T InstantiatePrefabForComponent<T>(Object prefab, Vector3 position, Quaternion rotation, Transform parentTransform) => 
            _instantiator.InstantiatePrefabForComponent<T>(prefab, position, rotation, parentTransform);
    }
}