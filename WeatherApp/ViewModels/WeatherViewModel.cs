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

        private string windSpeed = string.Empty;
        public string WindSpeed
        {
            get => windSpeed;
            set => SetProperty(ref windSpeed, value);
        }

        private string clouds = string.Empty;
        public string Clouds
        {
            get => clouds;
            set => SetProperty(ref clouds, value);
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
                Temperature = (Math.Round(currentWeather.Main.Temp) - 273.15).ToString() + (char)0xB0 + "C";
                TemperatureMin = currentWeather.Main.Temp_min.ToString();
                TemperatureMax = currentWeather.Main.Temp_max.ToString();
                WindSpeed = currentWeather.Wind.Speed.ToString();
                Clouds = currentWeather.Clouds.All.ToString();
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