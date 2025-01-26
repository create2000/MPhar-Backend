using HealthcareApp.Application.Interfaces;
using HealthcareApp.Domain.Entities;
using HealthcareApp.Application.DTOs;
using HealthcareApp.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;


namespace HealthcareApp.Application.Services
{
   public class AuthService : IAuthService

    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IPasswordHasher<AppUser> _passwordHasher;

        public AuthService(IUserRepository userRepository, ITokenService tokenService, IPasswordHasher<AppUser> passwordHasher)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _passwordHasher = passwordHasher;
        }

        public async Task<string> RegisterAsync(RegisterDto dto)
        {
            // Check if user already exists
            var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("User with this email already exists.");
            }

            // Create new user
            var newUser = new AppUser
            {
                Email = dto.Email,
                PasswordHash = dto.Password, // Hash the password in real implementation
                UserName = dto.UserName,
                Role = "User" // Default role
            };

              // Hash the password
            newUser.PasswordHash = _passwordHasher.HashPassword(newUser, dto.Password);
            await _userRepository.CreateUserAsync(newUser);
            // Generate token for the new user
            return _tokenService.GenerateToken(newUser);
        }

        public async Task<string> LoginAsync(LoginDto dto)
        {
            // Validate user credentials
            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if (user == null || user.PasswordHash != dto.Password) // Add password hashing validation
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            // Generate token for the user
            return _tokenService.GenerateToken(user);
        }

         public async Task<bool> LogoutAsync(string userId)
        {
            // For demonstration purposes, you can implement any logic you need for logout,
            // such as invalidating refresh tokens or clearing session data.
            var user = await _userRepository.FindByIdAsync(userId);
            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            // Simulate successful logout (e.g., by clearing tokens or logging out).
            return true; // Indicate successful logout.
        }

        public async Task<string> RefreshTokenAsync(string refreshToken)
        {
            // Replace with your actual token refresh logic.
            // For example, validate the provided refresh token, generate a new token, etc.
            var user = await _userRepository.GetByEmailAsync("example@example.com"); // Replace with actual logic
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid refresh token.");
            }

            // Generate a new token for the user.
            return _tokenService.GenerateToken(user);
        }
    }
}
