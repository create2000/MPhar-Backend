using HealthcareApp.Application.DTOs;
using HealthcareApp.Application.Interfaces;
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
        public async Task<IActionResult> SubmitRecommendation(int illnessId, [FromBody] RecommendationDto recommendationDto) // illnessId is now int
        {
            var illness = await _illnessService.GetIllnessByIdAsync(illnessId.ToString()); 

            if (illness == null)
            {
                return NotFound(new { message = "Illness not found" });
            }

            if (recommendationDto.HealthProfessionalId <= 0)
            {
                return BadRequest(new { message = "Invalid HealthProfessionalId. Must be a positive integer." });
            }

            var createRecommendationDto = new CreateRecommendationDto
            {
                IllnessId = illnessId, 
                RecommendationText = recommendationDto.RecommendationText,
                HealthProfessionalId = recommendationDto.HealthProfessionalId
            };

            await _recommendationService.CreateRecommendationAsync(createRecommendationDto);

            return Ok(new { message = "Recommendation submitted successfully" });
        }
    }


    }
