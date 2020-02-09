using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class OSSIndexClient :
    IDisposable
{
    HttpClient httpClient;
    bool isClientOwned;
    Downloader downloader;

    public OSSIndexClient(HttpClient httpClient)
    {
        Guard.AgainstNull(httpClient, nameof(httpClient));
        this.httpClient = httpClient;
        downloader = new Downloader(httpClient);
    }

    public OSSIndexClient() :
        this(new HttpClient())
    {
        isClientOwned = true;
    }

    public virtual async Task<ComponentReport> GetReport(
        string packageType,
        string package,
        string version)
    {
        var downloadFile = await downloader.DownloadFile(packageType,package,version);
        var report = await JsonSerializer.DeserializeAsync<ComponentReportDto>(downloadFile);
        return new ComponentReport(
            report.coordinates,
            report.description,
            report.reference,
            report.vulnerabilities.Select(x =>
                new Vulnerability(
                    x.id,
                    x.title,
                    x.description,
                    x.cvssScore,
                    x.cvssVector,
                    x.cve,
                    x.cwe,
                    x.reference,
                    x.versionRanges))
                .ToList());
    }

    public void Dispose()
    {
        if (isClientOwned)
        {
            httpClient.Dispose();
        }
    }
}