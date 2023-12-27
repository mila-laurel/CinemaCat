using CinemaCat.Domain.Identity;

namespace CinemaCat.Domain.Tests;

public class IdentityUtilsTests
{
    [Theory]
    [InlineData(10)]
    [InlineData(25)]
    public void GenerateSalt_ReturnRandomString(int length)
    {
        var result = IdentityUtils.GenerateSalt(length);

        result.Length.Equals(length);
    }

    [Fact]
    public void GetPasswordHash_ReturnsHexString()
    {
        var result = IdentityUtils.GetPasswordHash("pppp");
    }
}