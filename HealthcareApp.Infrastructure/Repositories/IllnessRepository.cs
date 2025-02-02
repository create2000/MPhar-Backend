using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthcareApp.Domain.Entities;
using HealthcareApp.Domain.Interfaces;
using HealthcareApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HealthcareApp.Infrastructure.Repositories
{
    public class IllnessRepository : IIllnessRepository
    {
        private readonly AppDbContext _context;

        public IllnessRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Illness>> GetAllAsync()
        {
            return await _context.Illnesses.ToListAsync();
        }

        public async Task<Illness> GetByIdAsync(int id)
        {
            return await _context.Illnesses.FindAsync(id);
        }

        public async Task AddAsync(Illness illness)
    {
        await _context.Illnesses.AddAsync(illness);  // Add the illness entity to the context
        await _context.SaveChangesAsync();  // Save the changes to the database
    }

     public async Task UpdateAsync(Illness illness)
    {
        _context.Set<Illness>().Update(illness);
        await _context.SaveChangesAsync();
    }

    }
}
