using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthcareApp.Domain.Entities;
using HealthcareApp.Domain.Interfaces;
using HealthcareApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HealthcareApp.Infrastructure.Repositories
{
    public class HealthProfessionalRepository : IHealthProfessionalRepository
    {
        private readonly AppDbContext _context;

        public HealthProfessionalRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HealthProfessional>> GetAllAsync()
        {
            return await _context.HealthProfessionals
                .AsNoTracking()  // Improves performance for read-only queries
                .Select(hp => new HealthProfessional
                {
                    Id = hp.Id,
                    Name = hp.Name,
                    Specialty = hp.Specialty,
                    ContactInfo = hp.ContactInfo,
                    Role = hp.Role
                })
                .ToListAsync();
        }

        public async Task<HealthProfessional> GetByIdAsync(int id)
        {
            return await _context.HealthProfessionals
                .AsNoTracking()
                .Where(hp => hp.Id == id)
                .Select(hp => new HealthProfessional
                {
                    Id = hp.Id,
                    Name = hp.Name,
                    Specialty = hp.Specialty,
                    ContactInfo = hp.ContactInfo,
                    Role = hp.Role
                })
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(HealthProfessional healthProfessional)
        {
            await _context.HealthProfessionals.AddAsync(healthProfessional);
            await _context.SaveChangesAsync();
        }
    }
}
