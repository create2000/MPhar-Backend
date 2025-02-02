using Microsoft.AspNetCore.Identity;

namespace HealthcareApp.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        
        public string Email { get; set; }
        public string Role { get; set; }
        
       
    }
}
