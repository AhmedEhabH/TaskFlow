using TaskFlow.Api.Models;

namespace TaskFlow.Api.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByUsernameAsync(string username);
        System.Threading.Tasks.Task AddUserAsync(User user);
        Task<bool> UserExistsAsync(string username);
    }
}
