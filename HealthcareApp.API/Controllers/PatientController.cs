using HealthcareApp.Application.Interfaces;
using HealthcareApp.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HealthcareApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly IRecommendationService _recommendationService;

        public PatientController(IPatientService patientService, IRecommendationService recommendationService)
        {
            _patientService = patientService;
            _recommendationService = recommendationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPatients()
        {
            var patients = await _patientService.GetAllPatientsAsync();
            return Ok(patients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientById(int id)
        {
            var patient = await _patientService.GetPatientByIdAsync(id);
            if (patient == null) return NotFound();

            return Ok(patient);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePatient([FromBody] PatientDto patientDto)
        {
            if (patientDto == null) return BadRequest("Invalid patient data.");

            var createdPatient = await _patientService.CreatePatientAsync(patientDto);
            return CreatedAtAction(nameof(GetPatientById), new { id = createdPatient.Id }, createdPatient);
        }

        [HttpGet("recommendations/{illnessId}")]
        public async Task<IActionResult> GetRecommendation(int illnessId)
        {
            var recommendation = await _recommendationService.GetByIllnessIdAsync(illnessId);
            if (recommendation == null)
                return NotFound(new { message = "No recommendation found" });

            return Ok(recommendation);
        }
    }
}
