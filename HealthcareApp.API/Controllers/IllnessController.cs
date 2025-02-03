using HealthcareApp.Application.DTOs;
using HealthcareApp.Application.Interfaces;
using HealthcareApp.Domain.Entities;
using HealthcareApp.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class IllnessController : ControllerBase
{
    private readonly IIllnessService _illnessService;
    private readonly AppDbContext _dbContext;
    private readonly IHealthProfessionalService _healthProfessionalService;

    public IllnessController(IIllnessService illnessService, AppDbContext dbContext, IHealthProfessionalService healthProfessionalService)
    {
        _illnessService = illnessService;
        _dbContext = dbContext;
        _healthProfessionalService = healthProfessionalService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<IEnumerable<IllnessDto>>> GetIllnesses()
    {
        var illnesses = await _illnessService.GetAllIllnessesAsync();
        return Ok(illnesses);
    }

    [HttpGet("assigned/{healthProfessionalId}")]
    [Authorize(Roles = "HealthProfessional")]
    public async Task<ActionResult<IEnumerable<IllnessDto>>> GetAssignedIllnesses(int healthProfessionalId)
    {
        var illnesses = await _illnessService.GetAssignedIllnessesAsync(healthProfessionalId);
        return Ok(illnesses);
    }

    [HttpPost]
    public async Task<ActionResult<IllnessDto>> CreateIllness([FromBody] IllnessDto illnessDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdIllness = await _illnessService.CreateIllnessAsync(illnessDto);

        return CreatedAtAction(nameof(GetIllness), new { id = createdIllness.Id }, createdIllness);
    }

    [HttpPost("assign/{illnessId}/{healthProfessionalId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AssignIllness(string illnessId, int healthProfessionalId) // illnessId is a string
    {
        var illness = await _illnessService.GetIllnessByIdAsync(illnessId); // Use string ID
        if (illness == null)
        {
            return NotFound(new { message = "Illness not found" });
        }

        var healthProfessional = await _healthProfessionalService.GetHealthProfessionalByIdAsync(healthProfessionalId);
        if (healthProfessional == null)
        {
            return NotFound(new { message = "Health professional not found" });
        }

        illness.AssignedHealthProfessionalId = healthProfessionalId;
        await _illnessService.UpdateIllnessAsync(illness);

        return Ok(new { message = "Illness assigned successfully" });
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,HealthProfessional,User")]
    public async Task<IActionResult> GetIllness(string id) 
    {
        var illness = await _illnessService.GetIllnessByIdAsync(id); 
        if (illness == null)
        {
            return NotFound();
        }

        return Ok(illness);
    }
}