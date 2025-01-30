// HealthcareApp.Api/Controllers/IllnessController.cs
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
using HealthcareApp.Application.Services;

namespace HealthcareApp.Api.Controllers {


[ApiController]
[Route("api/[controller]")]
public class IllnessController : ControllerBase
{
    private readonly IIllnessService _illnessService;

    public IllnessController(IIllnessService illnessService)
    {
        _illnessService = illnessService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<IllnessDto>>> GetIllnesses()
    {
        var illnesses = await _illnessService.GetAllIllnessesAsync();
        return Ok(illnesses);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IllnessDto>> GetIllness(int id)
    {
        var illness = await _illnessService.GetIllnessByIdAsync(id);
        if (illness == null)
        {
            return NotFound();
        }
        return Ok(illness);
    }
}

}