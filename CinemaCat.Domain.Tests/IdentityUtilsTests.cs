using CinemaCat.Domain.Identity;
using FluentAssertions;

namespace CinemaCat.Domain.Tests;

public class IdentityUtilsTests
{
    [Theory]
    [InlineData(10)]
    [InlineData(25)]
    public void GenerateSalt_ReturnRandomString(int length)
    {
        var result = IdentityUtils.GenerateSalt(length);

        result.Should().HaveLength(length);
        var symbolAsserts = result.Select(symbol => (Action)(() => symbol.Should().BeInRange((char)0, 'z'))).ToArray();
        Assert.Multiple(symbolAsserts);
    }

    [Fact]
    public void GetPasswordHash_ReturnsHexString()
    {
        var result = IdentityUtils.GetPasswordHash("pppp");
    }
}