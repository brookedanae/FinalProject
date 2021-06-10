using FinalProject.Models;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FinalProject.Services
{
    public class TicketmasterService : ITicketmasterService
    {
        private readonly HttpClient _client;

        public TicketmasterService(HttpClient client)
        {
            _client = client;
        }

        public async Task<EventsResponse> GetEventAsync(string postalcode)
        {
            var startTime = DateTime.Now;
            var endTime = startTime.AddDays(30);
            var startTime_str = startTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var endTime_str = endTime.ToString("yyyy-MM-ddTHH:mm:ssZ");

            return await _client.GetFromJsonAsync<EventsResponse>($"/discovery/v2/events.json?postalCode={postalcode}&radius=50&unit=miles&startDateTime={startTime_str}&endDateTime={endTime_str}&apikey=czGJRJzt5nN3zpCP6tmKdUA6VF6SP7cf");
        }
    }
}
