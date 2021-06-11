using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class UserConcert
    {
        [Key]
        public int Id { get; set; }

        public IdentityUser User { get; set; }

        public int ConcertId { get; set; }

        public Concert Concert { get; set; }
    }
}
