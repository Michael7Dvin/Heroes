using CodeBase.Gameplay.Units.Parts.Team;

namespace CodeBase.Gameplay.Services.TeamWinObserver
{
    public interface IWinService
    {
        void Reset(TeamID leftTeam, TeamID rightTeam);
    }
}