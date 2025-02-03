using HealthcareApp.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HealthcareApp.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Patient> Patients { get; set; } = null!;
        public DbSet<Recommendation> Recommendations { get; set; } = null!;
        public DbSet<AppUser> Users { get; set; } // You might not need this if using IdentityDbContext
        public DbSet<Illness> Illnesses { get; set; }
        public DbSet<HealthProfessional> HealthProfessionals { get; set; }
        public DbSet<PatientReport> PatientReports { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Illness>()
                .HasOne(i => i.Patient)
                .WithMany(p => p.Illnesses)
                .HasForeignKey(i => i.PatientId)
                .OnDelete(DeleteBehavior.Restrict); // Or appropriate action

            modelBuilder.Entity<Illness>()
                .HasOne(i => i.AssignedHealthProfessional)
                .WithMany(hp => hp.Illnesses)
                .HasForeignKey(i => i.AssignedHealthProfessionalId)
                .OnDelete(DeleteBehavior.SetNull); // Or appropriate action
        }
    }
}