using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Nodify.Helpers;

internal class UnscaleTransformConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) 
        => (Transform)((TransformGroup)value).Children[0].Inverse;

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value;
}

internal class UnscaleDoubleConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) 
        => (double)values[0] * (double)values[1];

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) 
        => throw new NotImplementedException();
}
