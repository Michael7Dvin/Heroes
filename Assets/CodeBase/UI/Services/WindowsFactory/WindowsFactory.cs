using CodeBase.Infrastructure.Services.AddressablesLoader;
using CodeBase.Infrastructure.Services.AddressablesLoader.AssetAddresses;
using CodeBase.Infrastructure.Services.Instantiator;
using CodeBase.Infrastructure.Services.Logging;
using CodeBase.Infrastructure.Services.StaticDataProvider;
using CodeBase.UI.Services.UiUtilitiesProvider;
using CodeBase.UI.Windows;
using CodeBase.UI.Windows.Base.Window;
using CodeBase.UI.Windows.BattleField;
using CodeBase.UI.Windows.ResultsWindow;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.UI.Services.WindowsFactory
{
    public class WindowsFactory : IWindowsFactory
    {
        private readonly IAddressablesLoader _addressablesLoader;
        private readonly IObjectsInstantiator _instantiator;
        private readonly IUiUtilitiesProvider _uiUtilitiesProvider;
        private readonly ICustomLogger _logger;
        
        private readonly UIAssetsAddresses _uiAssetsAddresses;

        public WindowsFactory(IAddressablesLoader addressablesLoader,
            IObjectsInstantiator instantiator,
            IUiUtilitiesProvider uiUtilitiesProvider,
            ICustomLogger logger,
            IStaticDataProvider staticDataProvider)
        {
            _addressablesLoader = addressablesLoader;
            _instantiator = instantiator;
            _uiUtilitiesProvider = uiUtilitiesProvider;
            _logger = logger;

            _uiAssetsAddresses = staticDataProvider.AssetsAddresses.UI;
        }

        private Transform Canvas =>
            _uiUtilitiesProvider.Canvas.Value.transform;

        public async UniTask WarmUp()
        {
            await _addressablesLoader.LoadComponent<BattleFieldWindowView>(_uiAssetsAddresses.BattleFieldWindowView);
        }

        public async UniTask<IWindow> Create(WindowID id)
        {
            switch (id)
            {
                case WindowID.BattleField:
                    return await CreateBattleFieldWindow();
                case WindowID.HumansWinResults:
                    return await CreateHumansWinResultsWindow();
                case WindowID.UndeadsWinResults:
                    return await CreateUndeadsWinResultsWindow();
                default:
                    _logger.LogError($"Unsupported {nameof(WindowID)}: '{id}'");
                    return null;
            }
        }

        private async UniTask<IWindow> CreateBattleFieldWindow()
        {
            BattleFieldWindowView viewPrefab =
                await _addressablesLoader.LoadComponent<BattleFieldWindowView>(_uiAssetsAddresses.BattleFieldWindowView);
            
            BattleFieldWindowView view = _instantiator.InstantiatePrefabForComponent<BattleFieldWindowView>(viewPrefab, Canvas);
            BattleFieldWindowLogic logic = _instantiator.Instantiate<BattleFieldWindowLogic>();
            BattleFieldWindow window = new(WindowID.BattleField, view, logic);

            window.Open();
            
            return window;
        }
        
        private async UniTask<IWindow> CreateHumansWinResultsWindow()
        {
            ResultsWindowView viewPrefab =
                await _addressablesLoader.LoadComponent<ResultsWindowView>(_uiAssetsAddresses.HumansWinResultsWindowView);
            
            ResultsWindowView view = _instantiator.InstantiatePrefabForComponent<ResultsWindowView>(viewPrefab, Canvas);
            ResultsWindowLogic logic = _instantiator.Instantiate<ResultsWindowLogic>();
            ResultsWindow window = new(WindowID.HumansWinResults, view, logic);

            window.Open();
            
            return window;
        }
        
        private async UniTask<IWindow> CreateUndeadsWinResultsWindow()
        {
            ResultsWindowView viewPrefab =
                await _addressablesLoader.LoadComponent<ResultsWindowView>(_uiAssetsAddresses.UndeadsWinResultsWindowView);
            
            ResultsWindowView view = _instantiator.InstantiatePrefabForComponent<ResultsWindowView>(viewPrefab, Canvas);
            ResultsWindowLogic logic = _instantiator.Instantiate<ResultsWindowLogic>();
            ResultsWindow window = new(WindowID.UndeadsWinResults, view, logic);

            window.Open();
            
            return window;
        }
    }
}