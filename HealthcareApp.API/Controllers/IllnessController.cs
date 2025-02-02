// HealthcareApp.Api/Controllers/IllnessController.cs
using HealthcareApp.Application.DTOs; // Ensure this is the correct namespace for LoginDto
using HealthcareApp.Application.Interfaces; // For IAuthService
using HealthcareApp.Domain.Entities; // For AppUser
using HealthcareApp.Application.Services;
using HealthcareApp.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;  


namespace HealthcareApp.Api.Controllers {


[ApiController]
[Route("api/[controller]")]
public class IllnessController : ControllerBase
{
    private readonly IIllnessService _illnessService;
    private readonly AppDbContext _dbContext;
    private readonly IHealthProfessionalService _healthProfessionalService;

    public IllnessController(IIllnessService illnessService, AppDbContext dbContext, IHealthProfessionalService healthProfessionalService)
{
    _illnessService = illnessService;
    _dbContext = dbContext; // Inject DbContext
    _healthProfessionalService = healthProfessionalService; 
}

    [HttpGet]
    public async Task<ActionResult<IEnumerable<IllnessDto>>> GetIllnesses()
    {
        var illnesses = await _illnessService.GetAllIllnessesAsync();
        return Ok(illnesses);
    }

   [HttpGet("assigned/{healthProfessionalId}")]
    public async Task<ActionResult<IEnumerable<IllnessDto>>> GetAssignedIllnesses(int healthProfessionalId)
    {
        var illnesses = await _illnessService.GetAllIllnessesAsync();
        var assignedIllnesses = illnesses.Where(i => i.AssignedHealthProfessionalId == healthProfessionalId);
        
        return Ok(assignedIllnesses);
    }


    [HttpPost]
    public async Task<ActionResult<IllnessDto>> CreateIllness([FromBody] IllnessDto illnessDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdIllness = await _illnessService.CreateIllnessAsync(illnessDto);
        
        var recommendation = await GenerateRecommendation(illnessDto);

        // Return illness and recommendation in the response
        return CreatedAtAction(nameof(GetIllness), new { id = createdIllness.Id }, new
        {
            createdIllness,
            recommendation.RecommendationText,
            recommendation.HealthProfessionalName
        });
    }

   private async Task<RecommendationResponseDto> GenerateRecommendation(IllnessDto illnessDto)
    {
        // Assuming illnessDto.Id should be used to match the illnessId
        var recommendation = await _dbContext.Recommendations
            .Where(r => r.IllnessId == illnessDto.Id)  // Use the Id field of IllnessDto for the match
            .FirstOrDefaultAsync();

        if (recommendation != null)
        {
            var healthProfessionalName = await _dbContext.HealthProfessionals
                .Where(hp => hp.Id == recommendation.HealthProfessionalId)
                .Select(hp => hp.Name)
                .FirstOrDefaultAsync();

            return new RecommendationResponseDto
            {
                RecommendationText = recommendation.RecommendationText,
                HealthProfessionalName = healthProfessionalName
            };
        }

        return new RecommendationResponseDto
        {
            RecommendationText = "Please consult a healthcare provider for further advice.",
            HealthProfessionalName = "Unknown"
        };
    }



    [HttpPost("assign/{illnessId}/{healthProfessionalId}")]
    public async Task<IActionResult> AssignIllness(int illnessId, int healthProfessionalId)
    {
        var illness = await _illnessService.GetIllnessByIdAsync(illnessId);
        if (illness == null)
            return NotFound(new { message = "Illness not found" });

        var healthProfessional = await _healthProfessionalService.GetHealthProfessionalByIdAsync(healthProfessionalId); // Use correct method
        if (healthProfessional == null)
            return NotFound(new { message = "Health professional not found" });

        // Assign the health professional
        illness.AssignedHealthProfessionalId = healthProfessionalId;
        await _illnessService.UpdateIllnessAsync(illness);

        return Ok(new { message = "Illness assigned successfully" });
    }


    [HttpGet("{id}")] // Explicitly bind the method to GET with the illness ID in the route
    public async Task<IActionResult> GetIllness(int id)
    {
        var illness = await _illnessService.GetIllnessByIdAsync(id);
        if (illness == null) return NotFound();

        return Ok(illness);
    }





}

}