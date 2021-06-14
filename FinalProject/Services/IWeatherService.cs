using System.Threading.Tasks;
using static FinalProject.Services.APIModels.WeatherModel;

namespace FinalProject.Services
{
    public interface IWeatherService
    {
        Task<WeatherApiResponse> GetWeatherAsync(string city);
    }
}