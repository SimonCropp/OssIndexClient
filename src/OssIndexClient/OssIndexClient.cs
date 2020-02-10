using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace OssIndexClient
{
    public class OssIndex :
        IDisposable
    {
        HttpClient httpClient;
        bool isClientOwned;
        Downloader downloader;

        public OssIndex(HttpClient httpClient)
        {
            Guard.AgainstNull(httpClient, nameof(httpClient));
            this.httpClient = httpClient;
            downloader = new Downloader(httpClient);
        }

        public OssIndex() :
            this(new HttpClient())
        {
            isClientOwned = true;
        }

        public virtual Task<IReadOnlyList<ComponentReport>> GetReports(params Package[] packages)
        {
            return GetReports((IEnumerable<Package>) packages);
        }

        public virtual async Task<IReadOnlyList<ComponentReport>> GetReports(IEnumerable<Package> packages)
        {
            Guard.AgainstNull(packages, nameof(packages));
            packages = packages.ToList();
            var targetPath = TempPath.GetPath(packages);
            var content = JsonSerializer.Serialize(
                new ComponentReportRequestDto
                {
                    coordinates = packages.Select(x => x.Url()).ToArray()
                });
            var uri = "https://ossindex.sonatype.org/api/v3/component-report";
#if NETSTANDARD2_1
            await using var stream = await downloader.Post(targetPath, uri, content);
#else
            using var stream = await downloader.Post(targetPath, uri,content);
#endif
            var reports = await JsonSerializer.DeserializeAsync<ComponentReportDto[]>(stream);
            return reports.Select(ConvertReport).ToList();
        }

        public virtual async Task<ComponentReport> GetReport(Package package)
        {
            Guard.AgainstNull(package, nameof(package));
            var targetPath = TempPath.GetPath(package);

            var uri = $"https://ossindex.sonatype.org/api/v3/component-report/{package.Url()}";
#if NETSTANDARD2_1
            await using var stream = await downloader.Get(targetPath, uri);
#else
            using var stream = await downloader.Get(targetPath, uri);
#endif
            var report = await JsonSerializer.DeserializeAsync<ComponentReportDto>(stream);
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
}