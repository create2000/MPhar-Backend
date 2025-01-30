using HealthcareApp.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthcareApp.Application.Interfaces
{
    public interface IIllnessService
    {
        Task<IEnumerable<IllnessDto>> GetAllIllnessesAsync();
        Task<IllnessDto> GetIllnessByIdAsync(int id);
    }
}
