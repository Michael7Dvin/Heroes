using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.Services.AddressablesLoader.AssetAddresses
{
    [CreateAssetMenu(fileName = "Units", menuName = "Assets Addresses/Units")]
    public class UnitsAddresses : ScriptableObject
    {
        [field: SerializeField] public AssetReferenceGameObject Knight { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject Archer { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject Zombie { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject SkeletonNecromancer { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject Skeleton { get; private set; }
    }
}