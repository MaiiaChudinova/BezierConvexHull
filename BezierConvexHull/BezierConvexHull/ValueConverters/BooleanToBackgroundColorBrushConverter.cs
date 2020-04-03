using System;
using System.Globalization;
using System.Windows.Media;

namespace BezierConvexHull
{
    /// <summary>
    /// A converter that takes in a boolean and returns a SolidColorBrush
    /// </summary>
    public class BooleanToBackgroundColorBrushConverter : BaseValueConverter<BooleanToBackgroundColorBrushConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // if (IsHullPoint) ...
            if ((bool)value) return new SolidColorBrush(Colors.Red);
            else return new SolidColorBrush(Colors.Black);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}