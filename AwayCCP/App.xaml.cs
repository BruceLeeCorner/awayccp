using System.Text.Json;
using System.Text.Json.Serialization;
using Prism.DryIoc;
using Prism.Ioc;
using System.Windows;
using System.Windows.Media;
using Hardcodet.Wpf.TaskbarNotification;
using Prism.Mvvm;

namespace AwayCCP
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public TaskbarIcon TrayIcon { get; private set; }

        public App()
        {
            Color color = Color.FromArgb(0, 0, 0, 255);
            var json = JsonSerializer.Serialize(color, new JsonSerializerOptions()
            {
                Converters = { new ColorJsonConverter() }
            });


        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<Config>();
            containerRegistry.Register<MainViewModel>();
            containerRegistry.Register<TrayIconViewModel>();
        }

        protected override Window CreateShell()
        {
            return null;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            TrayIcon = new TrayIcon();

            Container.Resolve<Config>().Initialize();
            var main = new MainView();
            MainWindow = main;
            main.ShowDialog();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            TrayIcon.Dispose();
            base.OnExit(e);
            Container.Resolve<Config>().Terminate();
        }

        //protected override void ConfigureViewModelLocator()
        //{
        //    base.ConfigureViewModelLocator();
        //    ViewModelLocationProvider.SetDefaultViewModelFactory(viewModelType => Container.Resolve(viewModelType));
        //}
    }
}