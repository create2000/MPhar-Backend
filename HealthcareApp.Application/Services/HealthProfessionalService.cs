// HealthcareApp.Application/Services/HealthProfessionalService.cs

using HealthcareApp.Application.Interfaces;
using HealthcareApp.Domain.Entities;
using HealthcareApp.Application.DTOs;
using HealthcareApp.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace HealthcareApp.Application.Services {


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
            ContactInfo = p.ContactInfo
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
            ContactInfo = professional.ContactInfo
        };
    }
}

}