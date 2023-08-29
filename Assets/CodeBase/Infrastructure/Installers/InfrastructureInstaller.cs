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
using CodeBase.Infrastructure.Services.TileFactory;
using CodeBase.Infrastructure.Services.UnitFactory;
using CodeBase.Infrastructure.Services.UnitsProvider;
using CodeBase.Infrastructure.Services.Updater;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Installers
{
    public class InfrastructureInstaller : MonoInstaller
    {
        [SerializeField] private AllScenesData _allScenesData;
        [SerializeField] private AllAssetsAddresses _allAssetsAddresses;
        [SerializeField] private AllConfigs _allConfigs;
        
        public override void InstallBindings()
        {
            BindGameStateMachine();
            BindStaticDataProvider();
            BindInfrastructureServices();
        }

        private void BindGameStateMachine()
        {
            Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();

            Container.Bind<InitializationState>().AsSingle();
            Container.Bind<WarmUppingState>().AsSingle();
            Container.Bind<LevelLoadingState>().AsSingle();
            Container.Bind<UnitsPlacingState>().AsSingle();
            Container.Bind<GameplayState>().AsSingle();
            Container.Bind<RestartState>().AsSingle();
        }

        private void BindStaticDataProvider()
        {
            Container
                .Bind<IStaticDataProvider>()
                .To<StaticDataProvider>()
                .AsSingle()
                .WithArguments(_allScenesData, _allAssetsAddresses, _allConfigs);
        }

        private void BindInfrastructureServices()
        {
            Container.Bind<IObjectsInstantiator>().To<ObjectsInstantiator>().AsSingle();
            Container.Bind<ICustomLogger>().To<CustomLogger>().AsSingle();
            Container.Bind<IAddressablesLoader>().To<AddressablesLoader>().AsSingle();
            Container.Bind<IInputService>().To<InputService>().AsSingle();
            Container.BindInterfacesAndSelfTo<Updater>().AsSingle();
            
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.Bind<IActiveSceneProvider>().To<ActiveSceneProvider>().AsSingle();

            Container.Bind<IUnitFactory>().To<UnitsFactory>().AsSingle();
            Container.Bind<IUnitsProvider>().To<UnitsProvider>().AsSingle();

            Container.Bind<ICameraFactory>().To<CameraFactory>().AsSingle();
            Container.Bind<ICameraProvider>().To<CameraProvider>().AsSingle();
            
            Container.Bind<ITileFactory>().To<TileFactory>().AsSingle();
        }
    }
}