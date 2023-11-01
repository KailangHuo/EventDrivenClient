using System;
using System.Globalization;
using System.Windows.Data;

namespace EventDrivenStruct.ViewModels.Converter; 

public class ReverseBoolConverter : IValueConverter{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        if (value is bool && (bool)value) {
            return false;
        }

        return true;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }
}