// HealthcareApp.Domain/Interfaces/IHealthProfessionalRepository.cs

using System.Collections.Generic;
using System.Threading.Tasks;
using HealthcareApp.Domain.Entities;
namespace HealthcareApp.Domain.Interfaces {

public interface IHealthProfessionalRepository
{
    Task<IEnumerable<HealthProfessional>> GetAllAsync();
    Task<HealthProfessional> GetByIdAsync(int id);
}

}