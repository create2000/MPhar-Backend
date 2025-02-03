using HealthcareApp.Domain.Entities;
using System.Threading.Tasks; // Ensure you are using this namespace

namespace HealthcareApp.Application.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateToken(AppUser user); // Asynchronous method returning Task<string>
    }
}
