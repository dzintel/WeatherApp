using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace WeatherApp.Converters
{
    class TemperatureConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(string))
                throw new InvalidOperationException("The target must be a string");

            double temperature;
            if (!Double.TryParse(value as string, out temperature))
            {
                return string.Empty;
            }
            string unit = parameter as string;
            string result = string.Empty;

            switch (unit)
            {
                default:
                case "metric":
                    result = $"{Math.Round(temperature - 273.15)} °C";
                    break;
                case "imperial":
                    result = $"{Math.Round((temperature - 273.15) * (9 / 5) + 32)} °F";
                    break;
                case "kelvin":
                    result = $"{Math.Round(temperature)} K";
                    break;
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
