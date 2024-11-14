using System.Security.Cryptography;
using Monitoring.App.Interfaces.Authentication;

namespace Monitoring.Data.Authentication;

public class PasswordHasher : IPasswordHasher
{
    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int Iterations = 100000;
    
    private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA512;

    public void Hash(string password, out byte[] salt, out byte[] hash)
    {
        salt = RandomNumberGenerator.GetBytes(SaltSize);
        hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, HashSize);
    }

    public bool Verify(string password, byte[] passwordSalt, byte[] passwordHash)
    {
        var inputHash = Rfc2898DeriveBytes.Pbkdf2(password, passwordSalt, Iterations, Algorithm, HashSize);

        return CryptographicOperations.FixedTimeEquals(passwordHash, inputHash);
    }
}