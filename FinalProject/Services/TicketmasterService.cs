using FinalProject.Services.APIModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FinalProject.Services
{
    public class TicketmasterService : ITicketmasterService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        public TicketmasterService(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task<EventsResponse> GetEventAsync(string postalcode)
        {
            var startTime = DateTime.Now;
            var endTime = startTime.AddDays(30);
            var startTime_str = startTime.ToString("yyyy-MM-ddThh:mm:ssZ");
            var endTime_str = endTime.ToString("yyyy-MM-ddThh:mm:ssZ");
            var key = _configuration["keys:Ticketmaster"];

            return await _client.GetFromJsonAsync<EventsResponse>($"/discovery/v2/events.json?postalCode={postalcode}&radius=50&unit=miles&startDateTime={startTime_str}&endDateTime={endTime_str}&apikey={key}");
        }

        public async Task<EventsResponse> GetEventsByKeywordAsync(string keyword)
        {
            var startTime = DateTime.Now;
            var endTime = startTime.AddDays(30);
            var startTime_str = startTime.ToString("yyyy-MM-ddThh:mm:ssZ");
            var endTime_str = endTime.ToString("yyyy-MM-ddThh:mm:ssZ");
            var key = _configuration["keys:Ticketmaster"];

            return await _client.GetFromJsonAsync<EventsResponse>($"/discovery/v2/events.json?keyword={keyword}&radius=50&unit=miles&startDateTime={startTime_str}&endDateTime={endTime_str}&apikey={key}");

        }
    }
}
