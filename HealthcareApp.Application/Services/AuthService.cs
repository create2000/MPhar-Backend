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

    // Create new user and hash the password
    var newUser = new AppUser
    {
        Email = dto.Email,
        UserName = dto.UserName,
        Role = "User" // Default role
    };
    newUser.PasswordHash = _passwordHasher.HashPassword(newUser, dto.Password);

    // Log the hashed password for debugging
    Console.WriteLine($"Registering user with password hash: {newUser.PasswordHash}");

    // Store the user in the database
    await _userRepository.CreateUserAsync(newUser);

    // Generate and return a token
    return _tokenService.GenerateToken(newUser);
}


public async Task<string> LoginAsync(LoginDto dto)
{
    // Validate user credentials
    var user = await _userRepository.GetByEmailAsync(dto.Email);
    if (user == null)
    {
        throw new UnauthorizedAccessException("Invalid email or password.");
    }

    // Log the hashed password for debugging
    Console.WriteLine($"Logging in user with password hash: {user.PasswordHash}");

    // Verify the password
    var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
    if (passwordVerificationResult != PasswordVerificationResult.Success)
    {
        throw new UnauthorizedAccessException("Invalid email or password.");
    }

    // Generate and return a token
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
