using Moq;
using Xunit;
using TaskFlow.Api.Models;
using TaskFlow.Api.Repositories;
using TaskFlow.Api.Utilities;
using TaskFlow.Api.Services;
using System.Threading.Tasks;
using TaskFlow.Api.Repositories.Interfaces;

namespace TaskFlow.Api.Tests
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IPasswordHasher> _passwordHasherMock;
        private readonly Mock<ITokenGenerator> _tokenGeneratorMock;
        private readonly AuthService _authService;

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _passwordHasherMock = new Mock<IPasswordHasher>();
            _tokenGeneratorMock = new Mock<ITokenGenerator>();
            _authService = new AuthService(
                _passwordHasherMock.Object,
                _tokenGeneratorMock.Object,
                _userRepositoryMock.Object
            );
        }

        [Fact]
        public async System.Threading.Tasks.Task RegisterUser_ShouldReturnToken_WhenRegistrationIsSuccessful()
        {
            // Arrange
            var username = "testuser";
            var password = "password123";
            var role = "User";
            var hashedPassword = "hashedPassword";
            var token = "generatedToken";

            _userRepositoryMock.Setup(repo => repo.UserExistsAsync(username))
                .ReturnsAsync(false);
            _passwordHasherMock.Setup(hasher => hasher.HashPassword(password))
                .Returns(hashedPassword);
            _tokenGeneratorMock.Setup(generator => generator.GenerateToken(username, role))
                .Returns(token);

            // Act
            var result = await _authService.RegisterUserAsync(username, password, role);

            // Assert
            Assert.Equal(token, result);
            _userRepositoryMock.Verify(repo => repo.AddUserAsync(It.Is<User>(u =>
                u.UserName == username && u.Password == hashedPassword && u.Role == role)), Times.Once);
        }

        [Fact]
        public async System.Threading.Tasks.Task RegisterUser_ShouldThrowException_WhenUsernameAlreadyExists()
        {
            // Arrange
            var username = "existinguser";
            var password = "password123";
            var role = "User";

            _userRepositoryMock.Setup(repo => repo.UserExistsAsync(username))
                .ReturnsAsync(true);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _authService.RegisterUserAsync(username, password, role));
        }

        [Fact]
        public async System.Threading.Tasks.Task ValidateUser_ShouldReturnTrue_WhenCredentialsAreValid()
        {
            // Arrange
            var username = "validuser";
            var password = "password123";
            var hashedPassword = "hashedPassword";

            var user = new User
            {
                UserName = username,
                Password = hashedPassword,
                Role = "User"
            };

            _userRepositoryMock.Setup(repo => repo.GetByUsernameAsync(username))
                .ReturnsAsync(user);
            _passwordHasherMock.Setup(hasher => hasher.VerifyPassword(hashedPassword, password))
                .Returns(true);

            // Act
            var result = await _authService.ValidateUserAsync(username, password);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async System.Threading.Tasks.Task ValidateUser_ShouldReturnFalse_WhenUserDoesNotExist()
        {
            // Arrange
            var username = "nonexistentuser";
            var password = "password123";

            _userRepositoryMock.Setup(repo => repo.GetByUsernameAsync(username))
                .ReturnsAsync((User)null);

            // Act
            var result = await _authService.ValidateUserAsync(username, password);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async System.Threading.Tasks.Task ValidateUser_ShouldReturnFalse_WhenPasswordIsInvalid()
        {
            // Arrange
            var username = "validuser";
            var password = "wrongpassword";
            var hashedPassword = "hashedPassword";

            var user = new User
            {
                UserName = username,
                Password = hashedPassword,
                Role = "User"
            };

            _userRepositoryMock.Setup(repo => repo.GetByUsernameAsync(username))
                .ReturnsAsync(user);
            _passwordHasherMock.Setup(hasher => hasher.VerifyPassword(hashedPassword, password))
                .Returns(false);

            // Act
            var result = await _authService.ValidateUserAsync(username, password);

            // Assert
            Assert.False(result);
        }
    }
}
