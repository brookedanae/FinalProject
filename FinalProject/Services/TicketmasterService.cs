using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FinalProject.Services
{
    public class TicketmasterService : ITicketmasterService

    {
        private readonly HttpClient _client;

        public TicketmasterService(HttpClient  client)
        {
            _client = client;
        }
    }
}
