using System;
using CodeBase.Infrastructure.Services.CameraProvider;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CodeBase.Infrastructure.Services.InputService
{
    public class InputService : IInputService
    {
        private readonly ICameraProvider _cameraProvider;
        private PlayerInput _input;

        public InputService(ICameraProvider cameraProvider)
        {
            _cameraProvider = cameraProvider;
        }

        public Vector3 MouseCursorWorldPosition
        {
            get
            {
                Camera camera = _cameraProvider.Camera.Value;
                Vector2 screenPosition = _input.Interaction.CursorPosition.ReadValue<Vector2>();
                return camera.ScreenToWorldPoint(screenPosition);
            }
        }

        public event Action NormalInteracted;

        public void Initialize()
        {
            _input = new PlayerInput();
            Enable();

            _input.Interaction.Normal.performed += OnNormalInteraction;
        }

        public void Enable() => 
            _input.Enable();

        public void Disable() => 
            _input.Disable();

        public void CleanUp() => 
            _input.Interaction.Normal.performed += OnNormalInteraction;

        private void OnNormalInteraction(InputAction.CallbackContext context) => 
            NormalInteracted?.Invoke();
    }
}