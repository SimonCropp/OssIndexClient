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

    public async Task<FileStream> GetPackageResponse(Package package)
    {
        var targetPath = GetPath(package);

        if (File.Exists(targetPath))
        {
            var lastWriteTimeUtc = File.GetLastWriteTimeUtc(targetPath);
            if (lastWriteTimeUtc < DateTime.Now.AddHours(-1))
            {
                return FileHelpers.OpenRead(targetPath);
            }

            File.Delete(targetPath);
        }

        var uri = $"https://ossindex.sonatype.org/api/v3/component-report/{package.Url()}";
#if (NETSTANDARD2_1)
        await using (var httpStream = await httpClient.GetStreamAsync(uri))
        {
            await using var fileStream = new FileStream(targetPath, FileMode.Create, FileAccess.Write, FileShare.None);
            await httpStream.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
        }
#else
        using (var httpStream = await httpClient.GetStreamAsync(uri))
        {
            using var fileStream = new FileStream(targetPath, FileMode.Create, FileAccess.Write, FileShare.None);
            await httpStream.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
        }
#endif
        return FileHelpers.OpenRead(targetPath);
    }

    static char[] invalidPathChars = Path.GetInvalidPathChars();

    static string tempDir = Path.Combine(Path.GetTempPath(), "OSSIndexClient");

    static string GetPath(Package package)
    {
        var packageDir = Path.Combine(tempDir, package.Type, package.Id);
        Directory.CreateDirectory(packageDir);
        var builder = new StringBuilder(packageDir + @"\");
        foreach (var ch in package.Version.Where(ch => !invalidPathChars.Contains(ch)))
        {
            builder.Append(ch);
        }

        builder.Append(".json");
        return builder.ToString();
    }
}