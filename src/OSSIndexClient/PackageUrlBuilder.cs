class PackageUrlBuilder
{
    public static string Build(Package package)
    {
        return $"pkg:{package.Type}/{package.Id}@{package.Version}";
    }
}