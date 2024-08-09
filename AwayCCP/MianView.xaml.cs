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

        private bool IsLocked=true;
        public MainView()
        {
            InitializeComponent();
        }


        private void TextBox_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (IsLocked)
            {
                DragMove();
            }
        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            IsLocked = !IsLocked;
        }

    }


    public class Color2BrushConverter  : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var value2 = value as Color?;
            if (value2.HasValue == false)
            {
                return DependencyProperty.UnsetValue;
            }
            
            return new SolidColorBrush(value2.Value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class NullableColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var value2 = (Color)value;
            return new Color?(value2);

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var value2 = (Color?)value;
            if (value2.HasValue == false)
            {
                return DependencyProperty.UnsetValue;
            }
            else
            {
                return value2.Value;
            }
        }
    }
}