using CodeBase.Infrastructure.Services.AddressablesLoader;
using CodeBase.Infrastructure.Services.CameraProvider;
using CodeBase.Infrastructure.Services.Instantiator;
using CodeBase.Infrastructure.Services.StaticDataProvider;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.Services.CameraFactory
{
    public class CameraFactory : ICameraFactory
    {
        private readonly IObjectsInstantiator _instantiator;
        private readonly IAddressablesLoader _addressablesLoader;
        private readonly ICameraProvider _cameraProvider;
        private readonly AssetReferenceGameObject _address;

        public CameraFactory(IObjectsInstantiator instantiator,
            IAddressablesLoader addressablesLoader,
            ICameraProvider cameraProvider,
            IStaticDataProvider staticDataProvider)
        {
            _instantiator = instantiator;
            _addressablesLoader = addressablesLoader;
            _cameraProvider = cameraProvider;
            _address = staticDataProvider.AssetsAddresses.Camera;
        }

        public async UniTask WarmUp() => 
            await _addressablesLoader.LoadGameObject(_address);

        public async UniTask<Camera> Create()
        {
            Camera prefab = await _addressablesLoader.LoadComponent<Camera>(_address);
            Camera camera = _instantiator.InstantiatePrefabForComponent<Camera>(prefab);
            _cameraProvider.ResetCamera(camera);
            return camera;
        }
    }
}