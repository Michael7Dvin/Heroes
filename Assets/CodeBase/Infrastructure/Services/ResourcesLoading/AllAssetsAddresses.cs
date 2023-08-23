using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.Services.ResourcesLoading
{
    [CreateAssetMenu(fileName = "All Assets Addresses", menuName = "Assets Addresses/All")]
    public class AllAssetsAddresses : ScriptableObject
    {
        [field: SerializeField] public AssetReferenceGameObject HexagonTileGrid { get; private set; }
    }
}