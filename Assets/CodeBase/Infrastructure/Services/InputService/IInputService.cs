using System;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.InputService
{
    public interface IInputService
    {
        event Action NormalInteracted;
        Vector3 MouseCursorWorldPosition { get; }

        void Initialize();
        void Enable();
        void Disable();
        void CleanUp();
    }
}