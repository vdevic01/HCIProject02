using System;
using System.Globalization;
using System.Windows.Data;
using Microsoft.Maps.MapControl.WPF;

namespace HCIProject02.GUI.Converters
{
    public class LatitudeLongitudeToLocationConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 2 && values[0] is double latitude && values[1] is double longitude)
            {
                return new Location(latitude, longitude);
            }

            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
