using System.Threading.Tasks;
using VerifyXunit;
using Xunit;
using Xunit.Abstractions;

public class CoordinatesHelperTests :
    VerifyBase
{
    [Fact]
    public Task Parse()
    {
        return Verify(CoordinatesHelper.Parse("pkg:nuget/System.Net.Security@4.3.0"));
    }

    public CoordinatesHelperTests(ITestOutputHelper output) : base(output)
    {
    }
}