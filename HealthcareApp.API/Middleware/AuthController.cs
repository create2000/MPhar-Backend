using HealthcareApp.Application.DTOs; // For RegisterDto and LoginDto
using HealthcareApp.Application.Interfaces; // For IAuthService
using HealthcareApp.API.Controllers;
using Microsoft.AspNetCore.Mvc; // For ControllerBase and IActionResult

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var result = await _authService.RegisterAsync(dto);
        return Ok(new { Token = result });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var result = await _authService.LoginAsync(dto);
        return Ok(new { Token = result });
    }
}
