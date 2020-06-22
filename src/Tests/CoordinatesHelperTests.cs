using System.Threading.Tasks;
using VerifyXunit;
using Xunit;

[UsesVerify]
public class CoordinatesHelperTests
{
    [Fact]
    public Task Parse()
    {
        return Verifier.Verify(CoordinatesHelper.Parse("pkg:nuget/System.Net.Security@4.3.0"));
    }

    [Fact]
    public Task ParseWithNamespace()
    {
        return Verifier.Verify(CoordinatesHelper.Parse("pkg:composer/bazalt/angular@4.3.0"));
    }
}