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

    public OSSIndexClient(HttpClient httpClient)
    {
        this.httpClient = httpClient;
        Guard.AgainstNull(httpClient, nameof(httpClient));
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
        var uri = $"https://ossindex.sonatype.org/api/v3/component-report/pkg:{packageType}/{package}@{version}";
        var response = await httpClient.GetStringAsync(uri);
        var report = JsonSerializer.Deserialize<ComponentReportDto>(response);
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