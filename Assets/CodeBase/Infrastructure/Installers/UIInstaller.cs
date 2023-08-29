using CodeBase.UI.Services.UiUtilitiesFactory;
using CodeBase.UI.Services.UiUtilitiesProvider;
using CodeBase.UI.Services.WindowsFactory;
using Zenject;

namespace CodeBase.Infrastructure.Installers
{
    public class UIInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IUiUtilitiesFactory>().To<UiUtilitiesFactory>().AsSingle();
            Container.Bind<IUiUtilitiesProvider>().To<UiUtilitiesProvider>().AsSingle();

            Container.Bind<IWindowsFactory>().To<WindowsFactory>().AsSingle();
        }
    }
}