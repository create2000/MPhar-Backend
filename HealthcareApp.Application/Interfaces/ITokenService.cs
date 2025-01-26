using HealthcareApp.Domain.Entities;

namespace HealthcareApp.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(AppUser user);
    }
}
