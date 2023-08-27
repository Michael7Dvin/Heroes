using CodeBase.Gameplay.Units;
using CodeBase.Gameplay.Units.Configs;
using CodeBase.Gameplay.Units.Parts.Attacker;
using CodeBase.Gameplay.Units.Parts.Death;
using CodeBase.Gameplay.Units.Parts.Health;
using CodeBase.Gameplay.Units.Parts.Stack;
using CodeBase.Gameplay.Units.Parts.Team;
using CodeBase.Infrastructure.Services.AddressablesLoader;
using CodeBase.Infrastructure.Services.AddressablesLoader.AssetAddresses;
using CodeBase.Infrastructure.Services.Instantiator;
using CodeBase.Infrastructure.Services.Logging;
using CodeBase.Infrastructure.Services.StaticDataProvider;
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
            
            await _addressablesLoader.LoadGameObject(_allAssetsAddresses.Units.Knight);
            await _addressablesLoader.LoadGameObject(_allAssetsAddresses.Units.Archer);
            await _addressablesLoader.LoadGameObject(_allAssetsAddresses.Units.Zombie);
            await _addressablesLoader.LoadGameObject(_allAssetsAddresses.Units.SkeletonNecromancer);
            await _addressablesLoader.LoadGameObject(_allAssetsAddresses.Units.Skeleton);
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

        private async UniTask<Unit> CreateKnight(Vector3 position, int stackAmount, TeamID teamID)
        {
            KnightConfig config = _configs.Knight;
            AssetReferenceGameObject address = _allAssetsAddresses.Units.Knight;
            
            UnitTeam team = new(teamID);
            UnitCoordinates coordinates = new();
            UnitStack stack = new(stackAmount);
            UnitAttacker attacker = new(config.Damage, stack);
            UnitDeath death = new();
            UnitHealth health = new(config.Health, stack, death);

            GameObject gameObject = await CreateView(position, death, stack, team, address);

            Unit unit = new(UnitType.Knight, gameObject, config.Initiative, team, coordinates, stack, attacker, health, death);
            return unit;
        }

        private async UniTask<Unit> CreateArcher(Vector3 position, int stackAmount, TeamID teamID)
        {
            ArcherConfig config = _configs.Archer;
            AssetReferenceGameObject address = _allAssetsAddresses.Units.Archer;
            
            UnitTeam team = new(teamID);
            UnitCoordinates coordinates = new();
            UnitStack stack = new(stackAmount);
            UnitAttacker attacker = new(config.Damage, stack);
            UnitDeath death = new();
            UnitHealth health = new(config.Health, stack, death);

            GameObject gameObject = await CreateView(position, death, stack, team, address);

            Unit unit = new(UnitType.Knight, gameObject, config.Initiative, team, coordinates, stack, attacker, health, death);
            return unit;
        }

        private async UniTask<Unit> CreateZombie(Vector3 position, int stackAmount, TeamID teamID)
        {
            ZombieConfig config = _configs.Zombie;
            AssetReferenceGameObject address = _allAssetsAddresses.Units.Zombie;
            
            UnitTeam team = new(teamID);
            UnitCoordinates coordinates = new();
            UnitStack stack = new(stackAmount);
            UnitAttacker attacker = new(config.Damage, stack);
            UnitDeath death = new();
            UnitHealth health = new(config.Health, stack, death);

            GameObject gameObject = await CreateView(position, death, stack, team, address);

            Unit unit = new(UnitType.Knight, gameObject, config.Initiative, team, coordinates, stack, attacker, health, death);
            return unit;
        }

        private async UniTask<Unit> CreateSkeletonNecromancer(Vector3 position, int stackAmount, TeamID teamID)
        {
            SkeletonNecromancerConfig config = _configs.SkeletonNecromancer;
            AssetReferenceGameObject address = _allAssetsAddresses.Units.SkeletonNecromancer;
            
            UnitTeam team = new(teamID);
            UnitCoordinates coordinates = new();
            UnitStack stack = new(stackAmount);
            UnitAttacker attacker = new(config.Damage, stack);
            UnitDeath death = new();
            UnitHealth health = new(config.Health, stack, death);

            GameObject gameObject = await CreateView(position, death, stack, team, address);

            Unit unit = new(UnitType.Knight, gameObject, config.Initiative, team, coordinates, stack, attacker, health, death);
            return unit;
        }

        private async UniTask<Unit> CreateSkeleton(Vector3 position, int stackAmount, TeamID teamID)
        {
            SkeletonConfig config = _configs.Skeleton;
            AssetReferenceGameObject address = _allAssetsAddresses.Units.Skeleton;
            
            UnitTeam team = new(teamID);
            UnitCoordinates coordinates = new();
            UnitStack stack = new(stackAmount);
            UnitAttacker attacker = new(config.Damage, stack);
            UnitDeath death = new();
            UnitHealth health = new(config.Health, stack, death);

            GameObject gameObject = await CreateView(position, death, stack, team, address);

            Unit unit = new(UnitType.Knight, gameObject, config.Initiative, team, coordinates, stack, attacker, health, death);
            return unit;
        }

        private async UniTask<GameObject> CreateView(Vector3 position,
            IUnitDeath death,
            UnitStack stack,
            UnitTeam team,
            AssetReferenceGameObject address)
        {
            GameObject prefab = await _addressablesLoader.LoadGameObject(address);
            GameObject gameObject = _instantiator.InstantiatePrefab(prefab, position, Quaternion.identity);
            
            UnitView view = gameObject.GetComponentInChildren<UnitView>();
            view.Construct(death);

            UnitCounter counter = gameObject.GetComponentInChildren<UnitCounter>();
            counter.Construct(stack, team);

            return gameObject;
        }
    }
}