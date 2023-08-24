using CodeBase.Common.Observable;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.CameraProvider
{
    public interface ICameraProvider
    {
        IReadOnlyObservable<Camera> Camera { get; }
        
        void ResetCamera(Camera camera);
    }
}