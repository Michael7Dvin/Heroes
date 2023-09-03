using CodeBase.Gameplay.Units;
using CodeBase.Gameplay.Units.Configs;
using CodeBase.Gameplay.Units.Logic;
using CodeBase.Gameplay.Units.Logic.Parts.Attacker;
using CodeBase.Gameplay.Units.Logic.Parts.Coordinates;
using CodeBase.Gameplay.Units.Logic.Parts.Death;
using CodeBase.Gameplay.Units.Logic.Parts.Health;
using CodeBase.Gameplay.Units.Logic.Parts.Mover;
using CodeBase.Gameplay.Units.Logic.Parts.Stack;
using CodeBase.Gameplay.Units.Logic.Parts.Team;
using CodeBase.Gameplay.Units.View;
using CodeBase.Gameplay.Units.View.Parts.DeathAnimator;
using CodeBase.Gameplay.Units.View.Parts.PathMoveAnimator;
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
            _configs = staticDataProvider.Configs.AllUnits;
        }

        public async UniTask WarmUp() => 
            await _addressablesLoader.LoadGameObject(_allAssetsAddresses.Units.Knight);

        public async UniTask<Unit> Create(Vector3 position, UnitType unitType, int unitsCount, TeamID teamID)
        {
            switch (unitType)
            {
                case UnitType.Knight:
                    return await CreateKnight(position, unitsCount, teamID);
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
            UnitMover mover = new UnitMover(config.MovePoints, config.IsMoveThroughObstacles);
            UnitAttacker attacker = new(config.Damage, stack);
            UnitHealth health = new(config.Health, stack);
            UnitDeath death = new(stack);

            UnitView view = await CreateView(position, stack, team, death, config.AnimationMoveSpeed, address);
            
            UnitLogic logic = new(UnitType.Knight,
                config.Initiative,
                team,
                coordinates,
                stack,
                mover,
                attacker,
                health,
                death);

            Unit unit = new(logic, view);
            
            return unit;
        }

        
        private async UniTask<UnitView> CreateView(Vector3 position,
            UnitStack stack,
            UnitTeam team,
            IUnitDeath unitDeath,
            float animationMoveSpeed,
            AssetReferenceGameObject address)
        {
            GameObject prefab = await _addressablesLoader.LoadGameObject(address);
            GameObject gameObject = _instantiator.InstantiatePrefab(prefab, position, Quaternion.identity);

            DeathAnimator deathAnimator = new(gameObject, unitDeath);
            GroundPathMoveAnimator groundPathMoveAnimator = new(gameObject.transform, animationMoveSpeed);
            UnitView unitView = new(deathAnimator, groundPathMoveAnimator);
            
            UnitCounter counter = gameObject.GetComponentInChildren<UnitCounter>();
            counter.Construct(stack, team);

            return unitView;
        }
    }
}