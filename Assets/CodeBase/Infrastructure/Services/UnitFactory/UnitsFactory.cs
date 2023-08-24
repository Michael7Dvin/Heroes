using CodeBase.Gameplay;
using CodeBase.Gameplay.Units;
using CodeBase.Gameplay.Units.Implementations.Archers;
using CodeBase.Gameplay.Units.Implementations.Knights;
using CodeBase.Gameplay.Units.Implementations.SkeletonNecromancers;
using CodeBase.Gameplay.Units.Implementations.Skeletons;
using CodeBase.Gameplay.Units.Implementations.Zombies;
using CodeBase.Infrastructure.Services.Instantiator;
using CodeBase.Infrastructure.Services.Logging;
using CodeBase.Infrastructure.Services.ResourcesLoading;
using CodeBase.Infrastructure.Services.ResourcesLoading.AssetAddresses;
using CodeBase.Infrastructure.Services.StaticDataProviding;
using CodeBase.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.Services.UnitFactory
{
    public class UnitsFactory : IUnitFactory
    {
        private readonly IAddressablesLoader _addressablesLoader;
        private readonly IObjectsInstantiator _instantiator;
        private readonly ICustomLogger _logger;
        
        private readonly AllUnitsConfigs _configs;
        private readonly AllAssetsAddresses _allAssetsAddresses;

        public UnitsFactory(IAddressablesLoader addressablesLoader,
            IObjectsInstantiator instantiator,
            ICustomLogger logger,
            IStaticDataProvider staticDataProvider)
        {
            _addressablesLoader = addressablesLoader;
            _instantiator = instantiator;
            _logger = logger;
            
            _allAssetsAddresses = staticDataProvider.AssetsAddresses;
            _configs = staticDataProvider.AllUnitsConfigs;
        }

        public async UniTask WarmUp()
        {
            await _addressablesLoader.LoadGameObject(_allAssetsAddresses.EmptyGameObject);
            
            await _addressablesLoader.LoadGameObject(_allAssetsAddresses.UnitsAddresses.Knight);
            await _addressablesLoader.LoadGameObject(_allAssetsAddresses.UnitsAddresses.Archer);
            await _addressablesLoader.LoadGameObject(_allAssetsAddresses.UnitsAddresses.Zombie);
            await _addressablesLoader.LoadGameObject(_allAssetsAddresses.UnitsAddresses.SkeletonNecromancer);
            await _addressablesLoader.LoadGameObject(_allAssetsAddresses.UnitsAddresses.Skeleton);
        }

        public async UniTask<Unit> Create(Vector3 position, UnitType unitType, int unitsCount, TeamID teamID)
        {
            switch (unitType)
            {
                case UnitType.Knight:
                    return await CreateKnight(position, unitsCount, teamID);
                case UnitType.Archer:
                    return await CreateArcher(position, unitsCount, teamID);
                case UnitType.SkeletonNecromancer:
                    return await CreateSkeletonNecromancer(position, unitsCount, teamID);
                case UnitType.Skeleton:
                    return await CreateSkeleton(position, unitsCount, teamID);
                case UnitType.Zombie:
                    return await CreateZombie(position, unitsCount, teamID); 
                default:
                    _logger.LogError($"Unsupported {nameof(UnitType)}: '{unitType}'");
                    return null;
            }
        }

        private async UniTask<Knight> CreateKnight(Vector3 position, int unitsCount, TeamID teamID)
        {
            Knight knight = new(unitsCount, teamID, _configs.Knight.Initiative);
            GameObject gameObject = await CreateView(position, knight, _allAssetsAddresses.UnitsAddresses.Knight);
            knight.Construct(gameObject);
            return knight;
        }

        private async UniTask<Archer> CreateArcher(Vector3 position, int unitsCount, TeamID teamID)
        {
            Archer archer = new(unitsCount, teamID, _configs.Archer.Initiative);
            GameObject gameObject = await CreateView(position, archer, _allAssetsAddresses.UnitsAddresses.Archer);
            archer.Construct(gameObject);
            return archer;
        }

        private async UniTask<Zombie> CreateZombie(Vector3 position, int unitsCount, TeamID teamID)
        {
            Zombie zombie = new(unitsCount, teamID, _configs.Zombie.Initiative);
            GameObject gameObject = await CreateView(position, zombie, _allAssetsAddresses.UnitsAddresses.Zombie);
            zombie.Construct(gameObject);
            return zombie;
        }

        private async UniTask<SkeletonNecromancer> CreateSkeletonNecromancer(Vector3 position, int unitsCount, TeamID teamID)
        {
            SkeletonNecromancer skeletonNecromancer = new(unitsCount, teamID, _configs.SkeletonNecromancer.Initiative);
            GameObject gameObject = await CreateView(position, skeletonNecromancer, _allAssetsAddresses.UnitsAddresses.SkeletonNecromancer);
            skeletonNecromancer.Construct(gameObject);
            return skeletonNecromancer;
        }

        private async UniTask<Skeleton> CreateSkeleton(Vector3 position, int unitsCount, TeamID teamID)
        {
            Skeleton skeleton = new(unitsCount, teamID, _configs.Skeletons.Initiative);
            GameObject gameObject = await CreateView(position, skeleton, _allAssetsAddresses.UnitsAddresses.Skeleton);
            skeleton.Construct(gameObject);
            return skeleton;
        }

        private async UniTask<GameObject> CreateView(Vector3 position, Unit unit, AssetReferenceGameObject viewAddress)
        {
            GameObject prefab = await _addressablesLoader.LoadGameObject(viewAddress);
            GameObject gameObject = _instantiator.InstantiatePrefab(prefab, position, Quaternion.identity);
            
            UnitView view = gameObject.GetComponentInChildren<UnitView>();
            view.Construct(unit);

            UnitCounter counter = gameObject.GetComponentInChildren<UnitCounter>();
            counter.Construct(unit.Count, unit.TeamID);

            return gameObject;
        }
    }
}