using System.Threading.Tasks;
using Verify;
using VerifyXunit;
using Xunit;
using Xunit.Abstractions;

public class Tests :
    VerifyBase
{

    [Fact]
    public async Task GetReport()
    {
        var settings = new VerifySettings();
        settings.ModifySerialization(_ => _.DontScrubGuids());
        #region GetReport

        using var ossIndexClient = new OSSIndexClient();
        var report = await ossIndexClient.GetReport(
            packageType: "nuget",
            package: "System.Net.Http",
            version: "4.3.1");

        #endregion

        await Verify(report, settings );
    }


    public Tests(ITestOutputHelper output) :
        base(output)
    {
    }
}