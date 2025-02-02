using Microsoft.AspNetCore.Identity;

namespace HealthcareApp.Domain.Entities {

public class PatientReport
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public string Diagnosis { get; set; }
    public string? ReportDetails { get; set; }
     public DateTime CreatedAt { get; set; } 
}

}