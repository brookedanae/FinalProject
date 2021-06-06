using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Concerts
    {
        [Key]
        public int Id { get; set; }

        public string ConcertName { get; set; }

        public DateTime Date { get; set; }

        public string Venue { get; set; }

        public string Weather { get; set; }

        public ICollection<UserConcerts> Concert { get; set; }
    }
}
