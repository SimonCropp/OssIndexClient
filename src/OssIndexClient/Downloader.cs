using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;

class Downloader
{
    HttpClient httpClient;

    public Downloader(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    const string RequestContentType = "application/vnd.ossindex.component-report-request.v1+json";

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
            await using var fileStream = await FileHelpers.SafeOpenWrite(targetPath);
            await response.Content.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
#else
            using var fileStream = await FileHelpers.SafeOpenWrite(targetPath);
            await response.Content.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
#endif
        }

        return await FileHelpers.SafeOpenRead(targetPath);
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
            await using var fileStream = await FileHelpers.SafeOpenWrite(targetPath);
            await httpStream.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
#else
            using var httpStream = await response.Content.ReadAsStreamAsync();
            using var fileStream = await FileHelpers.SafeOpenWrite(targetPath);
            await httpStream.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
#endif
        }
        return await FileHelpers.SafeOpenRead(targetPath);
    }

    static void EnsureOk(string uri, HttpResponseMessage response, string? content = null)
    {
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return;
        }

        if (content == null)
        {
            throw new($@"Invalid HTTP response: {response.StatusCode}.
Uri:{uri}");
        }

        throw new($@"Invalid HTTP response: {response.StatusCode}.
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