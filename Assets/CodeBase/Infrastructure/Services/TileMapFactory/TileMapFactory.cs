using CodeBase.Infrastructure.Services.Instantiator;
using CodeBase.Infrastructure.Services.ResourcesLoading;
using CodeBase.Infrastructure.Services.StaticDataProviding;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Tilemaps;

namespace CodeBase.Infrastructure.Services.TileMapFactory
{
    public class TileMapFactory : ITileMapFactory
    {
        private readonly IObjectsInstantiator _instantiator;
        private readonly IAddressablesLoader _addressablesLoader;
        
        private readonly AssetReferenceGameObject _tileGridReference;

        public TileMapFactory(IObjectsInstantiator instantiator,
            IAddressablesLoader addressablesLoader,
            IStaticDataProvider staticDataProvider)
        {
            _instantiator = instantiator;
            _addressablesLoader = addressablesLoader;

            _tileGridReference = staticDataProvider.AssetsAddresses.HexagonTileGrid;
        }

        public async UniTask WarmUp() => 
            await _addressablesLoader.LoadGameObject(_tileGridReference);

        public async UniTask<Tilemap> Create()
        {
            GameObject hexagonTileGridPrefab = await _addressablesLoader.LoadGameObject(_tileGridReference);
            GameObject hexagonTileGrid = _instantiator.InstantiatePrefab(hexagonTileGridPrefab);

            return hexagonTileGrid.GetComponentInChildren<Tilemap>();
        }
    }
}