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
        using OssIndex ossIndexClient = new();
        var report = await ossIndexClient.GetReport(
            new(
                ecoSystem: EcoSystem.nuget,
                name: "sdjhgfb",
                version: "4.3.1"));

        await Verify(report);
    }

    [Fact]
    public async Task GetReports()
    {
        VerifySettings settings = new();
        settings.DontScrubGuids();
        settings.AutoVerify();

        #region GetReports

        using OssIndex ossIndexClient = new();
        var reports = await ossIndexClient.GetReports(
            new(
                ecoSystem: EcoSystem.nuget,
                name: "System.Net.Http",
                version: "4.3.1"),
            new(
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

        await Verify(reports, settings);
    }

    [Fact]
    public async Task GetReport()
    {
        #region GetReport

        using OssIndex ossIndexClient = new();
        var report = await ossIndexClient.GetReport(
            new(
                ecoSystem: EcoSystem.nuget,
                name: "System.Net.Http",
                version: "4.3.1"));

        foreach (var vulnerability in report.Vulnerabilities)
        {
            Debug.WriteLine(vulnerability.Title);
        }

        #endregion

        await Verify(report).DontScrubGuids();
    }
}