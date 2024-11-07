using System.ComponentModel.Design;
using Hardcodet.Wpf.TaskbarNotification;
using System.Windows;
using Prism.Ioc;
using AwayCCP.ViewModels;
using AwayCCP.Services;
using AwayCCP.Views;

namespace AwayCCP
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public TaskbarIcon TrayIcon { get; private set; } = null!;
        

        protected override Window CreateShell()
        {
            var main = new MainView();
            MainWindow = main;
            return main;
        }

        protected override void OnExit(ExitEventArgs e)
        {
            TrayIcon?.Dispose();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            TrayIcon = new TrayIcon();
            Container.Resolve<IConfigRepo>().LoadAsync();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            RegisterViewModel(containerRegistry);
            RegisterService(containerRegistry);
            containerRegistry.RegisterDialog<DefaultDialogView>("dialog");
        }

       private void RegisterViewModel(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<TrayIconViewModel>();
            containerRegistry.Register<MainViewModel>();
        }

        private void RegisterService(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IConfigRepo, FileConfigRepo>();
            containerRegistry.RegisterSingleton<ISentenceManager, SentenceManager>();
        }

    }
}