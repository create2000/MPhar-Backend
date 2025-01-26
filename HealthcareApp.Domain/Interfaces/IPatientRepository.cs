using System.Collections.Generic;
using System.Threading.Tasks;
using HealthcareApp.Domain.Entities;

namespace HealthcareApp.Domain.Interfaces
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetAllPatientsAsync();
        Task<Patient?> GetPatientByIdAsync(int id);
    }
}
