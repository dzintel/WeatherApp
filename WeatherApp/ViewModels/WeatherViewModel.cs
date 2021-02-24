using WeatherApp.Models;
using WeatherApp.Services;
using System;
using Xamarin.Forms;

namespace WeatherApp.ViewModels
{
    public class WeatherViewModel : BaseViewModel
    {

        private WeatherService weatherService = new WeatherService();

        private string locationName = string.Empty;
        public string LocationName
        {
            get => locationName;
            set => SetProperty(ref locationName, value);
        }

        private string temperature = string.Empty;
        public string Temperature
        {
            get => temperature;
            set => SetProperty(ref temperature, value);
        }

        private string windSpeed = string.Empty;
        public string WindSpeed
        {
            get => windSpeed;
            set => SetProperty(ref windSpeed, value);
        }

        private string temperatureMin = string.Empty;
        public string TemperatureMin
        {
            get => temperatureMin;
            set => SetProperty(ref temperatureMin, value);
        }

        private string temperatureMax = string.Empty;
        public string TemperatureMax
        {
            get => temperatureMax;
            set => SetProperty(ref temperatureMax, value);
        }

        private string cloudiness = string.Empty;
        public string Cloudiness
        {
            get => cloudiness;
            set => SetProperty(ref cloudiness, value);
        }

        private string visibility = string.Empty;
        public string Visibility
        {
            get => visibility;
            set => SetProperty(ref visibility, value);
        }

        private string humidity = string.Empty;
        public string Humidity
        {
            get => humidity;
            set => SetProperty(ref humidity, value);
        }

        private string pressure = string.Empty;
        public string Pressure
        {
            get => pressure;
            set => SetProperty(ref pressure, value);
        }

        private string sunrise = string.Empty;
        public string Sunrise
        {
            get => sunrise;
            set => SetProperty(ref sunrise, value);
        }

        private string sunset = string.Empty;
        public string Sunset
        {
            get => sunset;
            set => SetProperty(ref sunset, value);
        }

        private bool isMetric = true;
        public bool IsMetric
        {
            get => isMetric;
            set => SetProperty(ref isMetric, value);
        }

        public Command RefreshCommand { get; }

        public Command ToggleTemperatureCommand { get; }

        public WeatherViewModel()
        {
            RefreshCommand = new Command(RefreshAsync);
            ToggleTemperatureCommand = new Command(ToggleTemperatureMeasurement);
            RefreshAsync(); // Temporary, for debugging purposes
        }

        private async void RefreshAsync()
        {
            IsRefreshing = true;
            try
            {
                CurrentWeather currentWeather = new CurrentWeather();
                currentWeather = await weatherService.GetWeather("Edmonton");
                LocationName = currentWeather.Name.ToString();
                Temperature = currentWeather.Main.Temp.ToString();
                WindSpeed = currentWeather.Wind.Speed.ToString();
                TemperatureMin = currentWeather.Main.Temp_min.ToString();
                TemperatureMax = currentWeather.Main.Temp_max.ToString();
                Cloudiness = currentWeather.Clouds.All.ToString();
                Visibility = currentWeather.Visibility.ToString();
                Humidity = currentWeather.Main.Humidity.ToString();
                Pressure = currentWeather.Main.Pressure.ToString();
                Sunrise = currentWeather.Sys.Sunrise.ToString();
                Sunset = currentWeather.Sys.Sunrise.ToString();
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("Error", $"{e.Message}", "Dismiss");
            }
            finally
            {
                IsRefreshing = false;
            }
        }

        private void ToggleTemperatureMeasurement()
        {
            IsMetric ^= true;
        }
    }
}