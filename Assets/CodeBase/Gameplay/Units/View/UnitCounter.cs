using CodeBase.Gameplay.Units.Logic.Parts.Stack;
using CodeBase.Gameplay.Units.Logic.Parts.Team;
using CodeBase.Infrastructure.Services.Logging;
using CodeBase.Infrastructure.Services.StaticDataProvider;
using TMPro;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Units.View
{
    public class UnitCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _textField;
        [SerializeField] private SpriteRenderer _fillRenderer;

        private ICustomLogger _logger;
        private Color _humansFillColor;
        private Color _undeadsFillColor;

        private UnitStack _unitStack;
        private UnitTeam _unitTeam;

        [Inject]
        public void InjectDependencies(ICustomLogger logger, IStaticDataProvider staticDataProvider)
        {
            _logger = logger;
            _humansFillColor = staticDataProvider.Configs.TeamColors.HumansFillColor;
            _undeadsFillColor = staticDataProvider.Configs.TeamColors.UndeadsFillColor;
        }
        
        public void Construct(UnitStack unitStack, UnitTeam unitTeam)
        {
            _unitStack = unitStack;
            _unitTeam = unitTeam;

            UpdateCounter(_unitStack.Amount.Value);
            UpdateFillColor(_unitTeam.Current.Value);
            
            _unitStack.Amount.Changed += UpdateCounter;
            _unitTeam.Current.Changed += UpdateFillColor;
        }

        private void OnDisable()
        {
            _unitStack.Amount.Changed -= UpdateCounter;
            _unitTeam.Current.Changed -= UpdateFillColor;
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