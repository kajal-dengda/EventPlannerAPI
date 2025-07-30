using EventPlannerAPI.Data;
using EventPlannerAPI.Models;

namespace EventPlannerAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly InMemoryDataStore _dataStore;

        public AuthService(InMemoryDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public Task<User> LoginAsync(string username)
        {
            var existingUser = _dataStore.Users.FirstOrDefault(u => u.Username == username);

            if (existingUser == null)
            {
                existingUser = new User
                {
                    Username = username,
                    LastLogin = DateTime.UtcNow
                };
                _dataStore.Users.Add(existingUser);
            }
            else
            {
                existingUser.LastLogin = DateTime.UtcNow;
            }

            return Task.FromResult(existingUser);
        }

        public Task<bool> ValidateUserAsync(string username)
        {
            var user = _dataStore.Users.FirstOrDefault(u => u.Username == username);
            return Task.FromResult(user != null);
        }
    }
}