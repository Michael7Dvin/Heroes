using System;

namespace CodeBase.UI.Windows.Base.Window
{
    public interface IWindow
    {
        WindowID ID { get; }

        event Action Destroyed;

        void Open();
        void Close();
    }
}