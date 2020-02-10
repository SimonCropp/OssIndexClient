using OssIndexClient;

static class CoordinatesHelper
{
    public static string Coordinates(this Package package)
    {
        return $"pkg:{package.Type}/{package.Id}@{package.Version}";
    }

    public static Package Parse(string coordinates)
    {
        var split = coordinates.Split(new []{'/','@'});
        var type = split[0].Substring(4);
        var id = split[1];
        var version = split[2];
        return new Package(type, id, version);
    }
}