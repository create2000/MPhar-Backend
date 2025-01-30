using HealthcareApp.Application.Interfaces;
using HealthcareApp.Application.DTOs;
using HealthcareApp.Infrastructure.Data;
using HealthcareApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthcareApp.Application.Services
{
    public class PatientReportService : IPatientReportService
    {
        private readonly AppDbContext _context; // ✅ Declare _context

        // ✅ Inject the database context in the constructor
        public PatientReportService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PatientReportDto>> GetAllReportsAsync()
        {
            var reports = await _context.PatientReports.ToListAsync();
            
            // ✅ Map entities to DTOs before returning
            return reports.Select(r => new PatientReportDto 
            { 
                Id = r.Id,
                PatientId = r.PatientId,
                Diagnosis = r.Diagnosis,
                CreatedAt = r.CreatedAt
            });
        }

        public async Task<PatientReportDto> GetReportByIdAsync(int id)
        {
            var report = await _context.PatientReports.FindAsync(id);
            if (report == null) return null;

            return new PatientReportDto
            {
                Id = report.Id,
                PatientId = report.PatientId,
                Diagnosis = report.Diagnosis,
                CreatedAt = report.CreatedAt
            };
        }

        public async Task<PatientReportDto> CreateReportAsync(CreatePatientReportDto reportDto)
        {
            var newReport = new PatientReport
            {
                PatientId = reportDto.PatientId,
                Diagnosis = reportDto.Diagnosis,
                CreatedAt = DateTime.UtcNow
            };

            _context.PatientReports.Add(newReport);
            await _context.SaveChangesAsync();

            return new PatientReportDto
            {
                Id = newReport.Id,
                PatientId = newReport.PatientId,
                Diagnosis = newReport.Diagnosis,
                CreatedAt = newReport.CreatedAt
            };
        }

        public async Task<bool> DeleteReportAsync(int id)
        {
            var report = await _context.PatientReports.FindAsync(id);
            if (report == null) return false;

            _context.PatientReports.Remove(report);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
