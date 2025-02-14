namespace TaskFlow.Api.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<string> RegisterUserAsync(string username, string password, string role);
        public Task<bool> ValidateUserAsync(string username, string password);
    }
}
