using HealthcareApp.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthcareApp.Application.Interfaces
{
    public interface IIllnessService
    {
        Task<IEnumerable<IllnessDto>> GetAllIllnessesAsync();
        Task<IllnessDto> GetIllnessByIdAsync(string id);
        Task<IllnessDto> CreateIllnessAsync(IllnessDto illnessDto);
        Task<IllnessDto> UpdateIllnessAsync(IllnessDto illnessDto);
        Task<IEnumerable<IllnessDto>> GetAssignedIllnessesAsync(int healthProfessionalId);


    }
}
