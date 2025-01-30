using System.Collections.Generic;
using System.Threading.Tasks;
using HealthcareApp.Domain.Entities;

namespace HealthcareApp.Domain.Interfaces
{
    public interface IRecommendationRepository
    {
        Task<IEnumerable<Recommendation>> GetAllAsync();  // Missing method
        Task<Recommendation> GetByIdAsync(int id);
        Task<IEnumerable<Recommendation>> GetByPatientIdAsync(int patientId);
        Task<IEnumerable<Recommendation>> GetRecommendationsByPatientIdAsync(int patientId);
        Task<Recommendation> CreateAsync(Recommendation recommendation);  // Missing method

        Task<bool> MarkAsCompletedAsync(int id);  // Missing method
        Task UpdateAsync(Recommendation recommendation);
    }
}
