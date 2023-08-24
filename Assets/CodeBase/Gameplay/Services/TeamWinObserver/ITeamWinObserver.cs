using CodeBase.Gameplay.Units.Parts.Team;

namespace CodeBase.Gameplay.Services.TeamWinObserver
{
    public interface ITeamWinObserver
    {
        void Reset(TeamID leftTeam, TeamID rightTeam);
    }
}