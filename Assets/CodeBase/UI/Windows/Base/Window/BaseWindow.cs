using System;
using CodeBase.Common.Observable;
using CodeBase.UI.Windows.Base.WindowView;

namespace CodeBase.UI.Windows.Base.Window
{
    public abstract class BaseWindow : IWindow
    {
        private readonly IWindowView _view;

        protected BaseWindow(WindowID id, IWindowView view)
        {
            ID = id;
            _view = view;
            Destroyed += OnDestroy;
        }

        public WindowID ID { get; }

        public event Action Destroyed
        {
            add => _view.Destroyed += value;
            remove => _view.Destroyed -= value;
        }
        
        public void Open()
        {
            _view.Open();
            OnOpened();
        }

        public void Close()
        {
            _view.Close();
            OnClosed();
        }

        protected abstract void OnOpened();
        protected abstract void OnClosed();
        
        private void OnDestroy()
        {
            Destroyed -= OnDestroy;
            OnClosed();
        }
    }
}