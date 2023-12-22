using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Zadanie3.Converter
{
    public class StateOfFieldConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "#fff";
            }
            
            var stateOfField=(StateOfField)value;
            
            switch (stateOfField)
            {
                case StateOfField.Miss:
                    return "#f00";
                case StateOfField.Hit:
                    return "#00f";
                case StateOfField.Occupied:
                    return "#000";
                case StateOfField.Empty:
                    return "#fff";
                default:
                    return "#fff";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
