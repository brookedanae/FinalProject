using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class UserConcerts
    {
        [Key]
        public int Id { get; set; }

        public IdentityUser User { get; set; }

        public int ConcertId { get; set; }

        public Concerts Concert { get; set; }
    }
}
