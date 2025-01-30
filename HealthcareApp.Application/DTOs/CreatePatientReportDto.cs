namespace HealthcareApp.Application.DTOs
{
    public class CreatePatientReportDto
    {
        public int PatientId { get; set; }
        public string ReportDetails { get; set; }
        public string Diagnosis { get; set; }
    }
}
