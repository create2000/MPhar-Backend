using HealthcareApp.Application.DTOs;

namespace HealthcareApp.Application.Interfaces
{
    public interface IPatientReportService
    {
        // Keep only one set of methods for each functionality
        Task<IEnumerable<PatientReportDto>> GetAllReportsAsync();
        Task<PatientReportDto> GetReportByIdAsync(int id);
        Task<PatientReportDto> CreateReportAsync(CreatePatientReportDto reportDto);
        Task<bool> DeleteReportAsync(int id);
    }
}
