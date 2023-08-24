using CodeBase.Common.Observable;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.UI.Services.UiUtilitiesProvider
{
    public interface IUiUtilitiesProvider
    {
        IReadOnlyObservable<Canvas> Canvas { get; }
        IReadOnlyObservable<EventSystem> EventSystem { get; }

        void ResetCanvas(Canvas canvas);
        void ResetEventSystem(EventSystem eventSystem);
    }
}