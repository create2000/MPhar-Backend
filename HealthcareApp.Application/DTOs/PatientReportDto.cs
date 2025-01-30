namespace HealthcareApp.Application.DTOs
{
    public class PatientReportDto
    {
        public int Id { get; set; }
        public string Diagnosis { get; set; } 
        public int PatientId { get; set; }
        public string ReportDetails { get; set; }
        public DateTime CreatedAt { get; set; } 
        public DateTime DateCreated { get; set; }
    }
}
