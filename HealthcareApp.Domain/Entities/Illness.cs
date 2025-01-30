using Microsoft.AspNetCore.Identity;

namespace HealthcareApp.Domain.Entities {

public class Illness
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}

}