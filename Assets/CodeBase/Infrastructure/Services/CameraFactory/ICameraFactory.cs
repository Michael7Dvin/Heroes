using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.CameraFactory
{
    public interface ICameraFactory
    {
        UniTask WarmUp();
        UniTask<Camera> Create();
    }
}