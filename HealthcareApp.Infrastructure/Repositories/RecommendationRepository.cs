using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthcareApp.Domain.Entities;
using HealthcareApp.Domain.Interfaces;
using HealthcareApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HealthcareApp.Infrastructure.Repositories
{
    public class RecommendationRepository : IRecommendationRepository
    {
        private readonly AppDbContext _context;

        public RecommendationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Recommendation>> GetAllAsync()
        {
            return await _context.Recommendations.ToListAsync();
        }

        public async Task<IEnumerable<Recommendation>> GetByPatientIdAsync(int patientId)
        {
            return await _context.Recommendations
                .Where(r => r.PatientId == patientId)
                .ToListAsync();
        }

        public async Task<Recommendation> GetByIdAsync(int id)
        {
            return await _context.Recommendations
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Recommendation> CreateAsync(Recommendation recommendation)
        {
            _context.Recommendations.Add(recommendation);
            await _context.SaveChangesAsync();
            return recommendation;
        }

        public async Task<bool> MarkAsCompletedAsync(int id)
        {
            var recommendation = await _context.Recommendations.FindAsync(id);
            if (recommendation == null)
                return false;

            recommendation.IsCompleted = true; // Assuming your entity has an `IsCompleted` field
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task UpdateAsync(Recommendation recommendation)
        {
            _context.Recommendations.Update(recommendation);
            await _context.SaveChangesAsync();
        }

        // ✅ Corrected placement: Moved this method inside the class
        public async Task<IEnumerable<Recommendation>> GetRecommendationsByPatientIdAsync(int patientId)
        {
            return await _context.Recommendations
                                 .Where(r => r.PatientId == patientId)
                                 .ToListAsync();
        }
    } // ✅ Correct closing brace for the class
} // ✅ Correct closing brace for the namespace
