namespace TaskFlow.Api.Utilities
{
    public interface ITokenGenerator
    {
        public string GenerateToken(string username, string role);
    }
}
