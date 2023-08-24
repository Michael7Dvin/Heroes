using CodeBase.Common.Observable;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.CameraProvider
{
    public class CameraProvider : ICameraProvider
    {
        private readonly Observable<Camera> _camera = new();

        public IReadOnlyObservable<Camera> Camera => _camera;
        
        public void ResetCamera(Camera camera) => 
            _camera.Value = camera;
    }
}