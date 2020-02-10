using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
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
    private const string RequestContentType = "application/vnd.ossindex.component-report-request.v1+json";

    public async Task<Stream> Post(string targetPath, string uri, string content)
    {
        if (ReadIfRecent(targetPath, out var stream))
        {
            return stream;
        }
        using var httpContent = new StringContent(content, Encoding.UTF8, RequestContentType);
        using (var response = await httpClient.PostAsync(uri, httpContent))
        {
            EnsureOk(uri, response, content);
#if (NETSTANDARD2_1)
            await using var fileStream = FileHelpers.OpenWrite(targetPath);
            await response.Content.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
#else
            using var fileStream = FileHelpers.OpenWrite(targetPath);
            await response.Content.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
#endif
        }

        return FileHelpers.OpenRead(targetPath);
    }


    public async Task<Stream> Get(string targetPath, string uri)
    {
        if (ReadIfRecent(targetPath, out var stream))
        {
            return stream;
        }

        using (var response = await httpClient.GetAsync(uri))
        {
            EnsureOk(uri, response);
#if (NETSTANDARD2_1)
            await using var httpStream = await response.Content.ReadAsStreamAsync();
            var fileStream = FileHelpers.OpenWrite(targetPath);
            await httpStream.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
#else
            using var httpStream = await response.Content.ReadAsStreamAsync();
            using var fileStream = FileHelpers.OpenWrite(targetPath);
            await httpStream.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
#endif
        }
        return FileHelpers.OpenRead(targetPath);
    }

    static void EnsureOk(string uri, HttpResponseMessage response, string? content = null)
    {
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return;
        }

        if (content == null)
        {
            throw new Exception($@"Invalid HTTP response: {response.StatusCode}.
Uri:{uri}");
        }

        throw new Exception($@"Invalid HTTP response: {response.StatusCode}.
Uri:{uri}
Content: {content}");
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