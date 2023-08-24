using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.UI.Services.UiUtilitiesFactory
{
    public interface IUiUtilitiesFactory
    {
        UniTask WarmUp();
        UniTask<Canvas> CreateCanvas();
        UniTask<EventSystem> CreateEventSystem();
    }
}