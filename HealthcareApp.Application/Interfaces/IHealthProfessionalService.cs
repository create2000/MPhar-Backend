using HealthcareApp.Application.DTOs;
namespace HealthcareApp.Application.Interfaces 
{

public interface IHealthProfessionalService
{
    Task<IEnumerable<HealthProfessionalDto>> GetAllHealthProfessionalsAsync();
    Task<HealthProfessionalDto> GetHealthProfessionalByIdAsync(int id);
}

}