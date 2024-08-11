using System.Globalization;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Prism.Mvvm;
using Path = System.IO.Path;

namespace AwayCCP
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        private bool _isLocked = true;

        public MainView()
        {
            InitializeComponent();
        }

        private void TextBox_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_isLocked)
            {
                DragMove();
            }
        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            _isLocked = !_isLocked;
        }

        private void MainView_OnInitialized(object? sender, EventArgs e)
        {
            (Application.Current as PrismApplication)!.Container.Resolve<IEventAggregator>().GetEvent<ShowTextBoxEvent>()
                .Subscribe(
                    () =>
                    {
                        this.Focus();
                        this.TextBox.Focus();
                        Keyboard.Focus(this.TextBox);
                        this.Show();
                    });
        }
    }

}