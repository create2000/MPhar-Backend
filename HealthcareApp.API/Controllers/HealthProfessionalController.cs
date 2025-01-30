// HealthcareApp.Api/Controllers/HealthProfessionalController.cs
using HealthcareApp.Application.DTOs; // Ensure this is the correct namespace for LoginDto
using HealthcareApp.Application.Interfaces; // For IAuthService
using HealthcareApp.Domain.Entities; // For AppUser
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
public class HealthProfessionalController : ControllerBase
{
    private readonly IHealthProfessionalService _healthProfessionalService;

    public HealthProfessionalController(IHealthProfessionalService healthProfessionalService)
    {
        _healthProfessionalService = healthProfessionalService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<HealthProfessionalDto>>> GetHealthProfessionals()
    {
        var professionals = await _healthProfessionalService.GetAllHealthProfessionalsAsync();
        return Ok(professionals);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<HealthProfessionalDto>> GetHealthProfessional(int id)
    {
        var professional = await _healthProfessionalService.GetHealthProfessionalByIdAsync(id);
        if (professional == null)
        {
            return NotFound();
        }
        return Ok(professional);
    }
}

}