using CodeBase.Common.Observable;

namespace CodeBase.Gameplay.Units.Parts.Team
{
    public class UnitTeam
    {
        private readonly Observable<TeamID> _current = new();

        public UnitTeam(TeamID teamID)
        {
            _current.Value = teamID;
        }

        public IReadOnlyObservable<TeamID> Current => _current;
        
        public void Change(TeamID teamID) => 
            _current.Value = teamID;
    }
}