using CodeBase.Gameplay.Groups;
using CodeBase.Gameplay.Services.GroupFactory;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Infrastructure.GameFSM.FSM;
using CodeBase.Infrastructure.GameFSM.States.Base;
using CodeBase.Infrastructure.Services.SceneLoading;

namespace CodeBase.Infrastructure.GameFSM.States
{
    public class LevelLoadingState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly ITurnQueue _turnQueue;
        private readonly IGroupFactory _groupFactory;

        public LevelLoadingState(IGameStateMachine gameStateMachine, ISceneLoader sceneLoader, ITurnQueue turnQueue, IGroupFactory groupFactory)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _turnQueue = turnQueue;
            _groupFactory = groupFactory;
        }

        public async void Enter()
        {
            await _sceneLoader.Load(SceneID.BattleField);
            
            _turnQueue.Initialize();

            _groupFactory.Create(UnitType.Zombie, 1);
            _groupFactory.Create(UnitType.SkeletonNecromancers, 15);
            _groupFactory.Create(UnitType.Knight, 5);
            _groupFactory.Create(UnitType.Archer, 5);

            _gameStateMachine.EnterState<BattleState>();
        }

        public void Exit()
        {
        }
    }
}