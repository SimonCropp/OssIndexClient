using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
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

    public async Task<Stream> Post(string targetPath, string uri, string content)
    {
        if (ReadIfRecent(targetPath, out var stream))
        {
            return stream;
        }

        using var stringContent = new StringContent(content, Encoding.UTF8);
#if (NETSTANDARD2_1)
        using (var response = await httpClient.PostAsync(uri, stringContent))
        {
            await using var fileStream = FileHelpers.OpenWrite(targetPath);
            await response.Content.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
        }
#else
        using (var response = await httpClient.PostAsync(uri, stringContent))
        {
            using var fileStream = FileHelpers.OpenWrite(targetPath);
            await response.Content.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
        }
#endif
        return FileHelpers.OpenRead(targetPath);
    }


    public async Task<Stream> Get(string targetPath, string uri)
    {
        if (ReadIfRecent(targetPath, out var stream))
        {
            return stream;
        }

#if (NETSTANDARD2_1)
        await using (var httpStream = await httpClient.GetStreamAsync(uri))
        {
            await using var fileStream = FileHelpers.OpenWrite(targetPath);
            await httpStream.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
        }
#else
        using (var httpStream = await httpClient.GetStreamAsync(uri))
        {
            using var fileStream = FileHelpers.OpenWrite(targetPath);
            await httpStream.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
        }
#endif
        return FileHelpers.OpenRead(targetPath);
    }

    static bool ReadIfRecent(string targetPath, [NotNullWhen(true)] out FileStream? stream)
    {
        if (File.Exists(targetPath))
        {
            var lastWriteTimeUtc = File.GetLastWriteTimeUtc(targetPath);
            if (lastWriteTimeUtc < DateTime.Now.AddHours(-1))
            {
                stream = FileHelpers.OpenRead(targetPath);
                return true;
            }

            File.Delete(targetPath);
        }

        stream = null;
        return false;
    }
}