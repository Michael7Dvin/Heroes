using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.Services.AddressablesLoader.AssetAddresses
{
    [CreateAssetMenu(fileName = "All Assets Addresses", menuName = "Assets Addresses/All")]
    public class AllAssetsAddresses : ScriptableObject
    {
        [field: SerializeField] public AssetReferenceGameObject Tile { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject EmptyGameObject { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject Camera { get; private set; }
        
        [field: SerializeField] public UnitsAddresses Units { get; private set; }
        [field: SerializeField] public UIAssetsAddresses UI { get; private set; }
    }
}