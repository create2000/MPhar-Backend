using HealthcareApp.Application.Interfaces;
using HealthcareApp.Domain.Entities;
using HealthcareApp.Application.DTOs;
using HealthcareApp.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace HealthcareApp.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IPasswordHasher<AppUser> _passwordHasher;
        private readonly UserManager<AppUser> _userManager;

        public AuthService(IUserRepository userRepository, ITokenService tokenService, IPasswordHasher<AppUser> passwordHasher, UserManager<AppUser> userManager)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _passwordHasher = passwordHasher;
            _userManager = userManager;
        }

        public async Task<string> RegisterAsync(RegisterDto dto)
        {
            var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("User with this email already exists.");
            }

            var newUser = new AppUser
            {
                Email = dto.Email,
                UserName = dto.UserName,
            };
            newUser.PasswordHash = _passwordHasher.HashPassword(newUser, dto.Password);

            await _userRepository.CreateUserAsync(newUser);

            var result = await _userManager.AddToRoleAsync(newUser, "User");
            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Failed to assign role to user.");
            }

            var token = await _tokenService.GenerateToken(newUser); // Await here!
            return token;
        }

        public async Task<string> LoginAsync(LoginDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (passwordVerificationResult != PasswordVerificationResult.Success)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            var token = await _tokenService.GenerateToken(user); // Await here!
            return token;
        }

        public async Task<bool> LogoutAsync(string userId)
        {
            var user = await _userRepository.FindByIdAsync(userId);
            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            return true; // Indicate successful logout.
        }

        public async Task<string> RefreshTokenAsync(string refreshToken)
        {
            // ... (Your refresh token logic)

            var user = await _userRepository.GetByEmailAsync("example@example.com"); // Replace with actual logic
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid refresh token.");
            }

            return await _tokenService.GenerateToken(user); // Await here!
        }
    }
}