using HealthcareApp.Application.DTOs;
using HealthcareApp.Domain.Entities;
namespace HealthcareApp.Application.Interfaces 
{

public interface IHealthProfessionalService
{
    Task<IEnumerable<HealthProfessionalDto>> GetAllHealthProfessionalsAsync();
    Task<HealthProfessionalDto> GetHealthProfessionalByIdAsync(int id);
    Task<HealthProfessionalDto> CreateHealthProfessionalAsync(HealthProfessionalDto healthProfessionalDto);
    Task AddAsync(HealthProfessional healthProfessional);

}

}