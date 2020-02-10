using System;
using OssIndexClient;

static class CoordinatesHelper
{
    public static string Coordinates(this Package package)
    {
        return $"pkg:{package.EcoSystem}/{package.Id}@{package.Version}";
    }

    public static Package Parse(string coordinates)
    {
        var split = coordinates.Split('/', '@');
        var ecoSystem = (EcoSystem)Enum.Parse(typeof(EcoSystem), split[0].Substring(4));
        var id = split[1];
        var version = split[2];
        return new Package(ecoSystem, id, version);
    }
}