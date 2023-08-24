using System;
using CodeBase.UI.Controls;
using CodeBase.UI.Windows.Base.WindowView;
using UnityEngine;

namespace CodeBase.UI.Windows.ResultsWindow
{
    public class ResultsWindowView : BaseWindowView
    {
        [SerializeField] private BaseButton _restartButton;
        
        public event Action RestartButtonClicked;
        
        protected override void SubscribeControls() => 
            _restartButton.Cliked += OnRestartButtonClick;

        protected override void UnsubscribeControls() => 
            _restartButton.Cliked -= OnRestartButtonClick;

        private void OnRestartButtonClick() => 
            RestartButtonClicked?.Invoke();
    }
}