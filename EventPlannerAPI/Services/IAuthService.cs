using EventPlannerAPI.Models;

namespace EventPlannerAPI.Services
{
    public interface IAuthService
    {
        Task<User> LoginAsync(string username);
        Task<bool> ValidateUserAsync(string username);
    }
}