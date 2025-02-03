using Microsoft.AspNetCore.Identity;
using System.Collections.Generic; // Import for ICollection

namespace HealthcareApp.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Email { get; set; }

        public ICollection<Illness> Illnesses { get; set; } = new List<Illness>(); // Navigation property
    }
}