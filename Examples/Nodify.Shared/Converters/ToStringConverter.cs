using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Nodify.Shared.Converters;

public class ToStringConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object parameter, CultureInfo culture) 
        => value switch
            {
                Point p => $"{p.X:0.0}, {p.Y:0.0}",
                Size s => $"{s.Width:0.0}, {s.Height:0.0}",
                double d => d.ToString("0.00"),
                _ => value?.ToString()
            };

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) 
        => throw new NotImplementedException();
}
