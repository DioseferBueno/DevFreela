namespace DevFreela.Infrastructure.Auth
{
    public interface IAuthService
    {
        string ComputerHash(string password);
        string GenerateToken(string email, string role);
    }
}
