using CodeBase.UI.Windows;
using CodeBase.UI.Windows.Base.Window;
using Cysharp.Threading.Tasks;

namespace CodeBase.UI.Services.WindowsFactory
{
    public interface IWindowsFactory
    {
        UniTask WarmUp();
        UniTask<IWindow> Create(WindowID id);
    }
}