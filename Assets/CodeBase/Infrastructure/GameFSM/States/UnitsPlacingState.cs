using CodeBase.Gameplay;
using CodeBase.Gameplay.Level;
using CodeBase.Gameplay.Services.MapService;
using CodeBase.Gameplay.Services.UnitsSpawner;
using CodeBase.Gameplay.Units;
using CodeBase.Infrastructure.GameFSM.FSM;
using CodeBase.Infrastructure.GameFSM.States.Base;
using UnityEngine;

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
                Vector3Int position = new Vector3Int(unit.Position.x, unit.Position.y, 0);
                UnitType type = unit.UnitType;
                int amount = unit.Amount;
                TeamID teamID = unit.TeamID;

                await _spawner.Spawn(position, type, amount, teamID);
            }
            
            _gameStateMachine.EnterState<BattleState>();
        }

        public void Exit()
        {
        }
    }
}