using System;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Services.Updater
{
    public class Updater : IUpdater, ITickable, IFixedTickable, ILateTickable
    {
        private bool _isUpdating;
        
        public event Action<float> Updated;
        public event Action<float> FixedUpdated;
        public event Action<float> LateUpdated;
        
        public event Action<float> NotPausingUpdated;

        public void StartUpdating() => 
            _isUpdating = true;

        public void StopUpdating() => 
            _isUpdating = false;

        public void Tick()
        {
            if (_isUpdating == true)
                Updated?.Invoke(Time.deltaTime);
            
            NotPausingUpdated?.Invoke(Time.deltaTime);
        }

        public void FixedTick()
        {
            if (_isUpdating == true)
                FixedUpdated?.Invoke(Time.fixedDeltaTime);
        }

        public void LateTick()
        {
            if (_isUpdating == true)
                LateUpdated?.Invoke(Time.deltaTime);
        }
    }
}