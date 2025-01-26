using HealthcareApp.Domain.Entities;
using HealthcareApp.Application.DTOs;

namespace HealthcareApp.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<AppUser> GetByEmailAsync(string email);
        Task<AppUser> FindByIdAsync(string userId);
        Task CreateUserAsync(AppUser user);
        Task UpdateUserAsync(AppUser user);
        Task AddAsync(AppUser user);
        Task DeleteUserAsync(string userId);
        Task<IEnumerable<AppUser>> GetAllUsersAsync();
    }
}
