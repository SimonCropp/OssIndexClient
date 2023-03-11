using System.Text.Json;

namespace OssIndexClient;

public class OssIndex :
    IDisposable
{
    HttpClient httpClient;
    bool isClientOwned;
    Downloader downloader;

    public OssIndex(HttpClient httpClient)
    {
        this.httpClient = httpClient;
        this.httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("OssIndexClient");
        downloader = new(httpClient);
    }

    public OssIndex() :
        this(new()) =>
        isClientOwned = true;

    public virtual Task<IReadOnlyList<ComponentReport>> GetReports(params Package[] packages) =>
        GetReports((IEnumerable<Package>) packages);

    public virtual async Task<IReadOnlyList<ComponentReport>> GetReports(IEnumerable<Package> packages)
    {
        packages = packages.ToList();
        var targetPath = TempPath.GetPath(packages);
        var content = JsonSerializer.Serialize(
            new ComponentReportRequestDto
            {
                coordinates = packages.Select(_ => _.Coordinates()).ToArray()
            });
        var uri = "https://ossindex.sonatype.org/api/v3/component-report";
        await using var stream = await downloader.Post(targetPath, uri, content);
        var reports = await JsonSerializer.DeserializeAsync<ComponentReportDto[]>(stream);
        return reports!.Select(ConvertReport).ToList();
    }

    public virtual async Task<ComponentReport> GetReport(Package package)
    {
        var targetPath = TempPath.GetPath(package);

        var uri = $"https://ossindex.sonatype.org/api/v3/component-report/{package.Coordinates()}";
        await using var stream = await downloader.Get(targetPath, uri);
        var report = await JsonSerializer.DeserializeAsync<ComponentReportDto>(stream);
        return ConvertReport(report!);
    }

    static ComponentReport ConvertReport(ComponentReportDto dto)
    {
        var package = CoordinatesHelper.Parse(dto.coordinates);
        return new(
            package.EcoSystem,
            package.Namespace,
            package.Name,
            package.Version,
            dto.description,
            dto.reference,
            dto.vulnerabilities.Select(ConvertVulnerability).ToList());
    }

    static Vulnerability ConvertVulnerability(VulnerabilityDto dto) =>
        new(dto.id, dto.title, dto.description, dto.cvssScore, dto.cvssVector, dto.cve, dto.cwe, dto.reference, dto.versionRanges, dto.externalReferences);

    public void Dispose()
    {
        if (isClientOwned)
        {
            httpClient.Dispose();
        }
    }
}