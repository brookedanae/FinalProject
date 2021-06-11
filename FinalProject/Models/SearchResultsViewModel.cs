using Microsoft.AspNetCore.Identity;
using System;

namespace FinalProject.Models
{
    public class SearchResultsViewModel
    {
        public string TicketMasterId { get; set; }

        public string Name { get; set; }

        public DateTime DateTime { get; set; }

        public string Venue { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Weather { get; set; }

        //public Concerts Itinerary { get; set; }
    }
}
