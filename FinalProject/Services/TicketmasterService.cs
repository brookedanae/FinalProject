using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<TMModel> GetEventAsync(string postalcode)
        {
            DateTime startTime = DateTime.Now;
            DateTime endTime = startTime.AddDays(14);
            string startTime_str = startTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
            string endTime_str = endTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
            Console.WriteLine(endTime_str);

            return await _client.GetFromJsonAsync<TMModel>($"/discovery/v2/events.json?postalCode={postalcode}&radius=50&unit=miles&startDateTime={startTime_str}&endDateTime={endTime_str}&apikey=czGJRJzt5nN3zpCP6tmKdUA6VF6SP7cf");
        }
    }
}
