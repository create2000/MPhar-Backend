using System.ComponentModel.DataAnnotations;

namespace HealthcareApp.Application.DTOs
{
    public class CreatePatientReportDto
    {
        [Required]
        public int PatientId { get; set; }

        [Required(ErrorMessage = "Diagnosis is required.")]
        public string Diagnosis { get; set; }

        [Required(ErrorMessage = "ReportDetails is required.")]
        public string ReportDetails { get; set; }
    }
}
