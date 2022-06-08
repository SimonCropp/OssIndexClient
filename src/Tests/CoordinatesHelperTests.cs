[UsesVerify]
public class CoordinatesHelperTests
{
    [Fact]
    public Task Parse() =>
        Verify(CoordinatesHelper.Parse("pkg:nuget/System.Net.Security@4.3.0"));

    [Fact]
    public Task ParseWithNamespace() =>
        Verify(CoordinatesHelper.Parse("pkg:composer/bazalt/angular@4.3.0"));
}