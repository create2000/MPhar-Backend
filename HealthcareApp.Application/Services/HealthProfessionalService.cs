using HealthcareApp.Application.Interfaces;
using HealthcareApp.Domain.Entities;
using HealthcareApp.Application.DTOs;
using HealthcareApp.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthcareApp.Application.Services
{
    public class HealthProfessionalService : IHealthProfessionalService
    {
        private readonly IHealthProfessionalRepository _repository;

        public HealthProfessionalService(IHealthProfessionalRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<HealthProfessionalDto>> GetAllHealthProfessionalsAsync()
        {
            var professionals = await _repository.GetAllAsync();
            return professionals.Select(p => new HealthProfessionalDto
            {
                Id = p.Id,
                Name = p.Name,
                Specialty = p.Specialty,
                ContactInfo = p.ContactInfo,
                Role = p.Role // Include the Role property!
            });
        }

        public async Task<HealthProfessionalDto> GetHealthProfessionalByIdAsync(int id)
        {
            var professional = await _repository.GetByIdAsync(id);
            if (professional == null)
            {
                return null;
            }
            return new HealthProfessionalDto
            {
                Id = professional.Id,
                Name = professional.Name,
                Specialty = professional.Specialty,
                ContactInfo = professional.ContactInfo,
                Role = professional.Role // Include the Role property!
            };
        }

        public async Task<HealthProfessionalDto> CreateHealthProfessionalAsync(HealthProfessionalDto healthProfessionalDto)
        {
            var professional = new HealthProfessional
            {
                Name = healthProfessionalDto.Name,
                Specialty = healthProfessionalDto.Specialty,
                ContactInfo = healthProfessionalDto.ContactInfo,
                Role = healthProfessionalDto.Role // Make sure Role is set when creating
            };

            await _repository.AddAsync(professional);

            return new HealthProfessionalDto
            {
                Id = professional.Id,
                Name = professional.Name,
                Specialty = professional.Specialty,
                ContactInfo = professional.ContactInfo,
                Role = professional.Role // Include Role here as well
            };
        }

        public async Task AddAsync(HealthProfessional healthProfessional)
        {
            await _repository.AddAsync(healthProfessional);
        }
    }
}