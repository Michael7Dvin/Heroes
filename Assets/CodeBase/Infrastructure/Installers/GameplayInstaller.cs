using CodeBase.Gameplay.Services.Map.MapGenerator;
using CodeBase.Gameplay.Services.Map.MapService;
using CodeBase.Gameplay.Services.Map.MapVisualizer;
using CodeBase.Gameplay.Services.Map.TileInteractor;
using CodeBase.Gameplay.Services.Map.TileSelector;
using CodeBase.Gameplay.Services.RandomService;
using CodeBase.Gameplay.Services.TeamWinObserver;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Gameplay.Services.UnitsSpawner;
using Zenject;

namespace CodeBase.Infrastructure.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindGameplayServices();
            BindMapServices();
        }
        
        private void BindGameplayServices()
        {
            Container.Bind<IUnitsSpawner>().To<UnitsSpawner>().AsSingle();
            Container.Bind<IRandomService>().To<RandomService>().AsSingle();
            Container.Bind<ITurnQueue>().To<TurnQueue>().AsSingle();
            Container.Bind<IWinService>().To<WinService>().AsSingle();
        }

        private void BindMapServices()
        {
            Container.Bind<IMapGenerator>().To<MapGenerator>().AsSingle();
            Container.Bind<IMapService>().To<MapService>().AsSingle();
            Container.Bind<IMapVisualizer>().To<MapVisualizer>().AsSingle();
            
            Container.Bind<ITileSelector>().To<TileSelector>().AsSingle();
            Container.Bind<ITileInteractor>().To<TileInteractor>().AsSingle();
        }
    }
}