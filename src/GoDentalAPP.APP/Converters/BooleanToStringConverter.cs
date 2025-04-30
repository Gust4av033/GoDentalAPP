using System;
using System.Globalization;
using System.Windows.Data;

namespace GoDentalAPP.src.GoDentalAPP.APP.Converters
{
    public class BooleanToStringConverter : IValueConverter
    {
        // Textos personalizables (puedes cambiarlos desde XAML)
        public string TrueText { get; set; } = "Sí";
        public string FalseText { get; set; } = "No";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue ? TrueText : FalseText;
            }
            return "N/A"; // Valor por defecto si no es booleano
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string strValue)
            {
                return strValue.Equals(TrueText, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }
    }
}