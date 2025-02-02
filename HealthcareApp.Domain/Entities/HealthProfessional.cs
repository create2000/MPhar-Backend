using Microsoft.AspNetCore.Identity;

namespace HealthcareApp.Domain.Entities {

public class HealthProfessional
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Specialty { get; set; }  // Dentist, Optometrist, etc.
    public string ContactInfo { get; set; }
    public string Role { get; set; }
}

}