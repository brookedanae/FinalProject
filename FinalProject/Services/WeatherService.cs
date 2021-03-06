using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using static FinalProject.Services.APIModels.WeatherModel;

namespace FinalProject.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;


        public WeatherService(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task<WeatherApiResponse> GetWeatherAsync(string city)
        {
            var key = _configuration["keys:Weather"];
            var apiUrl = $"data/2.5/forecast?q={city}&units=imperial&cnt=200&appid={key}";

            return await _client.GetFromJsonAsync<WeatherApiResponse>(apiUrl);
        }

    }
}
