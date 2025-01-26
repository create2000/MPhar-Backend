using HealthcareApp.Application.DTOs;

namespace HealthcareApp.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterDto dto);   // For user registration
        Task<string> LoginAsync(LoginDto dto);         // For user login (returns token)
        Task<bool> LogoutAsync(string userId);         // For user logout
        Task<string> RefreshTokenAsync(string refreshToken); // For token refresh
    }
}
