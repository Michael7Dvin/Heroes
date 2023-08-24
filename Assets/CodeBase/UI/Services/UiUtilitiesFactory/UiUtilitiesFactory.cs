using CodeBase.Infrastructure.Services.AddressablesLoader;
using CodeBase.Infrastructure.Services.AddressablesLoader.AssetAddresses;
using CodeBase.Infrastructure.Services.Instantiator;
using CodeBase.Infrastructure.Services.StaticDataProvider;
using CodeBase.UI.Services.UiUtilitiesProvider;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.UI.Services.UiUtilitiesFactory
{
    public class UiUtilitiesFactory : IUiUtilitiesFactory
    {
        private readonly IObjectsInstantiator _instantiator;
        private readonly IAddressablesLoader _addressablesLoader;
        private readonly IUiUtilitiesProvider _uiUtilitiesProvider;
        
        private readonly UIAssetsAddresses _addresses;

        public UiUtilitiesFactory(IObjectsInstantiator instantiator,
            IAddressablesLoader addressablesLoader,
            IUiUtilitiesProvider uiUtilitiesProvider,
            IStaticDataProvider staticDataProvider)
        {
            _instantiator = instantiator;
            _addressablesLoader = addressablesLoader;
            _uiUtilitiesProvider = uiUtilitiesProvider;

            _addresses = staticDataProvider.AssetsAddresses.UI;
        }
        
        public async UniTask WarmUp()
        {
            await _addressablesLoader.LoadGameObject(_addresses.Canvas);
            await _addressablesLoader.LoadGameObject(_addresses.EventSystem);
        }
        
        public async UniTask<Canvas> CreateCanvas()
        {
            Canvas prefab = await _addressablesLoader.LoadComponent<Canvas>(_addresses.Canvas);
            Canvas canvas = _instantiator.InstantiatePrefabForComponent<Canvas>(prefab);
            
            _uiUtilitiesProvider.ResetCanvas(canvas);
            
            return canvas;
        }
        
        public async UniTask<EventSystem> CreateEventSystem()
        {
            EventSystem prefab = await _addressablesLoader.LoadComponent<EventSystem>(_addresses.EventSystem);
            EventSystem eventSystem = _instantiator.InstantiatePrefabForComponent<EventSystem>(prefab);
            
            _uiUtilitiesProvider.ResetEventSystem(eventSystem);
            
            return eventSystem;
        }
    }
}