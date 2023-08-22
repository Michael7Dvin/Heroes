﻿using CodeBase.Gameplay.Services.GroupFactory;
using CodeBase.Gameplay.Services.GroupsProvider;
using CodeBase.Gameplay.Services.RandomService;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Infrastructure.GameFSM.FSM;
using CodeBase.Infrastructure.GameFSM.States;
using CodeBase.Infrastructure.Services.Logging;
using CodeBase.Infrastructure.Services.SceneLoading;
using CodeBase.Infrastructure.Services.StaticDataProviding;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private AllScenesData _allScenesData;
        
        public override void InstallBindings()
        {
            BindGameStateMachine();
            
            BindStaticDataProvider();
            BindInfrastructureServices();
            
            BindGameplayServices();
        }

        private void BindGameStateMachine()
        {
            Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();

            Container.Bind<InitializationState>().AsSingle();
            Container.Bind<LevelLoadingState>().AsSingle();
            Container.Bind<BattleState>().AsSingle();
            Container.Bind<ResultsState>().AsSingle();
        }

        private void BindStaticDataProvider()
        {
            Container
                .Bind<IStaticDataProvider>()
                .To<StaticDataProvider>()
                .AsSingle()
                .WithArguments(_allScenesData);
        }

        private void BindInfrastructureServices()
        {
            Container.Bind<ICustomLogger>().To<CustomLogger>().AsSingle();
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
        }

        private void BindGameplayServices()
        {
            Container.Bind<IGroupFactory>().To<GroupFactory>().AsSingle();
            Container.Bind<IRandomService>().To<RandomService>().AsSingle();
            Container.Bind<ITurnQueue>().To<TurnQueue>().AsSingle();
            Container.Bind<IGroupsProvider>().To<GroupsProvider>().AsSingle();
        }
    }
}