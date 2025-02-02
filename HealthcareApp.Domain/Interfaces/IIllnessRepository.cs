using System.Collections.Generic;
using System.Threading.Tasks;
using HealthcareApp.Domain.Entities;

namespace HealthcareApp.Domain.Interfaces
{
    public interface IIllnessRepository
    {
        Task<IEnumerable<Illness>> GetAllAsync();
        Task<Illness> GetByIdAsync(int id);
        Task AddAsync(Illness illness);
        Task UpdateAsync(Illness illness);
    }
}
