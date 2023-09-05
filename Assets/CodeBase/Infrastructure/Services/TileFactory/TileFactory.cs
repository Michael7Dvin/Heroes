using CodeBase.Gameplay.Tiles;
using CodeBase.Infrastructure.Services.AddressablesLoader;
using CodeBase.Infrastructure.Services.Instantiator;
using CodeBase.Infrastructure.Services.StaticDataProvider;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.Services.TileFactory
{
    public class TileFactory : ITileFactory
    {
        private readonly IObjectsInstantiator _instantiator;
        private readonly IAddressablesLoader _addressablesLoader;
        private readonly AssetReferenceGameObject _tileAddress;
        
        public TileFactory(IObjectsInstantiator instantiator,
            IAddressablesLoader addressablesLoader,
            IStaticDataProvider staticDataProvider)
        {
            _instantiator = instantiator;
            _addressablesLoader = addressablesLoader;
            _tileAddress = staticDataProvider.AssetsAddresses.Tile;
        }

        public async UniTask WarmUp() => 
            await _addressablesLoader.LoadGameObject(_tileAddress);

        public async UniTask<Tile> Create(Vector3 position, Vector2Int coordinates, bool isWalkable)
        {
            Tile prefab = await _addressablesLoader.LoadComponent<Tile>(_tileAddress);
            Tile tile = _instantiator.InstantiatePrefabForComponent<Tile>(prefab, position, Quaternion.identity);

            TileLogic logic = _instantiator.Instantiate<TileLogic>();
            logic.Construct(coordinates, isWalkable);
            
            tile.Construct(logic);

            return tile;
        }
    }
}