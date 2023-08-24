using CodeBase.Common.Observable;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.UI.Services.UiUtilitiesProvider
{
    public class UiUtilitiesProvider : IUiUtilitiesProvider
    {
        private readonly Observable<Canvas> _canvas = new();
        private readonly Observable<EventSystem> _eventSystem = new();

        public IReadOnlyObservable<Canvas> Canvas => _canvas;
        public IReadOnlyObservable<EventSystem> EventSystem => _eventSystem;
        
        public void ResetCanvas(Canvas canvas) => 
            _canvas.Value = canvas;

        public void ResetEventSystem(EventSystem eventSystem) => 
            _eventSystem.Value = eventSystem;
    }
}