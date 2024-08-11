using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace AwayCCP
{
    public class Color2BrushConverter : IValueConverter
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