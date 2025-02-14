using TaskFlow.Api.Models;

namespace TaskFlow.Api.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> RegisterAsync(string username, string password);
        Task<string> LoginAsync(string username, string password);
        Task<bool> AssignRoleAsync(string username, string role);
    }
}
