using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.Services.SceneLoading
{
    [CreateAssetMenu(fileName = "Scene Data", menuName = "Scenes Data/Scene Data")]
    public class SceneData : ScriptableObject
    {
        [field: SerializeField] public AssetReference AssetReference { get; private set; }
        [field: SerializeField] public SceneID SceneID { get; private set; }
    }
}