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

namespace HealthcareApp.API.Controllers
{
    [Route("api/auth")] // Unified route prefix
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthController(
            IAuthService authService,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IConfiguration configuration)
        {
            _authService = authService;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        // POST api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var result = await _authService.RegisterAsync(dto);
            return Ok(new { Token = result });
        }

        // POST api/auth/signup
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpModel model)
        {
            var user = new AppUser
            {
                UserName = model.UserName,
                Email = model.Email,
                Role = model.Role ?? "User" // Optionally assign role from request
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            // Optional: Assign role to user
            await _userManager.AddToRoleAsync(user, model.Role);

            return Ok(new { message = "User created successfully" });
        }

        // POST api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto) // This should reference HealthcareApp.Application.DTOs.LoginDto
        {
            var result = await _authService.LoginAsync(dto);
            if (result == null)
                return Unauthorized(new { message = "Invalid email or password" });

            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return Unauthorized(new { message = "Invalid email or password" });

            var token = GenerateJwtToken(user);
            return Ok(new { token });
        }

        // Method to generate JWT token
        private string GenerateJwtToken(AppUser user)
{
    var claims = new[]
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id),
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim(ClaimTypes.Role, user.Role)  // Make sure user.Role is not null!
    };

    // Access JWT settings correctly
    var jwtSettings = _configuration.GetSection("JwtSettings"); // Get the JwtSettings section
    var secret = jwtSettings["Secret"];
    var issuer = jwtSettings["Issuer"];
    var audience = jwtSettings["Audience"];



    if (string.IsNullOrEmpty(secret))
    {
        _logger.LogError("JWT Secret is missing in configuration.");
        throw new InvalidOperationException("JWT Secret is missing in configuration.");
    }

    if (string.IsNullOrEmpty(issuer))
    {
        _logger.LogError("JWT Issuer is missing in configuration.");
        throw new InvalidOperationException("JWT Issuer is missing in configuration.");
    }

    if (string.IsNullOrEmpty(audience))
    {
        _logger.LogError("JWT Audience is missing in configuration.");
        throw new InvalidOperationException("JWT Audience is missing in configuration.");
    }


    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
        issuer: issuer,
        audience: audience,
        claims: claims,
        expires: DateTime.Now.AddHours(1),
        signingCredentials: creds
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
}}

    // Models for SignUp and Login
    public class SignUpModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // Can be "Admin", "HealthcareProfessional", etc.
    }

    // Keep only one LoginDto class in the appropriate namespace
    // This class should belong to HealthcareApp.Application.DTOs
}
