// HealthcareApp.Api/Controllers/RecommendationController.cs
using HealthcareApp.Application.DTOs; 
using HealthcareApp.Application.Interfaces; 
using HealthcareApp.Domain.Entities; 
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareApp.Api.Controllers {

[ApiController]
[Route("api/[controller]")]
public class RecommendationController : ControllerBase
{
    private readonly IRecommendationService _recommendationService;

    public RecommendationController(IRecommendationService recommendationService)
    {
        _recommendationService = recommendationService;
    }

    [HttpGet("patient/{patientId}")]
    public async Task<ActionResult<IEnumerable<RecommendationDto>>> GetRecommendations(int patientId)
    {
        var recommendations = await _recommendationService.GetRecommendationsForPatientAsync(patientId);
        return Ok(recommendations);
    }

    [HttpPut("{recommendationId}/complete")]
    public async Task<ActionResult<RecommendationDto>> MarkAsCompleted(int recommendationId)
    {
        var recommendation = await _recommendationService.MarkRecommendationAsCompletedAsync(recommendationId);
        if (recommendation == null)
        {
            return NotFound();
        }
        return Ok(recommendation);
    }
}

}