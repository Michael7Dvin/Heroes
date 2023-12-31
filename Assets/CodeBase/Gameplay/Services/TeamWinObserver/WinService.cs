﻿using CodeBase.Gameplay.Units;
using CodeBase.Gameplay.Units.Logic.Parts.Team;
using CodeBase.Infrastructure.Services.Logging;
using CodeBase.Infrastructure.Services.UnitsProvider;
using CodeBase.UI.Services.WindowsFactory;
using CodeBase.UI.Windows;

namespace CodeBase.Gameplay.Services.TeamWinObserver
{
    public class WinService : IWinService
    {
        private readonly IUnitsProvider _unitsProvider;
        private readonly ICustomLogger _logger;
        private readonly IWindowsFactory _windowsFactory;

        public WinService(IUnitsProvider unitsProvider, ICustomLogger logger, IWindowsFactory windowsFactory)
        {
            _unitsProvider = unitsProvider;
            _logger = logger;
            _windowsFactory = windowsFactory;
        }

        private TeamID _leftTeam;
        private TeamID _rightTeam;

        private int _leftTeamUnits;
        private int _rightTeamUnits;

        public void Initialize()
        {
            _unitsProvider.Spawned += OnUnitSpawned;
            _unitsProvider.Died += OnUnitDied;
        }
        
        public void Reset(TeamID leftTeam, TeamID rightTeam)
        {
            _leftTeam = leftTeam;
            _rightTeam = rightTeam;
        }

        private void OnUnitSpawned(Unit unit)
        {
            TeamID unitTeamID = unit.Logic.Team.Current.Value;
            
            if (unitTeamID == _leftTeam)
            {
                _leftTeamUnits++;
                return;
            }
            
            if (unitTeamID == _rightTeam)
            {
                _rightTeamUnits++;
                return;
            }

            _logger.LogError($"Unable to process spawned unit. Unexpected {nameof(TeamID)}: {unitTeamID}");
        }

        private void OnUnitDied(Unit unit)
        {
            TeamID unitTeamID = unit.Logic.Team.Current.Value;
            
            if (unitTeamID == _leftTeam)
            {
                _leftTeamUnits--;
                
                if (_leftTeamUnits == 0) 
                    FinishGame(_rightTeam);
                
                return;
            }
            
            if (unitTeamID == _rightTeam)
            {
                _rightTeamUnits--;
                
                if (_rightTeamUnits == 0) 
                    FinishGame(_leftTeam);
                
                return;
            }

            _logger.LogError($"Unable to process removed unit. Unexpected {nameof(TeamID)}: {unitTeamID}");
        }

        private void FinishGame(TeamID winnerTeamID)
        {
            switch (winnerTeamID)
            {
                case TeamID.Humans:
                    _windowsFactory.Create(WindowID.HumansWinResults);
                    break;
                case TeamID.Undeads:
                    _windowsFactory.Create(WindowID.UndeadsWinResults);
                    break;
                default:
                    _logger.LogError($"Unsupported {nameof(TeamID)}: '{winnerTeamID}'");
                    return;
            }
            
            CleanUp();
        }

        private void CleanUp()
        {
            _leftTeamUnits = 0;
            _rightTeamUnits = 0;
            
            _unitsProvider.Spawned -= OnUnitSpawned;
            _unitsProvider.Died -= OnUnitDied;
        }
    }
}