using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class Concert
    {
        [Key]
        public int Id { get; set; }

        public string TicketMasterId { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string Venue { get; set; }

        public string Weather { get; set; }

        public string City { get; set; } //for weather data API

        public string PostalCode { get; set; } //for event search

        public ICollection<UserConcert> UserConcert { get; set; }
    }
}
