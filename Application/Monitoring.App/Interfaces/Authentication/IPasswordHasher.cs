namespace Monitoring.App.Interfaces.Authentication;

public interface IPasswordHasher
{
    void Hash(string password, out byte[] salt, out byte[] hash);
    bool Verify(string password, byte[] passwordSalt, byte[] passwordHash);
}