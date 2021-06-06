using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class UserConcerts
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public int ConcertId { get; set; }
    }
}
