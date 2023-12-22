using System;
using System.Globalization;
using System.Windows.Data;

namespace Zadanie3.Converter;

public class IsPlayerReadyTextConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var isPlayerReady = (IsReady?)value;

        return isPlayerReady == IsReady.Ready ? "Gotowy" : "Nie gotowy";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}