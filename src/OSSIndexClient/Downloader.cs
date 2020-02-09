using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

class Downloader
{
    HttpClient httpClient;

    public Downloader(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<FileStream> DownloadFile(string targetPath, string uri)
    {
        if (File.Exists(targetPath))
        {
            var lastWriteTimeUtc = File.GetLastWriteTimeUtc(targetPath);
            if (lastWriteTimeUtc < DateTime.Now.AddHours(-1))
            {
                return FileHelpers.OpenRead(targetPath);
            }

            File.Delete(targetPath);
        }

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
}