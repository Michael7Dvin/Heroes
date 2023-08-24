using CodeBase.Gameplay.Level;
using CodeBase.Gameplay.Services.MapInteractor;
using CodeBase.Gameplay.Services.MapService;
using CodeBase.Gameplay.Services.Mover;
using CodeBase.Gameplay.Services.RandomService;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Gameplay.Services.UnitsSpawner;
using CodeBase.Gameplay.Teams;
using CodeBase.Gameplay.Units;
using CodeBase.Infrastructure.GameFSM.FSM;
using CodeBase.Infrastructure.GameFSM.States;
using CodeBase.Infrastructure.Services.AddressablesLoader;
using CodeBase.Infrastructure.Services.AddressablesLoader.AssetAddresses;
using CodeBase.Infrastructure.Services.CameraFactory;
using CodeBase.Infrastructure.Services.CameraProvider;
using CodeBase.Infrastructure.Services.CurrentSceneProvider;
using CodeBase.Infrastructure.Services.InputService;
using CodeBase.Infrastructure.Services.Instantiator;
using CodeBase.Infrastructure.Services.Logging;
using CodeBase.Infrastructure.Services.SceneLoader;
using CodeBase.Infrastructure.Services.StaticDataProvider;
using CodeBase.Infrastructure.Services.TileMapFactory;
using CodeBase.Infrastructure.Services.UnitFactory;
using CodeBase.Infrastructure.Services.UnitsProvider;
using CodeBase.UI.Services.UiUtilitiesFactory;
using CodeBase.UI.Services.UiUtilitiesProvider;
using CodeBase.UI.Services.WindowsFactory;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private AllScenesData _allScenesData;
        [SerializeField] private AllAssetsAddresses _allAssetsAddresses;
        [SerializeField] private TeamColors _teamColors;
        [SerializeField] private AllUnitsConfigs _allUnitsConfigs;
        [SerializeField] private LevelConfig _levelConfig;
        
        public override void InstallBindings()
        {
            BindGameStateMachine();
            
            BindStaticDataProvider();
            BindInfrastructureServices();
            
            BindGameplayServices();

            BindUIServices();
        }

        private void BindGameStateMachine()
        {
            Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();

            Container.Bind<InitializationState>().AsSingle();
            Container.Bind<WarmUppingState>().AsSingle();
            Container.Bind<LevelLoadingState>().AsSingle();
            Container.Bind<UnitsPlacingState>().AsSingle();
            Container.Bind<BattleState>().AsSingle();
            Container.Bind<ResultsState>().AsSingle();
        }

        private void BindStaticDataProvider()
        {
            Container
                .Bind<IStaticDataProvider>()
                .To<StaticDataProvider>()
                .AsSingle()
                .WithArguments(_allScenesData, _allAssetsAddresses, _teamColors, _allUnitsConfigs, _levelConfig);
        }

        private void BindInfrastructureServices()
        {
            Container.Bind<IObjectsInstantiator>().To<ObjectsInstantiator>().AsSingle();
            Container.Bind<ICustomLogger>().To<CustomLogger>().AsSingle();
            Container.Bind<IAddressablesLoader>().To<AddressablesLoader>().AsSingle();
            Container.Bind<IInputService>().To<InputService>().AsSingle();
            
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.Bind<IActiveSceneProvider>().To<ActiveSceneProvider>().AsSingle();

            Container.Bind<IUnitFactory>().To<UnitsFactory>().AsSingle();
            Container.Bind<IUnitsProvider>().To<UnitsProvider>().AsSingle();
            
            Container.Bind<ITileMapFactory>().To<TileMapFactory>().AsSingle();

            Container.Bind<ICameraFactory>().To<CameraFactory>().AsSingle();
            Container.Bind<ICameraProvider>().To<CameraProvider>().AsSingle();
        }

        private void BindGameplayServices()
        {
            Container.Bind<IUnitsSpawner>().To<UnitsSpawner>().AsSingle();
            Container.Bind<IRandomService>().To<RandomService>().AsSingle();
            Container.Bind<ITurnQueue>().To<TurnQueue>().AsSingle();
            Container.Bind<IMapService>().To<MapService>().AsSingle();
            Container.Bind<IMapInteractor>().To<MapInteractor>().AsSingle();
            Container.Bind<IMover>().To<Mover>().AsSingle();
        }

        private void BindUIServices()
        {
            Container.Bind<IUiUtilitiesFactory>().To<UiUtilitiesFactory>().AsSingle();
            Container.Bind<IUiUtilitiesProvider>().To<UiUtilitiesProvider>().AsSingle();

            Container.Bind<IWindowsFactory>().To<WindowsFactory>().AsSingle();
        }
    }
}