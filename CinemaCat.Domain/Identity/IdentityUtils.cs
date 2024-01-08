using System.Security.Cryptography;
using System.Text;

namespace CinemaCat.Domain.Identity;

public static class IdentityUtils
{
    public static string GenerateSalt(int length = 10)
    {
        var random = new Random();
        var chars = Enumerable.Range(0, length)
            .Select(_ => random.NextSaltChar())
            .ToArray();
        return Encoding.ASCII.GetString(chars);
    }

    public static string GetPasswordHash(string value)
    {
        using var sha256 = SHA256.Create();
        return Convert.ToHexString(sha256.ComputeHash(Encoding.UTF8.GetBytes(value)));
    }

    private static byte NextSaltChar(this Random random)
        => (byte)random.Next('0', 'z');
}
