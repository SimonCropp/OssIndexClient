using OssIndexClient;

static class PackageUrlBuilder
{
    public static string Url(this Package package)
    {
        return $"pkg:{package.Type}/{package.Id}@{package.Version}";
    }
}