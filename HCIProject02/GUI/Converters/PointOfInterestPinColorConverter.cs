using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using HCIProject02.Core.Model;

namespace HCIProject02.GUI.Converters
{
    public class PointOfInterestPinColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is PointOfInterest pointOfInterest)
            {
                if (value is Hotel)
                    return new BitmapImage(new Uri("../../Resources/Images/Static/pink-pin.png", UriKind.Relative));
                if (value is Attraction)
                    return new BitmapImage(new Uri("../../Resources/Images/Static/yellow-pin.png", UriKind.Relative));
                if (value is Restaurant )
                    return new BitmapImage(new Uri("../../Resources/Images/Static/blue-pin.png", UriKind.Relative));
            }

            // Return a default pin image if no specific image is found
            return new BitmapImage(new Uri("../../Resources/Images/Static/pink-pin.png"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
