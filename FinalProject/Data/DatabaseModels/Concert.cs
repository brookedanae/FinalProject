using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Data.DatabaseModels
{
    public class Concert
    {
        [Key]
        public int Id { get; set; }

        public string TicketMasterId { get; set; }

        public string Name { get; set; }

        //public DateTime Date { get; set; }

        public string Date { get; set; }
        public string Time { get; set; }

        public string Venue { get; set; }

        public string Temperature { get; set; }

        public string Forecast { get; set; }

        public string City { get; set; } //for weather data API

        public string PostalCode { get; set; } //for event search

        public ICollection<UserConcert> UserConcert { get; set; }
    }
}
