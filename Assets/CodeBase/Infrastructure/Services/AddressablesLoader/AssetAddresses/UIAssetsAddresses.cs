using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.Services.AddressablesLoader.AssetAddresses
{
    [CreateAssetMenu(fileName = "UI", menuName = "Assets Addresses/UI")]
    public class UIAssetsAddresses : ScriptableObject
    {
        [field: SerializeField] public AssetReferenceGameObject Canvas { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject EventSystem { get; private set; }
        
        [field: SerializeField] public AssetReferenceGameObject BattleFieldWindowView { get; private set; }
    }
}