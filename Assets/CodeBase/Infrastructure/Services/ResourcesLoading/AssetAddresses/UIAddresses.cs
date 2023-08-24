using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.Services.ResourcesLoading.AssetAddresses
{
    [CreateAssetMenu(fileName = "UI Addresses", menuName = "Assets Addresses/UI")]
    public class UIAddresses : ScriptableObject
    {
        [field: SerializeField] public AssetReferenceGameObject KnightView { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject ArcherView { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject ZombieView { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject SkeletonNecromancerView { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject SkeletonView { get; private set; }
    }
}