using System;

namespace CodeBase.UI.Windows.Base.Window
{
    public interface IWindow
    {
        WindowID Type { get; }

        event Action Destroyed;

        void Open();
        void Close();
    }
}