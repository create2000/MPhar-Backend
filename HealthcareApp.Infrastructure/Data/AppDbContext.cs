using HealthcareApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HealthcareApp.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; } = null!;
        public DbSet<Recommendation> Recommendations { get; set; } = null!;
           public DbSet<AppUser> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Add configurations if necessary
        }
    }
}
