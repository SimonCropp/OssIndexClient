class PackageUrlBuilder
{
    public static string Build(
        string packageType,
        string package,
        string version)
    {
        return $"pkg:{packageType}/{package}@{version}";
    }
}