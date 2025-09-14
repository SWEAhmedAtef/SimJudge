using SimJudge.Domain.Entities;

namespace SimJudge.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserByUserNameAsync(string userName);
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
        Task<bool> UserExistsAsync(int id);
        Task<bool> UserExistsByEmailAsync(string email);
        Task<bool> UserExistsByUserNameAsync(string userName);
    }
}
