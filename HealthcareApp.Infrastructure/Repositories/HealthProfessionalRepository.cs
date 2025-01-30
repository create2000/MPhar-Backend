// HealthcareApp.Infrastructure/Repositories/HealthProfessionalRepository.cs

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthcareApp.Domain.Entities;
using HealthcareApp.Domain.Interfaces;
using HealthcareApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HealthcareApp.Infrastructure.Repositories {

public class HealthProfessionalRepository : IHealthProfessionalRepository
{
    private readonly AppDbContext _context;

    public HealthProfessionalRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<HealthProfessional>> GetAllAsync()
    {
        return await _context.HealthProfessionals.ToListAsync();
    }

    public async Task<HealthProfessional> GetByIdAsync(int id)
    {
        return await _context.HealthProfessionals
            .FirstOrDefaultAsync(h => h.Id == id);
    }
}

}