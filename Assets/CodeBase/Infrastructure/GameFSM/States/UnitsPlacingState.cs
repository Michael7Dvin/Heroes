using CodeBase.Gameplay.Level;
using CodeBase.Gameplay.Services.UnitsSpawner;
using CodeBase.Gameplay.Units;
using CodeBase.Gameplay.Units.Parts.Team;
using CodeBase.Infrastructure.GameFSM.FSM;
using CodeBase.Infrastructure.GameFSM.States.Base;

namespace CodeBase.Infrastructure.GameFSM.States
{
    public class UnitsPlacingState : IStateWithArgument<LevelConfig>
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IUnitsSpawner _spawner;
        
        public UnitsPlacingState(IGameStateMachine gameStateMachine, IUnitsSpawner spawner)
        {
            _gameStateMachine = gameStateMachine;
            _spawner = spawner;
        }

        public async void Enter(LevelConfig levelConfig)
        {
            foreach (UnitPlacementConfig unit in levelConfig.Units)
            {
                UnitType type = unit.UnitType;
                int amount = unit.Amount;
                TeamID teamID = unit.TeamID;

                await _spawner.Spawn(unit.Coordinates, type, amount, teamID);
            }
            
            _gameStateMachine.EnterState<BattleState>();
        }

        public void Exit()
        {
        }
    }
}