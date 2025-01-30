using HealthcareApp.Application.Interfaces;
using HealthcareApp.Domain.Entities;
using HealthcareApp.Application.DTOs;
using HealthcareApp.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthcareApp.Application.Services
{
    public class RecommendationService : IRecommendationService
    {
        private readonly IRecommendationRepository _repository;

        public RecommendationService(IRecommendationRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<RecommendationDto>> GetAllRecommendationsAsync()
        {
            var recommendations = await _repository.GetAllAsync();
            return recommendations.Select(r => new RecommendationDto
            {
                Id = r.Id,
                PatientId = r.PatientId,
                HealthProfessionalId = r.HealthProfessionalId,
                RecommendationText = r.RecommendationText,
                IsCompleted = r.IsCompleted,
                DateCreated = r.DateCreated
            });
        }

        public async Task<RecommendationDto> GetRecommendationByIdAsync(int id)
        {
            var recommendation = await _repository.GetByIdAsync(id);
            if (recommendation == null) return null;

            return new RecommendationDto
            {
                Id = recommendation.Id,
                PatientId = recommendation.PatientId,
                HealthProfessionalId = recommendation.HealthProfessionalId,
                RecommendationText = recommendation.RecommendationText,
                IsCompleted = recommendation.IsCompleted,
                DateCreated = recommendation.DateCreated
            };
        }

        public async Task<RecommendationDto> CreateRecommendationAsync(CreateRecommendationDto recommendationDto)
        {
            var recommendation = new Recommendation
            {
                PatientId = recommendationDto.PatientId,
                HealthProfessionalId = recommendationDto.HealthProfessionalId,
                RecommendationText = recommendationDto.RecommendationText,
                IsCompleted = false
            };

            var createdRecommendation = await _repository.CreateAsync(recommendation);

            return new RecommendationDto
            {
                Id = createdRecommendation.Id,
                PatientId = createdRecommendation.PatientId,
                HealthProfessionalId = createdRecommendation.HealthProfessionalId,
                RecommendationText = createdRecommendation.RecommendationText,
                IsCompleted = createdRecommendation.IsCompleted,
                DateCreated = createdRecommendation.DateCreated
            };
        }

        public async Task<bool> MarkRecommendationAsCompletedAsync(int recommendationId)
        {
            var recommendation = await _repository.GetByIdAsync(recommendationId);
            if (recommendation == null) return false;

            recommendation.IsCompleted = true;
            await _repository.UpdateAsync(recommendation);
            return true;
        }

        // ✅ Corrected method placement inside the class
        public async Task<IEnumerable<RecommendationDto>> GetRecommendationsForPatientAsync(int patientId)
        {
            var recommendations = await _repository.GetRecommendationsByPatientIdAsync(patientId);

            return recommendations.Select(r => new RecommendationDto
            {
                Id = r.Id,
                PatientId = r.PatientId,
                HealthProfessionalId = r.HealthProfessionalId,
                RecommendationText = r.RecommendationText,
                IsCompleted = r.IsCompleted,
                DateCreated = r.DateCreated
            });
        }
    } // ✅ Closing brace of the class correctly placed
}
