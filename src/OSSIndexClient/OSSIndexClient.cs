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

    public virtual async Task<ComponentReport> GetReport(Package package)
    {
        Guard.AgainstNull(package, nameof(package));
        var targetPath = TempPath.GetPath(package);

        var uri = $"https://ossindex.sonatype.org/api/v3/component-report/{package.Url()}";
        var downloadFile = await downloader.DownloadFile(targetPath, uri);
        var report = await JsonSerializer.DeserializeAsync<ComponentReportDto>(downloadFile);
        return ConvertReport(report);
    }

    static ComponentReport ConvertReport(ComponentReportDto dto)
    {
        return new ComponentReport(
            dto.coordinates,
            dto.description,
            dto.reference,
            dto.vulnerabilities.Select(ConvertVulnerability).ToList());
    }

    static Vulnerability ConvertVulnerability(VulnerabilityDto dto)
    {
        return new Vulnerability(
            dto.id,
            dto.title,
            dto.description,
            dto.cvssScore,
            dto.cvssVector,
            dto.cve,
            dto.cwe,
            dto.reference,
            dto.versionRanges);
    }

    public void Dispose()
    {
        if (isClientOwned)
        {
            httpClient.Dispose();
        }
    }
}