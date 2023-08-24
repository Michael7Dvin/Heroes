using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.Services.ResourcesLoading.AssetAddresses
{
    [CreateAssetMenu(fileName = "All Assets Addresses", menuName = "Assets Addresses/All")]
    public class AllAssetsAddresses : ScriptableObject
    {
        [field: SerializeField] public AssetReferenceGameObject HexagonTileGrid { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject EmptyGameObject { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject Camera { get; private set; }
        
        [field: SerializeField] public UnitsAddresses UnitsAddresses { get; private set; }
    }
}