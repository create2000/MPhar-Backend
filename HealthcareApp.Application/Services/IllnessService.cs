using HealthcareApp.Application.DTOs;
using HealthcareApp.Application.Interfaces;
using HealthcareApp.Domain.Interfaces;
using HealthcareApp.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

    public async Task<IllnessDto> CreateIllnessAsync(IllnessDto illnessDto)
    {
        var illness = new Illness
        {
            Name = illnessDto.Name,
            Description = illnessDto.Description
        };

        await _illnessRepository.AddAsync(illness);
        return new IllnessDto
        {
            Id = illness.Id,
            Name = illness.Name,
            Description = illness.Description
        };
    }

    // ✅ Move UpdateIllnessAsync OUTSIDE CreateIllnessAsync
    public async Task<IllnessDto> UpdateIllnessAsync(IllnessDto illnessDto)
    {
        var illness = await _illnessRepository.GetByIdAsync(illnessDto.Id);
        if (illness == null)
            return null;

        illness.Name = illnessDto.Name;
        illness.Description = illnessDto.Description;

        await _illnessRepository.UpdateAsync(illness); // Use repository instead of _context
        return new IllnessDto
        {
            Id = illness.Id,
            Name = illness.Name,
            Description = illness.Description
        };
    }
}
