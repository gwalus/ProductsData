using System;
using System.Globalization;
using System.Windows.Data;

namespace ShoesApp.Converters
{
    public class StringToCurrencyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var cultureInfo = CultureInfo.GetCultureInfo("pl-PL");
                return string.Format(cultureInfo, "{0:C}", value);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
