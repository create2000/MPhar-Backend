using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthcareApp.Application.DTOs;
using HealthcareApp.Application.Interfaces;
using HealthcareApp.Domain.Interfaces;
using HealthcareApp.Domain.Entities;

namespace HealthcareApp.Application.Services
{
    public class IllnessService : IIllnessService
    {
        private readonly IIllnessRepository _illnessRepository;

        public IllnessService(IIllnessRepository illnessRepository)
        {
            _illnessRepository = illnessRepository;
        }

        public async Task<IEnumerable<IllnessDto>> GetAllIllnessesAsync()
        {
            var illnesses = await _illnessRepository.GetAllAsync();
            return illnesses.Select(i => new IllnessDto
            {
                Id = i.Id,
                Name = i.Name,
                Description = i.Description
            }).ToList();
        }

        public async Task<IllnessDto> GetIllnessByIdAsync(int id)
        {
            var illness = await _illnessRepository.GetByIdAsync(id);
            if (illness == null) return null;

            return new IllnessDto
            {
                Id = illness.Id,
                Name = illness.Name,
                Description = illness.Description
            };
        }
    }
}
