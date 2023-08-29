using CodeBase.Gameplay.Units.Parts.Team;

namespace CodeBase.Gameplay.Services.TeamWinObserver
{
    public interface IWinService
    {
        void Initialize();
        void Reset(TeamID leftTeam, TeamID rightTeam);
    }
}