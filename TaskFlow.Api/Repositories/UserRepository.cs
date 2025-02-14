using Microsoft.EntityFrameworkCore;
using TaskFlow.Api.Data;
using TaskFlow.Api.Models;
using TaskFlow.Api.Repositories.Interfaces;

namespace TaskFlow.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async System.Threading.Tasks.Task AddUserAsync(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            return await _dbContext.Users.AnyAsync(u => u.UserName == username);
        }
    }
}
