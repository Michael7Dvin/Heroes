using CodeBase.Gameplay.Level;
using CodeBase.Gameplay.Services.TeamWinObserver;
using CodeBase.Gameplay.Services.UnitsSpawner;
using CodeBase.Gameplay.Units;
using CodeBase.Gameplay.Units.Logic.Parts.Team;
using CodeBase.Infrastructure.GameFSM.FSM;
using CodeBase.Infrastructure.GameFSM.States.Base;

namespace CodeBase.Infrastructure.GameFSM.States
{
    public class UnitsPlacingState : IStateWithArgument<LevelConfig>
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IUnitsSpawner _spawner;
        private readonly IWinService _winService;

        public UnitsPlacingState(IGameStateMachine gameStateMachine, IUnitsSpawner spawner, IWinService winService)
        {
            _gameStateMachine = gameStateMachine;
            _spawner = spawner;
            _winService = winService;
        }

        public async void Enter(LevelConfig levelConfig)
        {
            _winService.Reset(TeamID.Humans, TeamID.Undeads);

            foreach (UnitPlacementConfig unit in levelConfig.Units)
            {
                UnitType type = unit.UnitType;
                int amount = unit.Amount;
                TeamID teamID = unit.TeamID;

                await _spawner.Spawn(unit.Coordinates, type, amount, teamID);
            }
            
            _gameStateMachine.EnterState<GameplayState>();
        }

        public void Exit()
        {
        }
    }
}