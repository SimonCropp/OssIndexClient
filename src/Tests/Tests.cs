using System.Diagnostics;
using System.Threading.Tasks;
using OssIndexClient;
using VerifyTests;
using VerifyXunit;
using Xunit;

[UsesVerify]
public class Tests
{
    [Fact]
    public async Task GetReport404()
    {
        using var ossIndexClient = new OssIndex();
        var report = await ossIndexClient.GetReport(
            new Package(
                ecoSystem: EcoSystem.nuget,
                name: "sdjhgfb",
                version: "4.3.1"));

        await Verifier.Verify(report);
    }

    [Fact]
    public async Task GetReports()
    {
        var settings = new VerifySettings();
        settings.ModifySerialization(_ => _.DontScrubGuids());
        settings.AutoVerify();
        #region GetReports

        using var ossIndexClient = new OssIndex();
        var reports = await ossIndexClient.GetReports(
            new Package(
                ecoSystem: EcoSystem.nuget,
                name: "System.Net.Http",
                version: "4.3.1"),
            new Package(
                ecoSystem: EcoSystem.npm,
                name: "jquery",
                version: "1.11.3"));
        foreach (var report in reports)
        {
            foreach (var vulnerability in report.Vulnerabilities)
            {
                Debug.WriteLine(vulnerability.Title);
            }
        }

        #endregion

        await Verifier.Verify(reports, settings);
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
                name: "System.Net.Http",
                version: "4.3.1"));

        foreach (var vulnerability in report.Vulnerabilities)
        {
            Debug.WriteLine(vulnerability.Title);
        }
        #endregion

        await Verifier.Verify(report, settings);
    }
}