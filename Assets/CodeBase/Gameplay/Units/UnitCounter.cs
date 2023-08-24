using CodeBase.Common.Observable;
using CodeBase.Gameplay.Teams;
using CodeBase.Infrastructure.Services.Logging;
using CodeBase.Infrastructure.Services.StaticDataProvider;
using TMPro;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Units
{
    public class UnitCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _textField;
        [SerializeField] private SpriteRenderer _fillRenderer;

        private ICustomLogger _logger;
        private Color _humansFillColor;
        private Color _undeadsFillColor;

        private IReadOnlyObservable<int> _unitsCount;
        private IReadOnlyObservable<TeamID> _teamID;

        [Inject]
        public void InjectDependencies(ICustomLogger logger, IStaticDataProvider staticDataProvider)
        {
            _logger = logger;
            _humansFillColor = staticDataProvider.TeamColors.HumansFillColor;
            _undeadsFillColor = staticDataProvider.TeamColors.UndeadsFillColor;
        }
        
        public void Construct(IReadOnlyObservable<int> count, IReadOnlyObservable<TeamID> teamID)
        {
            _unitsCount = count;
            _teamID = teamID;

            UpdateCounter(count.Value);
            UpdateFillColor(teamID.Value);
            
            _unitsCount.Changed += UpdateCounter;
            _teamID.Changed += UpdateFillColor;
        }

        private void OnDisable()
        {
            _unitsCount.Changed -= UpdateCounter;
            _teamID.Changed -= UpdateFillColor;
        }

        private void UpdateCounter(int value) => 
            _textField.text = value.ToString();

        private void UpdateFillColor(TeamID id)
        {
            switch (id)
            {
                case TeamID.Humans:
                    _fillRenderer.color = _humansFillColor;
                    break;
                case TeamID.Undeads:
                    _fillRenderer.color = _undeadsFillColor;
                    break;
                default:
                    _logger.LogError($"Unsupported {nameof(TeamID)}: '{id}'");
                    break;
            }
        }
    }
}