using CodeBase.Gameplay.Services.Attacker;
using CodeBase.Gameplay.Services.MapGenerator;
using CodeBase.Gameplay.Services.MapService;
using CodeBase.Gameplay.Services.Mover;
using CodeBase.Gameplay.Services.PathFinder;
using CodeBase.Gameplay.Services.RandomService;
using CodeBase.Gameplay.Services.TeamWinObserver;
using CodeBase.Gameplay.Services.TileInteractor;
using CodeBase.Gameplay.Services.TileSelector;
using CodeBase.Gameplay.Services.TilesVisualizer;
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
            Container.Bind<IPathFinder>().To<PathFinder>().AsSingle();
            Container.Bind<IMover>().To<Mover>().AsSingle();
            Container.Bind<IAttacker>().To<Attacker>().AsSingle();
        }

        private void BindMapServices()
        {
            Container.Bind<IMapGenerator>().To<MapGenerator>().AsSingle();
            Container.Bind<IMapService>().To<MapService>().AsSingle();
            
            Container.Bind<ITilesVisualizer>().To<TilesVisualizer>().AsSingle();
            Container.Bind<ITileSelector>().To<TileSelector>().AsSingle();
            Container.Bind<ITileInteractor>().To<TileInteractor>().AsSingle();
        }
    }
}