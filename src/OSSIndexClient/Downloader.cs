using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

class Downloader
{
    HttpClient httpClient;

    public Downloader(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }
    public async Task<FileStream> DownloadFile(
        string packageType,
        string package,
        string version)
    {
        var targetPath = GetPath(packageType, package, version);

        if (File.Exists(targetPath))
        {
            var lastWriteTimeUtc = File.GetLastWriteTimeUtc(targetPath);
            if (lastWriteTimeUtc < DateTime.Now.AddHours(-1))
            {
                return FileHelpers.OpenRead(targetPath);
            }

            File.Delete(targetPath);
        }

        var packageUrl = PackageUrlBuilder.Build(packageType, package, version);
        var uri = $"https://ossindex.sonatype.org/api/v3/component-report/{packageUrl}";
        using (var response = await httpClient.GetAsync(uri))
        {
            #if (NETSTANDARD2_1)
            await using var httpStream = await response.Content.ReadAsStreamAsync();
            await using var fileStream = new FileStream(targetPath, FileMode.Create, FileAccess.Write, FileShare.None);
            #else
            using var httpStream = await response.Content.ReadAsStreamAsync();
            using var fileStream = new FileStream(targetPath, FileMode.Create, FileAccess.Write, FileShare.None);
            #endif
            await httpStream.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
        }

        return FileHelpers.OpenRead(targetPath);
    }

    static char[] invalidPathChars = Path.GetInvalidPathChars();

    static string GetPath(
        string packageType,
        string package,
        string version)
    {
        var packageDir = Path.Combine(Path.GetTempPath(), "OSSIndexClient", packageType, package);
        Directory.CreateDirectory(packageDir);
        var builder = new StringBuilder(packageDir + @"\");
        foreach (var ch in version)
        {
            if (invalidPathChars.Contains(ch))
            {
                continue;
            }

            builder.Append(ch);
        }

        builder.Append(".json");
        return builder.ToString();
    }
}