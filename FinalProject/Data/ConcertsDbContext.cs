using FinalProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Data
{
    public class ConcertsDbContext : DbContext
    {
        public ConcertsDbContext(DbContextOptions<ConcertsDbContext> options) : base(options)
        {
        }

        public DbSet<Concerts> Concerts { get; set; }

        public DbSet<UserConcerts> UserConcerts { get; set; }
    }
}
