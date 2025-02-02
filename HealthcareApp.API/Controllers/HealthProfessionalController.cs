using HealthcareApp.Application.DTOs; // Ensure this is the correct namespace for DTOs
using HealthcareApp.Application.Interfaces; // For IRecommendationService
using HealthcareApp.Domain.Entities; // For Recommendation
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HealthcareApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthProfessionalController : ControllerBase
    {
        private readonly IHealthProfessionalService _healthProfessionalService;
        private readonly IIllnessService _illnessService;
        private readonly IRecommendationService _recommendationService;

        public HealthProfessionalController(IHealthProfessionalService healthProfessionalService, IIllnessService illnessService, IRecommendationService recommendationService)
        {
            _healthProfessionalService = healthProfessionalService;
            _illnessService = illnessService;
            _recommendationService = recommendationService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllHealthProfessionals()
        {
            var professionals = await _healthProfessionalService.GetAllHealthProfessionalsAsync();
            return Ok(professionals);
        }

        [HttpPost("add")]
    public async Task<IActionResult> AddHealthProfessional([FromBody] HealthProfessionalDto healthProfessionalDto)
        {
            if (healthProfessionalDto == null)
                return BadRequest(new { message = "Invalid data" });

            var createdProfessional = await _healthProfessionalService.CreateHealthProfessionalAsync(healthProfessionalDto);

            if (createdProfessional == null)
                return StatusCode(500, new { message = "Failed to add health professional" });

            return CreatedAtAction(nameof(GetHealthProfessionalById), new { id = createdProfessional.Id }, createdProfessional);
        }

        [HttpGet("{id}")]
    public async Task<IActionResult> GetHealthProfessionalById(int id)
        {
            var professional = await _healthProfessionalService.GetHealthProfessionalByIdAsync(id);
            
            if (professional == null)
                return NotFound(new { message = "Health professional not found" });

            return Ok(professional);
        }


        [HttpPost("recommend/{illnessId}")]
        public async Task<IActionResult> SubmitRecommendation(int illnessId, [FromBody] RecommendationDto recommendationDto)
        {
            var illness = await _illnessService.GetIllnessByIdAsync(illnessId);
            if (illness == null)
                return NotFound(new { message = "Illness not found" });

            // Correct the variable name to avoid conflict
            var recommendation = new CreateRecommendationDto
            {
                IllnessId = illnessId,  // Using illnessId from the route parameter
                RecommendationText = recommendationDto.RecommendationText, // Assuming RecommendationDto contains this
                HealthProfessionalId = recommendationDto.HealthProfessionalId // Assuming RecommendationDto contains this too
            };

            // Assuming this method exists in IRecommendationService
            await _recommendationService.CreateRecommendationAsync(recommendation);

            return Ok(new { message = "Recommendation submitted successfully" });
        }
    }
}
