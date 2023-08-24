using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.Services.ResourcesLoading.AssetAddresses
{
    [CreateAssetMenu(fileName = "All Assets Addresses", menuName = "Assets Addresses/All")]
    public class AllAssetsAddresses : ScriptableObject
    {
        [field: SerializeField] public AssetReferenceGameObject HexagonTileGrid { get; private set; }
        
        [field: SerializeField] public UIAddresses UIAddresses { get; private set; }
    }
}