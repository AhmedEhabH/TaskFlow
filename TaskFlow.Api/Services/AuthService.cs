using Microsoft.AspNetCore.Identity;
using TaskFlow.Api.Models;
using TaskFlow.Api.Repositories;
using TaskFlow.Api.Repositories.Interfaces;
using TaskFlow.Api.Services.Interfaces;
using TaskFlow.Api.Utilities;

namespace TaskFlow.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IUserRepository _userRepository;

        public AuthService(IPasswordHasher passwordHasher, ITokenGenerator tokenGenerator, IUserRepository userRepository)
        {
            _passwordHasher = passwordHasher;
            _tokenGenerator = tokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<string> RegisterUserAsync(string username, string password, string role)
        {
            // Check if the username already exists
            if (await _userRepository.UserExistsAsync(username))
            {
                throw new InvalidOperationException("Username already exists.");
            }

            // Hash the password
            var hashedPassword = _passwordHasher.HashPassword(password);

            // Create a new user entity
            var user = new User
            {
                UserName = username,
                Password = hashedPassword,
                Role = role
            };

            // Save the user to the database
            await _userRepository.AddUserAsync(user);

            // Generate a token
            return _tokenGenerator.GenerateToken(username, role);
        }

        public async Task<bool> ValidateUserAsync(string username, string password)
        {
            // Retrieve the user from the database
            var user = await _userRepository.GetByUsernameAsync(username);

            if (user == null)
            {
                return false; // User not found
            }

            // Verify the password
            return _passwordHasher.VerifyPassword(user.Password, password);
        }
    }
}
