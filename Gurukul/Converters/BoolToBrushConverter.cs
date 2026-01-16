using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Gurukul.Converters;

public class BoolToBrushConverter : IValueConverter
{
    public Brush Active { get; set; } = new SolidColorBrush(Color.FromRgb(45, 45, 48));
    public Brush Normal { get; set; } = Brushes.Transparent;

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => (bool)value ? Active : Normal;

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
