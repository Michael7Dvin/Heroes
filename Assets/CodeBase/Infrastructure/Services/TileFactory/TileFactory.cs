﻿using CodeBase.Gameplay.Tiles;
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
            TileView prefab = await _addressablesLoader.LoadComponent<TileView>(_tileAddress);
            TileView view = _instantiator.InstantiatePrefabForComponent<TileView>(prefab, position, Quaternion.identity);
            view.Construct(coordinates);
            
            TileLogic logic = _instantiator.Instantiate<TileLogic>();
            logic.Construct(isWalkable);
            
            return new Tile(logic, view);
        }
    }
}