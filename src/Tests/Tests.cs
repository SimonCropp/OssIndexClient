﻿using System.Threading.Tasks;
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
                type: "nuget",
                id: "sdjhgfb",
                version: "4.3.1"));

        await Verify(report);
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
                type: "nuget",
                id: "System.Net.Http",
                version: "4.3.1"));

        #endregion

        await Verify(report, settings);
    }

    public Tests(ITestOutputHelper output) :
        base(output)
    {
    }
}