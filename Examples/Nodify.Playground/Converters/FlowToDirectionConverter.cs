using Nodify.Playground.Editor;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Nodify.Playground.Converters;

public class FlowToDirectionConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) 
        => value is ConnectorFlow flow ? 
           flow == ConnectorFlow.Output ? 
           ConnectionDirection.Forward : 
           ConnectionDirection.Backward : 
           value;

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) 
        => value is ConnectionDirection dir ? 
           dir == ConnectionDirection.Forward ? 
           ConnectorFlow.Output : 
           ConnectorFlow.Input : 
           value;
}
