using UnityEngine;

namespace CodeBase.Infrastructure.Services.Instantiator
{
    public interface IObjectsInstantiator
    {
        T Instantiate<T>();
        
        GameObject InstantiatePrefab(Object prefab);
        GameObject InstantiatePrefab(Object prefab, Transform parentTransform);
        GameObject InstantiatePrefab(Object prefab, Vector3 position, Quaternion rotation);
        GameObject InstantiatePrefab(Object prefab, Vector3 position, Quaternion rotation, Transform parentTransform);
        
        T InstantiatePrefabForComponent<T>(Object prefab);
        T InstantiatePrefabForComponent<T>(Object prefab, Transform parentTransform);
        T InstantiatePrefabForComponent<T>(Object prefab, Vector3 position, Quaternion rotation);
        T InstantiatePrefabForComponent<T>(Object prefab, Vector3 position, Quaternion rotation, Transform parentTransform);
    }
}