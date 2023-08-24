using System;
using UnityEngine;

namespace CodeBase.UI.Windows.Base.WindowView
{
    public abstract class BaseWindowView : MonoBehaviour, IWindowView
    {
        public event Action Destroyed;

        private void OnEnable() => 
            SubscribeControls();

        private void OnDisable() => 
            UnsubscribeControls();

        private void OnDestroy() => 
            Destroyed?.Invoke();

        public virtual void Open() => 
            gameObject.SetActive(true);

        public virtual void Close() => 
            gameObject.SetActive(false);

        protected abstract void SubscribeControls();
        protected abstract void UnsubscribeControls();
    }
}