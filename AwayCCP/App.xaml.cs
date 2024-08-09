using System.ComponentModel.Design;
using Hardcodet.Wpf.TaskbarNotification;
using System.Windows;

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
            containerRegistry.RegisterSingleton<IConfigRepo, FileConfigRepo>();
            containerRegistry.Register<TrayIconViewModel>();
            containerRegistry.Register<MainViewModel>();
        }
    }
}