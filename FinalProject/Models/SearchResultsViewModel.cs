using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class SearchResultsViewModel
    {
        public string TicketMasterId { get; set; }

        public string Name { get; set; }

        [Display(Name = "Photo")]
        public string Url { get; set; }

        //public DateTime DateTime { get; set; }

        public string Date { get; set; }
        public string Time { get; set; }

        public string Venue { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Temperature { get; set; }

        public string Forecast { get; set; }


        //public Concerts Itinerary { get; set; }
    }
}
