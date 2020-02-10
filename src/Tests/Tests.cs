using System.Diagnostics;
using System.Threading.Tasks;
using OssIndexClient;
using Verify;
using VerifyXunit;
using Xunit;
using Xunit.Abstractions;


public class Tests :
    VerifyBase
{
    [Fact]
    public async Task GetReport404()
    {
        using var ossIndexClient = new OssIndex();
        var report = await ossIndexClient.GetReport(
            new Package(
                ecoSystem: EcoSystem.nuget,
                id: "sdjhgfb",
                version: "4.3.1"));

        await Verify(report);
    }

    [Fact]
    public async Task GetReports()
    {
        var settings = new VerifySettings();
        settings.ModifySerialization(_ => _.DontScrubGuids());

        #region GetReports

        using var ossIndexClient = new OssIndex();
        var reports = await ossIndexClient.GetReports(
            new Package(
                ecoSystem: EcoSystem.nuget,
                id: "System.Net.Http",
                version: "4.3.1"),
            new Package(
                ecoSystem: EcoSystem.nuget,
                id: "System.Net.Security",
                version: "4.3.0"));
        foreach (var report in reports)
        {
            foreach (var vulnerability in report.Vulnerabilities)
            {
                Debug.WriteLine(vulnerability.Title);
            }
        }

        #endregion

        await Verify(reports, settings);
    }

    [Fact]
    public async Task GetReport()
    {
        var settings = new VerifySettings();
        settings.ModifySerialization(_ => _.DontScrubGuids());

        #region GetReport

        using var ossIndexClient = new OssIndex();
        var report = await ossIndexClient.GetReport(
            new Package(
                ecoSystem: EcoSystem.nuget,
                id: "System.Net.Http",
                version: "4.3.1"));

        foreach (var vulnerability in report.Vulnerabilities)
        {
            Debug.WriteLine(vulnerability.Title);
        }
        #endregion

        await Verify(report, settings);
    }

    public Tests(ITestOutputHelper output) :
        base(output)
    {
    }
}