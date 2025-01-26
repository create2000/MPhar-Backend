using Microsoft.AspNetCore.Identity;

namespace HealthcareApp.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        // You can add custom properties such as Role here
        public string Role { get; set; }
    }
}
