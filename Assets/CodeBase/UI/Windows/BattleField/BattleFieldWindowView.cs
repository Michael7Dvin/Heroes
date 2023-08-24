using System;
using CodeBase.UI.Controls;
using CodeBase.UI.Windows.Base.WindowView;
using UnityEngine;

namespace CodeBase.UI.Windows.BattleField
{
    public class BattleFieldWindowView : BaseWindowView
    {
        [SerializeField] private BaseButton _endTurnButton;
        
        public event Action EndTurnButtonClicked;
        
        protected override void SubscribeControls() => 
            _endTurnButton.Cliked += OnEndTurnButtonClick;

        protected override void UnsubscribeControls() => 
            _endTurnButton.Cliked -= OnEndTurnButtonClick;

        private void OnEndTurnButtonClick() => 
            EndTurnButtonClicked?.Invoke();
    }
}