using HealthcareApp.Application.DTOs;
using HealthcareApp.Application.Interfaces;
using HealthcareApp.Domain.Entities;
using HealthcareApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthcareApp.Application.Services
{
    public class IllnessService : IIllnessService
    {
        private readonly AppDbContext _dbContext;

        public IllnessService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<IllnessDto>> GetAllIllnessesAsync()
        {
            return await _dbContext.Illnesses
                .Select(i => MapToDto(i)) // Use MapToDto for consistency
                .ToListAsync();
        }

        public async Task<IllnessDto> GetIllnessByIdAsync(string id)
        {
            var illness = await _dbContext.Illnesses.FindAsync(id);
            return illness == null ? null : MapToDto(illness); // Correct: Only one return statement
        }

        public async Task<IllnessDto> CreateIllnessAsync(IllnessDto illnessDto)
        {
            var illness = new Illness
            {
                 Id = Guid.NewGuid().ToString(),
                Name = illnessDto.Name,
                Description = illnessDto.Description,
                PatientId = illnessDto.PatientId.ToString(), // Convert int to string
                AssignedHealthProfessionalId = illnessDto.AssignedHealthProfessionalId,
                Recommendation = illnessDto.Recommendation,
                RecommendationDate = illnessDto.RecommendationDate
                
            };

            _dbContext.Illnesses.Add(illness);
            await _dbContext.SaveChangesAsync();

            illnessDto.Id = illness.Id;
            return illnessDto;
        }

        public async Task<IllnessDto> UpdateIllnessAsync(IllnessDto illnessDto)
        {
            var illness = await _dbContext.Illnesses.FindAsync(illnessDto.Id);
            if (illness == null) return null;

            illness.Name = illnessDto.Name;
            illness.Description = illnessDto.Description;
            // ... other properties

            _dbContext.Illnesses.Update(illness);
            await _dbContext.SaveChangesAsync();

            return MapToDto(illness);
        }

        public async Task<IEnumerable<IllnessDto>> GetAssignedIllnessesAsync(int healthProfessionalId)
        {
            return await _dbContext.Illnesses
                .Where(i => i.AssignedHealthProfessionalId.HasValue && i.AssignedHealthProfessionalId.Value == healthProfessionalId)
                .Select(i => MapToDto(i))
                .ToListAsync();
        }


        private IllnessDto MapToDto(Illness illness)
        {
            return new IllnessDto
            {
                Id = illness.Id,
                Name = illness.Name,
                Description = illness.Description,
                PatientId = int.TryParse(illness.PatientId, out var pid) ? pid : 0,
                AssignedHealthProfessionalId = illness.AssignedHealthProfessionalId,
                Recommendation = illness.Recommendation,
                RecommendationDate = illness.RecommendationDate
            };
        }
    }
}