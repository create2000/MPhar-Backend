using HealthcareApp.Application.DTOs; 
using HealthcareApp.Application.Interfaces; 
using HealthcareApp.Domain.Entities; 
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace HealthcareApp.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<AppUser> _signInManager;

        // Constructor for dependency injection
        public AuthController(
            IAuthService authService,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IConfiguration configuration,
            ILogger<AuthController> logger)
        {
            _authService = authService;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _logger = logger;
        }

        // POST api/auth/register-admin (Admin registration)
        [HttpPost("register-admin")]
        public async Task<IActionResult> RegisterAdmin(RegisterDto dto)
        {
            _logger.LogInformation($"Received UserName: '{dto.UserName}'");

            // Validate that the UserName is valid
            if (string.IsNullOrWhiteSpace(dto.UserName) || !dto.UserName.All(char.IsLetterOrDigit))
            {
                return BadRequest(new { message = "Username can only contain letters and digits." });
            }

            if (dto.Role != "Admin")
            {
                return BadRequest(new { message = "Role must be 'Admin' for Admin registration" });
            }

            // Create the Admin user
            var user = new AppUser
            {
                UserName = dto.UserName,
                Email = dto.Email,
                FullName = dto.FullName
            };

            _logger.LogInformation("Creating Admin user with UserName: " + dto.UserName);

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                _logger.LogError("Error creating user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                return BadRequest(result.Errors);
            }

            // Assign the 'Admin' role to the user
            await _userManager.AddToRoleAsync(user, "Admin");

            _logger.LogInformation("Admin user created successfully.");
            return Ok(new { message = "Admin registered successfully" });
        }

        // POST api/auth/login (Login)
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(dto.Email);
                if (user == null)
                {
                    return Unauthorized(new { message = "Invalid username or email" });
                }
            }

            var isValidPassword = await _userManager.CheckPasswordAsync(user, dto.Password);
            if (!isValidPassword)
            {
                return Unauthorized(new { message = "Invalid password" });
            }

            // Check if the user has a role assigned using UserManager
            var roles = await _userManager.GetRolesAsync(user);
            if (!roles.Contains("Admin") && !roles.Contains("Health Professional") && !roles.Contains("User"))
            {
                return Unauthorized(new { message = "Access denied for this role" });
            }

            var token = GenerateJwtToken(user);
            return Ok(new { token, role = roles });
        }

        [HttpPost("admin/login")] 
    public async Task<IActionResult> AdminLogin([FromBody] LoginDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(dto.Email);
                if (user == null)
                {
                    return Unauthorized(new { message = "Invalid username or email" });
                }
            }

            var isValidPassword = await _userManager.CheckPasswordAsync(user, dto.Password);
            if (!isValidPassword)
            {
                return Unauthorized(new { message = "Invalid password" });
            }

            var roles = await _userManager.GetRolesAsync(user);
            if (!roles.Contains("Admin")) // Check if the user is an Admin
            {
                return Unauthorized(new { message = "Access denied for this role" });
            }

            var token = GenerateJwtToken(user);
            return Ok(new { token, role = roles });
        }


        // Method to generate JWT token
        private async Task<string> GenerateJwtToken(AppUser user)
        {
            // Retrieve the user's roles
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
            };

            // Add the user's roles as claims
            foreach (var role in roles)
            {
                claims.Add(new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", role));
            }

            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secret = jwtSettings["Secret"];
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];

            if (string.IsNullOrEmpty(secret))
            {
                _logger.LogError("JWT Secret is missing in configuration.");
                throw new InvalidOperationException("JWT Secret is missing in configuration.");
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
        }


        // POST api/auth/signup (Optional user registration)
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpModel model)
        {
            var user = new AppUser
            {
                UserName = model.UserName,
                Email = model.Email,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            // Assign role to user
            await _userManager.AddToRoleAsync(user, model.Role);

            return Ok(new { message = "User created successfully" });
        }

        [HttpPost("register-health-professional")]
        public async Task<IActionResult> RegisterHealthProfessional(RegisterDto dto)
        {
            if (dto.Role != "Health Professional")
            {
                return BadRequest(new { message = "Role must be 'Health Professional'" });
            }

            var user = new AppUser
            {
                UserName = dto.UserName,
                Email = dto.Email,
                FullName = dto.FullName,
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            await _userManager.AddToRoleAsync(user, "Health Professional");

            return Ok(new { message = "Health Professional registered successfully" });
        }

        // Authorization to access Admin endpoints
        [Authorize(Roles = "Admin")]
        [HttpGet("admin/dashboard")]
        public IActionResult AdminDashboard()
        {
            return Ok(new { message = "Welcome to the Admin dashboard!" });
        }

        [Authorize(Roles = "HealthProfessional")]
        [HttpGet("healthprofessional/dashboard")]
        public IActionResult HealthProfessionalDashboard()
        {
            return Ok(new { message = "Welcome to the Health Professional dashboard!" });
        }
    }

    // Model for SignUp (make sure it's defined in the same file or imported properly)
    public class SignUpModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } 
    }
}
