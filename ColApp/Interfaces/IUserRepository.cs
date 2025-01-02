using ColApp.Partials;

namespace ColApp.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAsync(string email);
        Task UpdateUserAsync(User user);
    }

 
}
