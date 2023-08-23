using CodeBase.Infrastructure.Services.ResourcesLoading;
using CodeBase.Infrastructure.Services.StaticDataProviding;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Tilemaps;
using Zenject;

namespace CodeBase.Gameplay.Services.MapFactory
{
    public class TileMapFactory : ITileMapFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly IAddressablesLoader _addressablesLoader;
        private readonly AssetReferenceGameObject _hexagonTileGridReference;

        public TileMapFactory(IInstantiator instantiator, IAddressablesLoader addressablesLoader, IStaticDataProvider staticDataProvider)
        {
            _instantiator = instantiator;
            _addressablesLoader = addressablesLoader;
            _hexagonTileGridReference = staticDataProvider.AssetsAddresses.HexagonTileGrid;
        }

        public async UniTask<Tilemap> Create()
        {
            GameObject hexagonTileGridPrefab = await _addressablesLoader.LoadGameObject(_hexagonTileGridReference);
            GameObject hexagonTileGrid = _instantiator.InstantiatePrefab(hexagonTileGridPrefab);

            return hexagonTileGrid.GetComponentInChildren<Tilemap>();
        }
    }
}