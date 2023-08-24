using CodeBase.Gameplay;
using CodeBase.Gameplay.Units;
using CodeBase.Gameplay.Units.Implementations.Archers;
using CodeBase.Gameplay.Units.Implementations.Knights;
using CodeBase.Gameplay.Units.Implementations.SkeletonNecromancers;
using CodeBase.Gameplay.Units.Implementations.Skeletons;
using CodeBase.Gameplay.Units.Implementations.Zombies;
using CodeBase.Infrastructure.Services.Logging;
using CodeBase.Infrastructure.Services.ResourcesLoading;
using CodeBase.Infrastructure.Services.ResourcesLoading.AssetAddresses;
using CodeBase.Infrastructure.Services.StaticDataProviding;
using CodeBase.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace CodeBase.Infrastructure.Services.UnitFactory
{
    public class UnitsFactory : IUnitFactory
    {
        private readonly IAddressablesLoader _addressablesLoader;
        private readonly IInstantiator _instantiator;
        private readonly ICustomLogger _logger;
        
        private readonly AllUnitsConfigs _configs;
        private readonly UIAddresses _uiAddresses;

        public UnitsFactory(IAddressablesLoader addressablesLoader,
            IInstantiator instantiator,
            ICustomLogger logger,
            IStaticDataProvider staticDataProvider)
        {
            _addressablesLoader = addressablesLoader;
            _instantiator = instantiator;
            _logger = logger;
            
            _uiAddresses = staticDataProvider.AssetsAddresses.UIAddresses;
            _configs = staticDataProvider.AllUnitsConfigs;
        }

        public async UniTask WarmUp()
        {
            await _addressablesLoader.LoadGameObject(_uiAddresses.KnightView);
            await _addressablesLoader.LoadGameObject(_uiAddresses.ArcherView);
            await _addressablesLoader.LoadGameObject(_uiAddresses.ZombieView);
            await _addressablesLoader.LoadGameObject(_uiAddresses.SkeletonNecromancerView);
            await _addressablesLoader.LoadGameObject(_uiAddresses.SkeletonView);
        }

        public async UniTask<Unit> Create(Vector3 position, UnitType unitType, int unitsCount, TeamID teamID)
        {
            switch (unitType)
            {
                case UnitType.Knight:
                    return await CreateKnights(position, unitsCount, teamID);
                case UnitType.Archer:
                    return await CreateArchers(position, unitsCount, teamID);
                case UnitType.SkeletonNecromancer:
                    return await CreateSkeletonNecromancers(position, unitsCount, teamID);
                case UnitType.Skeleton:
                    return await CreateSkeletons(position, unitsCount, teamID);
                case UnitType.Zombie:
                    return await CreateZombies(position, unitsCount, teamID); 
                default:
                    _logger.LogError($"Unsupported {nameof(UnitType)}: '{unitType}'");
                    return null;
            }
        }

        private async UniTask<Knight> CreateKnights(Vector3 position, int unitsCount, TeamID teamID)
        {
            Knight knight = new(UnitType.Knight, unitsCount, teamID, _configs.Knight.Initiative);
            await CreateViews(position, knight, _uiAddresses.KnightView);
            return knight;
        }

        private async UniTask<Archer> CreateArchers(Vector3 position, int unitsCount, TeamID teamID)
        {
            Archer archer = new(UnitType.Archer, unitsCount, teamID, _configs.Archer.Initiative);
            await CreateViews(position, archer, _uiAddresses.ArcherView);
            return archer;
        }

        private async UniTask<Zombie> CreateZombies(Vector3 position, int unitsCount, TeamID teamID)
        {
            Zombie zombie = new(UnitType.Zombie, unitsCount, teamID, _configs.Zombie.Initiative);
            await CreateViews(position, zombie, _uiAddresses.ZombieView);
            return zombie;
        }

        private async UniTask<SkeletonNecromancer> CreateSkeletonNecromancers(Vector3 position, int unitsCount, TeamID teamID)
        {
            SkeletonNecromancer skeletonNecromancer = 
                new(UnitType.SkeletonNecromancer, unitsCount, teamID, _configs.SkeletonNecromancer.Initiative);
            
            await CreateViews(position, skeletonNecromancer, _uiAddresses.SkeletonNecromancerView);
            return skeletonNecromancer;
        }

        private async UniTask<Skeleton> CreateSkeletons(Vector3 position, int unitsCount, TeamID teamID)
        {
            Skeleton skeleton = new(UnitType.Skeleton, unitsCount, teamID, _configs.Skeletons.Initiative);
            await CreateViews(position, skeleton, _uiAddresses.SkeletonView);
            return skeleton;
        }

        private async UniTask CreateViews(Vector3 position, Unit unit, AssetReferenceGameObject viewAddress)
        {
            UnitView prefab = await _addressablesLoader.LoadComponent<UnitView>(viewAddress);
            UnitView view = _instantiator.InstantiatePrefabForComponent<UnitView>(prefab, position, Quaternion.identity, null);
            view.Construct(unit);

            UnitCounter counter = view.GetComponentInChildren<UnitCounter>();
            counter.Construct(unit.Count, unit.TeamID);
        }
    }
}