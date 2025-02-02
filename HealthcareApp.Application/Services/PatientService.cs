using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthcareApp.Application.DTOs;
using HealthcareApp.Application.Interfaces;
using HealthcareApp.Domain.Entities;
using HealthcareApp.Domain.Interfaces;

namespace HealthcareApp.Application.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<IEnumerable<PatientDto>> GetAllPatientsAsync()
        {
            var patients = await _patientRepository.GetAllPatientsAsync();

            return patients.Select(p => new PatientDto
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Gender = p.Gender,
                DateOfBirth = p.DateOfBirth
            });
        }

        public async Task<PatientDto?> GetPatientByIdAsync(int id)
        {
            var patient = await _patientRepository.GetPatientByIdAsync(id);
            if (patient == null) return null;

            return new PatientDto
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Gender = patient.Gender,
                DateOfBirth = patient.DateOfBirth
            };
        }

        public async Task<PatientDto> CreatePatientAsync(PatientDto patientDto)
        {
            var patient = new Patient
            {
                FirstName = patientDto.FirstName,
                LastName = patientDto.LastName,
                Gender = patientDto.Gender,
                DateOfBirth = patientDto.DateOfBirth
            };

            await _patientRepository.AddPatientAsync(patient);

            return new PatientDto
            {
                Id = patient.Id,
                FirstName = patientDto.FirstName,
                LastName = patientDto.LastName,
                Gender = patientDto.Gender,
                DateOfBirth = patientDto.DateOfBirth
            };
        }

    }
}
