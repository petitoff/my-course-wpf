using System;
using System.Globalization;
using System.Windows.Data;

namespace Zadanie3.Converter;

public class PlayerWinConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not Player player) return "";
            
        switch (player)
        {
            case Player.Player1:
                return "Wygrał gracz 1";
            case Player.Player2:
                return "Wygrał gracz 2";
            default:
                return "";
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}