using System;

namespace CodeBase.UI.Windows.Base.WindowView
{
    public interface IWindowView
    {
        event Action Destroyed;

        void Open();
        void Close();
    }
}