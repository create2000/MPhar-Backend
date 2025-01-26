using System.Collections.Generic;
using System.Threading.Tasks;
using HealthcareApp.Application.DTOs;

namespace HealthcareApp.Application.Interfaces
{
    public interface IPatientService
    {
        Task<IEnumerable<PatientDto>> GetAllPatientsAsync();
        Task<PatientDto?> GetPatientByIdAsync(int id);
    }
}
