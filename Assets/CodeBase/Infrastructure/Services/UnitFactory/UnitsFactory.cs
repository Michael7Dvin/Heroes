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
            int initiative = _configs.Knight.Initiative;
            int health = _configs.Knight.Health;
            int damage = _configs.Knight.Damage;
            
            Knight knight = new(unitsCount, teamID, initiative, health, damage);
            
            AssetReferenceGameObject view = _allAssetsAddresses.UnitsAddresses.Knight;
            GameObject gameObject = await CreateView(position, knight, view);
            
            knight.Construct(gameObject);
            return knight;
        }

        private async UniTask<Archer> CreateArcher(Vector3 position, int unitsCount, TeamID teamID)
        {
            int initiative = _configs.Archer.Initiative;
            int health = _configs.Archer.Health;
            int damage = _configs.Archer.Damage;
            
            Archer archer = new(unitsCount, teamID, initiative, health, damage);
            
            AssetReferenceGameObject viewAddress = _allAssetsAddresses.UnitsAddresses.Archer;
            GameObject gameObject = await CreateView(position, archer, viewAddress);
            
            archer.Construct(gameObject);
            return archer;
        }

        private async UniTask<Zombie> CreateZombie(Vector3 position, int unitsCount, TeamID teamID)
        {
            int initiative = _configs.Zombie.Initiative;
            int health = _configs.Zombie.Health;
            int damage = _configs.Zombie.Damage;
            
            Zombie zombie = new(unitsCount, teamID, initiative, health, damage);
            
            AssetReferenceGameObject viewAddress = _allAssetsAddresses.UnitsAddresses.Zombie;
            GameObject gameObject = await CreateView(position, zombie, viewAddress);
            
            zombie.Construct(gameObject);
            return zombie;
        }

        private async UniTask<SkeletonNecromancer> CreateSkeletonNecromancer(Vector3 position, int unitsCount, TeamID teamID)
        {
            int initiative = _configs.SkeletonNecromancer.Initiative;
            int health = _configs.SkeletonNecromancer.Health;
            int damage = _configs.SkeletonNecromancer.Damage;
            
            SkeletonNecromancer skeletonNecromancer = new(unitsCount, teamID, initiative, health, damage);
            
            AssetReferenceGameObject viewAddress = _allAssetsAddresses.UnitsAddresses.SkeletonNecromancer;
            GameObject gameObject = await CreateView(position, skeletonNecromancer, viewAddress);
            
            skeletonNecromancer.Construct(gameObject);
            return skeletonNecromancer;
        }

        private async UniTask<Skeleton> CreateSkeleton(Vector3 position, int unitsCount, TeamID teamID)
        {
            int initiative = _configs.Skeleton.Initiative;
            int health = _configs.Skeleton.Health;
            int damage = _configs.Skeleton.Damage;
            
            Skeleton skeleton = new(unitsCount, teamID, initiative, health, damage);
            
            AssetReferenceGameObject viewAddress = _allAssetsAddresses.UnitsAddresses.Skeleton;
            GameObject gameObject = await CreateView(position, skeleton, viewAddress);
            
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
            counter.Construct(unit.Amount, unit.TeamID);

            return gameObject;
        }
    }
}