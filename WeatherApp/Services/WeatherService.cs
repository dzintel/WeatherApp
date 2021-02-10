using Newtonsoft.Json;
using WeatherApp.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherApp.Services
{
    public class WeatherService
    {

        public WeatherService()
        {

        }

        public async Task<CurrentWeather> GetWeather(string location)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://api.openweathermap.org/data/2.5/weather?q={location}&appid=a0a9eea28763ee9f46fe4d86c8f50d64"),

            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                string content = await response.Content.ReadAsStringAsync();
                CurrentWeather currentWeather = JsonConvert.DeserializeObject<CurrentWeather>(content);

                return currentWeather;
            }

        }

    }
}
