using System.Collections.Generic;
using System.Threading.Tasks;
using HealthcareApp.Application.DTOs;

namespace HealthcareApp.Application.Interfaces
{
    public interface IRecommendationService
    {
        Task<IEnumerable<RecommendationDto>> GetAllRecommendationsAsync();
        Task<RecommendationDto> GetRecommendationByIdAsync(int id);
        Task<RecommendationDto> CreateRecommendationAsync(CreateRecommendationDto recommendationDto);

        Task<IEnumerable<RecommendationDto>> GetRecommendationsForPatientAsync(int patientId);
        Task<bool> MarkRecommendationAsCompletedAsync(int id);
    }
}
