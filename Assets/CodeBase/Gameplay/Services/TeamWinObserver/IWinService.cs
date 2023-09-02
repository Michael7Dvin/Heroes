using CodeBase.Gameplay.Units.Logic.Parts.Team;

namespace CodeBase.Gameplay.Services.TeamWinObserver
{
    public interface IWinService
    {
        void Initialize();
        void Reset(TeamID leftTeam, TeamID rightTeam);
    }
}