using ArcadeAppNick.Models.APIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ArcadeAppNick.Services
{
    internal class WeatherApiService
    {
        private readonly HttpClient _httpClient;

        public WeatherApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(Constants.API_BASE_URL); 
        }

        public async Task<WeatherApiResponse> GetWeatherInformation(string city)
        {
            return await _httpClient.GetFromJsonAsync<WeatherApiResponse>
                (
                $"current?access_key={Constants.API_KEY}&query={city}"
                );
        }
    }
}
